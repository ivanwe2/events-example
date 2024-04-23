using EventExample.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.Abstractions
{
    public interface IDevice
    {
        double WarningLevelTemperature { get; }
        double CriticalLevelTemperature { get; }
        void RunDevice();
        void HandleEmergency(EmergencyEventArgs eventArgs);
    }
}
