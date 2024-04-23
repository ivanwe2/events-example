using EventExample.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.Models
{
    public class CoolingMechanism : ICoolingMechanism
    {
        public void SwitchOff()
        {
            Console.WriteLine();
            Console.WriteLine("Cooling mechanism is on...");
            Console.WriteLine();
        }

        public void SwitchOn()
        {
            Console.WriteLine();
            Console.WriteLine("Cooling mechanism is off...");
            Console.WriteLine();
        }
    }
}
