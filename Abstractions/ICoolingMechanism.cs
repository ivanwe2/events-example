using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.Abstractions
{
    public interface ICoolingMechanism
    {
        void SwitchOn();
        void SwitchOff();
    }
}
