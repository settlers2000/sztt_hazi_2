using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    public interface IDataGenerator
    {
        List<Data> generate(DateTime[] time, int num, double[] range);
    }

    public class RandomGenerator : IDataGenerator
    {
        public List<Data> generate(DateTime[] time, int num, double[] range)
        {
            List<Data> list = new List<Data>();
            Random rand = new Random();
            for (int i = 0; i < num; i++)
            {
                TimeSpan timespan = time[1] - time[0];
                TimeSpan newspan = new TimeSpan(0, rand.Next(0, (int)timespan.TotalMinutes), 0);
                var timestamp = time[0] + newspan;
                var value = rand.NextDouble() * (range[1] - range[0]) + range[0];
                var units = UnitConverter.getElements();
                var unit = units[rand.Next(units.Count())];
                var source = false;

                Data data = new Data(timestamp, value, unit, source, null);
                list.Add(data);
            }
            Console.WriteLine("Generated successfully!");
            return list;
        }
    }   
}
