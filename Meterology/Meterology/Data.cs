using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    internal class Data
    {
        private DateTime timestamp;
        private double value;
        private string unit;
        private bool imported;
        private string sensor;
        public Data(DateTime timestamp, double value, string unit, bool imported, string sensor)
        {
            this.timestamp = timestamp;
            this.value = value;
            this.unit = unit;
            this.imported = imported;
            this.sensor = sensor;
        }

        public void changeUnit()
        {

        }

        public override string ToString()
        {
            return "Timestamp:" + timestamp + "\nValue:" + value + "Unit:" + unit + "Source" + (imported == true ? "imported" : "generated") + "Sensor" + sensor; 
        }
    }
}
