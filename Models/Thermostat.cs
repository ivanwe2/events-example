using EventExample.Abstractions;
using EventExample.EventArgs;
using EventExample.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.Models
{
    public class Thermostat : IThermostat
    {   
        private ICoolingMechanism _coolingMechanism; 
        private IHeatSensor _heatSensor;
        private IDevice _device;

        public Thermostat(IDevice device, IHeatSensor heatSensor, ICoolingMechanism coolingMechanism)
        {
            _device = device;
            _coolingMechanism = coolingMechanism;
            _heatSensor = heatSensor;
        }

        private void WireUpEventsToEventHandlers()
        {
            _heatSensor.BackToSafeTemperatureEventHandler += BackToSafeTemperature;
            _heatSensor.WarningTemperatureReachedEventHandler += WarningTemperatureReached;
            _heatSensor.CriticalTemperatureReachedEventHandler += CriticalTemperatureReached;
        }

        private void BackToSafeTemperature(object? sender, TemperatureEventArgs eventArgs)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine($"Information Alert!! Temperature falls below warning level (Warning level is between {_device.WarningLevelTemperature} and {_device.CriticalLevelTemperature})");
            _coolingMechanism.SwitchOff();
            Console.ResetColor();
        }
        private void WarningTemperatureReached(object? sender, TemperatureEventArgs eventArgs)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($"Warning Alert!! (Warning level is between {_device.WarningLevelTemperature} and {_device.CriticalLevelTemperature})");
            _coolingMechanism.SwitchOn();
            Console.ResetColor();
        }
        private void CriticalTemperatureReached(object? sender, EmergencyEventArgs eventArgs)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"Emergency Alert!! (Emergency level is {_device.CriticalLevelTemperature} and above)");
            _device.HandleEmergency(eventArgs);

            Console.ResetColor();
        }

        public void RunThermostat()
        {
            Console.WriteLine("Thermostat is running...");
            WireUpEventsToEventHandlers();
            _heatSensor.RunHeatSensor();
        }
    }
}
