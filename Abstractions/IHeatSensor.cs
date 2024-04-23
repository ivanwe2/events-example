using EventExample.EventArgs;
using EventExample.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.Abstractions
{
    public interface IHeatSensor
    {
        void RunHeatSensor(); 

        event EventHandler<EmergencyEventArgs> CriticalTemperatureReachedEventHandler;

        event EventHandler<TemperatureEventArgs> WarningTemperatureReachedEventHandler;

        event EventHandler<TemperatureEventArgs> BackToSafeTemperatureEventHandler;
    }

}
