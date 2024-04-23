using EventExample.Abstractions;
using EventExample.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.Models
{
    public class Device : IDevice
    {
        const double Warning_Level = 27;
        const double Critical_Level = 75;

        public double WarningLevelTemperature => Warning_Level;

        public double CriticalLevelTemperature => Critical_Level;

        public void HandleEmergency(EmergencyEventArgs eventArgs)
        {
            Console.WriteLine();
            ShutDownDevice();
            Console.WriteLine(eventArgs.ToString());
            Console.WriteLine();
        }

        private void ShutDownDevice()
        {
            Console.WriteLine("Restarting device device...");
            System.Threading.Thread.Sleep(2000);
        }

        public void RunDevice()
        {
            Console.WriteLine("Device is running...");

            ICoolingMechanism coolingMechanism = new CoolingMechanism();
            IHeatSensor heatSensor = new HeatSensor(Warning_Level, Critical_Level);
            IThermostat thermostat = new Thermostat(this, heatSensor, coolingMechanism);

            thermostat.RunThermostat();

        }
    }
}
