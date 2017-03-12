using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraTravelManager
{
    public class Battery
    {
        public int id { get; set; }
        public string label { get; set; }
        public double batteryLevel { get; set; }
        public Battery() { }
        public Battery(string label, double batteryLevel)
        {
            this.batteryLevel = batteryLevel;
            this.label = label;
        }
        public Battery(string label)
        {
            this.label = label;
        }
    }

    public class SDCard
    {

    }
}
