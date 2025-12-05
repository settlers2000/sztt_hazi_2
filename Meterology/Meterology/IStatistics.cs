using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    public interface IStatistics
    {
        public void statistics(bool dayly, List<Data> list);
    }

    public class Statistic : IStatistics
    {
        public void statistics(bool dayly, List<Data> list)
        {
            var normalizedlist = list.Select(x => UnitConverter.Normalize(x)).ToList();

            if (dayly)
            {
                var daylist = normalizedlist.GroupBy(x => x.timestamp.Date);
                int counter = 0;

                foreach (var group in daylist)
                {
                    Console.WriteLine($"Day: {group.Key}");

                    var categorylist = group.GroupBy(x => x.category);
                    foreach (var group2 in categorylist)
                    {
                        string category = group2.Key;
                        string baseUnit = group2.First().baseunit;

                        double min = group2.Min(x => x.value);
                        double max = group2.Max(x => x.value);
                        double avarege = group2.Average(x => x.value);
                        double count = group2.Count();

                        Console.WriteLine($"Category: {category}, Unit: {baseUnit}\nMinimum: {min}\nMaximum: {max}\nAvarege: {avarege}\nCount: {count}");
                        counter++;
                        if (counter % 5 == 0)
                        {
                            Console.WriteLine("Press enter to continue, q to exit.");
                            while (true)
                            {
                                var command = Console.ReadLine();
                                if (string.IsNullOrEmpty(command))
                                {
                                    break;
                                }
                                else if (command == "q")
                                {
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid command!");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var categorylist = normalizedlist.GroupBy(x => x.category);

                foreach (var group in categorylist)
                {
                    string category = group.Key;
                    string baseUnit = group.First().baseunit;

                    double min = group.Min(x => x.value);
                    double max = group.Max(x => x.value);
                    double avarege = group.Average(x => x.value);
                    double count = group.Count();

                    Console.WriteLine($"Category: {category}, Unit: {baseUnit}\nMinimum: {min}\nMaximum: {max}\nAvarege: {avarege}\nCount: {count}");
                }
            }
        }
    }
}