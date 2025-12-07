using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    public class Data
    {
        public DateTime timestamp { get; set; }
        public double value { get; set; }
        public string unit { get; set; }
        public bool imported { get; set; }
        public string? sensor { get; set; }

        public Data(DateTime timestamp, double value, string unit, bool imported, string sensor)
        {
            this.timestamp = timestamp;
            this.value = value;
            this.unit = unit;
            this.imported = imported;
            this.sensor = sensor;
        }
        public override string ToString()
        {
            return "\nTimestamp: " + timestamp + "\nValue: " + value + "\nUnit: " + unit + "\nSource: " + (imported == true ? "imported" : "generated") + (sensor != null ? "\nSensor: " + sensor : null); 
        }
    }
}