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
        private DateTime _timestamp;
        private double _value;
        private string _unit;
        private bool _imported;
        private string? _sensor;

        public DateTime timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
            }
        }
        public double value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public string unit              
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }
        public bool imported
        {
            get
            {
                return _imported;
            }
            set
            {
                _imported = value;
            }
        }
        public string sensor
        {
            get
            {
                return _sensor;
            }
            set
            {
                _sensor = value;
            }
        }

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
            return "\nTimestamp: " + timestamp + "\nValue: " + value + "\nUnit: " + unit + "\nSource: " + (imported == true ? "imported" : "generated") + (sensor != null ? "\nSensor: " + sensor : null); 
        }
    }
}
