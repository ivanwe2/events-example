using EventExample.Abstractions;
using EventExample.EventArgs;
using EventExample.EventArguments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.Models
{
    public delegate EmergencyEventArgs ConvertToEmergencyEventArgsDel(TemperatureEventArgs eventArgs);
    public class HeatSensor : IHeatSensor
    {
        private readonly double _warningTemperature;
        private readonly double _criticalTemperature;

        private bool _warningTemperatureReached = false;

        private readonly EventHandlerList _listEventDelegates = new();
        private readonly double[] exampleTemperatures = new double[10];

        private static readonly object _temperatureReachesCriticalLevelKey = new();
        private static readonly object _temperatureReachesWarningLevelKey = new();
        private static readonly object _temperatureFallsBackToNormalLevelKey = new();

        private readonly ConvertToEmergencyEventArgsDel _converterDel = EmergencyEventArgs.ConvertToEmergencyArgs;

        public HeatSensor(double warningTemperature, double criticalTemperature)
        {
            _warningTemperature = warningTemperature;
            _criticalTemperature = criticalTemperature;
         

            SeedData(ref exampleTemperatures);
        }

        public void RunHeatSensor()
        {
            Console.WriteLine("Heat sensor is running...");
            MonitorTemperature();
        }

        private static void SeedData(ref double[] arr)
        {
            arr = [22, 30, 26.75, 28.7, 25, 85, 24, 103, 45, 105];
        }

        private void MonitorTemperature()
        {
            foreach (var temperature in exampleTemperatures)
            {
                Console.ResetColor();
                Console.WriteLine($"DateTime: {DateTime.Now}, Temperature: {temperature}");

                TemperatureEventArgs args = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    MeasurementDateTime = DateTime.Now
                };

                if (temperature >= _criticalTemperature)
                {
                    OnCriticalTemperatureReached(_converterDel(args));
                }
                else if(temperature >= _warningTemperature)
                {
                    _warningTemperatureReached = true;

                    OnWarningTemperatureReached(args);
                }
                else if(temperature < _warningTemperature && _warningTemperatureReached) 
                {
                    _warningTemperatureReached = false;

                    OnBackToNormalTemperature(args);
                }

                System.Threading.Thread.Sleep(1000);
            }
        }

        public void OnCriticalTemperatureReached(EmergencyEventArgs args)
        {
            EventHandler<EmergencyEventArgs> handler = (EventHandler<EmergencyEventArgs>)_listEventDelegates[_temperatureReachesCriticalLevelKey]!;
            if(handler is not null) 
                handler(this, args);
        }

        public void OnWarningTemperatureReached(TemperatureEventArgs args)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesWarningLevelKey]!;
            if (handler is not null)
                handler(this, args);
        }
        public void OnBackToNormalTemperature(TemperatureEventArgs args)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureFallsBackToNormalLevelKey]!;
            if (handler is not null)
                handler(this, args);
        }
        event EventHandler<EmergencyEventArgs> IHeatSensor.CriticalTemperatureReachedEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesCriticalLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesCriticalLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.WarningTemperatureReachedEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesWarningLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.BackToSafeTemperatureEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureFallsBackToNormalLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureFallsBackToNormalLevelKey, value);
            }
        }
    }
}
