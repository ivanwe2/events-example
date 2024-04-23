using EventExample.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.EventArguments
{
    public enum DeviceStatus
    {
        NoDamage,
        MinorDamage,
        Toasted
    }
    public class EmergencyEventArgs : TemperatureEventArgs
    {
        public DeviceStatus DeviceStatus
        {
            get
            {
                if(this.Temperature < 100)
                    return DeviceStatus.NoDamage;
                else if(this.Temperature >= 100 && Temperature < 105)
                    return DeviceStatus.MinorDamage;
                else 
                    return DeviceStatus.Toasted;
            }
        }

        public override string ToString()
        {
            return $"Critical Temperature of: {Temperature}, was reached on: {MeasurementDateTime}. Device Status: {DeviceStatus}";
        }

        public static EmergencyEventArgs ConvertToEmergencyArgs(TemperatureEventArgs temperatureEventArgs)
            => new EmergencyEventArgs() { Temperature = temperatureEventArgs.Temperature, MeasurementDateTime = temperatureEventArgs.MeasurementDateTime };
    }
}
