using EventExample.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExample.EventArgs
{
    public class TemperatureEventArgs : System.EventArgs
    {
        public double Temperature { get; set; }
        public DateTime MeasurementDateTime { get; set; }
        
    }
}
