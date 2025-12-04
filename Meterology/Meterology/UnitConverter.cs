using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    public static class UnitConverter
    {
        private static readonly Dictionary<string, (string category, string baseunit, Func<double, double> toBase, Func<double, double> frombase)> map = new Dictionary<string, (string category, string baseunit, Func<double, double> toBase, Func<double, double> frombase)>()
        {
            {"c", ("temperature", "°C", val => val, val => val)},
            {"°c" , ("temperature", "°C", val => val, val => val)},
            {"f" , ("temperature", "°C", val => (val - 32) / (9.0/5.0), val => (val * (9.0/5.0)) + 32)},
            {"°f" , ("temperature", "°C", val => (val - 32) / (9.0/5.0), val => (val * (9.0/5.0)) + 32)},
            {"k" , ("temperature", "°C", val => val - 273.15, val => val + 273.15)},

            {"m" , ("size", "m", val => val, val => val)},
            {"dm" , ("size", "m", val => val / 10, val => val * 10)},
            {"cm" , ("size", "m", val => val / 100, val => val * 100)},
            {"mm" , ("size", "m", val => val / 1000, val => val * 1000)},
            {"km" , ("size", "m", val => val * 1000, val => val / 1000)},
            {"ft" , ("size", "m", val => val * 0.3048, val => val / 0.3048)},
            {"in" , ("size", "m", val => val * 0.0254, val => val / 0.0254)},

            {"hpa", ("pressure", "hpa", val => val, val => val)},
            {"mb" , ("pressure", "hpa", val => val, val => val)},
            {"pa" , ("pressure", "hpa", val => val / 100, val => val * 100)},
            {"bar" , ("pressure", "hpa", val => val * 1000, val => val / 1000)},
            {"atm" , ("pressure", "hpa", val => val * 1013.25, val => val / 1013.25)},
            {"mmhg" , ("pressure", "hpa", val => val * 1.33322, val => val / 1.33322)},
            {"psi" , ("pressure", "hpa", val => val * 68.9476, val => val / 68.9476)},

            {"m/s" , ("wind", "m/s", val => val, val => val)},
            {"km/h" , ("wind", "m/s", val => val / 3.6, val => val * 3.6)},
            {"mph" , ("wind", "m/s", val => val * 0.44704, val => val / 0.44704)},
            {"kt" , ("wind", "m/s", val => val * 0.51444, val => val / 0.51444)},
            {"kn" , ("wind", "m/s", val => val * 0.51444, val => val / 0.51444)},

            {"%" , ("rain", "%", val => val, val => val)},
        };

        public static void changeDefaultUnit(List<Data> list, string unit)
        {
            if (!map.ContainsKey(unit))
            {
                Console.WriteLine("Unknown unit!");
                return;
            }
            else
            {
                var targetRule = map[unit];

                foreach(var data in list)
                {
                    var current = data.unit;

                    if (map.ContainsKey(current))
                    {
                        var currentRule = map[current];

                        if(currentRule.category == targetRule.category)
                        {
                            double baseVal = currentRule.toBase(data.value);
                            double final = targetRule.frombase(baseVal);

                            data.value = final;
                            data.unit = unit;
                        }
                    }
                }
            }
            Console.WriteLine("Converted default unit!");
        }
    }
}
