using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    public class User
    {
        public string name { get; set; }
        public User(string name)
        {
            this.name = name;
        }

        public List<Data> loadData()
        {
            List<Data> list = new List<Data>();
            Console.WriteLine("Do you want to import or generate data?");
            while (true)
            {
                var input = Console.ReadLine().ToLower();
                if (input == "import")
                {

                    IFileManager fileManager = new XmlManager();
                    Console.WriteLine("Wich file? (Now usable for test: Data, Dataempty, Datamix Datawrong.xml)");
                    var file = Console.ReadLine();
                    list = fileManager.importFromFile(file);
                    break;
                }
                else if (input == "generate")
                {
                    try
                    {
                        string temp;
                        Console.WriteLine("Generating Data...\nInput Time Interval (From-To):\nyy-mm-ddThh:mm:ss,yy-mm-ddThh:mm:ss");
                        temp = Console.ReadLine();
                        string[] token = temp.Split(",");
                        DateTime[] time = { DateTime.Parse(token[0]), DateTime.Parse(token[1]) };
                        Console.WriteLine("Input Number Of Data To Generate:");
                        var num = int.Parse(Console.ReadLine());
                        Console.WriteLine("Input Value Range (1,200):");
                        temp = Console.ReadLine();
                        token = temp.Split(",");
                        double[] range = { double.Parse(token[0]), double.Parse(token[1]) };
                        IDataGenerator generator = new RandomGenerator();
                        list = generator.generate(time, num, range);
                    }
                    catch(System.FormatException e)
                    {
                        Console.WriteLine("Wrong format used!");
                    }
                    catch (System.IndexOutOfRangeException e)
                    {
                        Console.WriteLine("Wrong format used!");
                    }
                    break;
                }
                else if (input == "help")
                {
                    Console.WriteLine("\nimport\ngenerate\nback\n");
                }
                else if (input == "back")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
            return list;
        }

        public void filterData(List<Data> list)
        {
            try
            {
                string temp;
                double? min = null, max = null;
                DateTime[] time = new DateTime[2];
                Console.WriteLine("Makin a filter...\nInput Time Interval (From,To):\nyy-mm-ddThh:mm:ss,yy-mm-ddThh:mm:ss\nOr (From,-) Or (-,To)");
                temp = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(temp) && temp.Contains(","))
                {
                    string[] token = temp.Split(",");
                    if (token[0] == "-") { token[0] = null; }
                    if (token[1] == "-") { token[1] = null; }
                    time[0] = token[0] == null ? default : DateTime.Parse(token[0]);
                    time[1] = token[1] == null ? default : DateTime.Parse(token[1]);
                }
                else
                {
                    time[0] = default;
                    time[1] = default;
                }
                Console.WriteLine("Input Minimal value:");
                temp = Console.ReadLine();
                if(!string.IsNullOrEmpty(temp))
                {
                    min = double.Parse(temp);
                }
                Console.WriteLine("Input Maximal value:");
                temp = Console.ReadLine();
                if (!string.IsNullOrEmpty(temp))
                {
                    max = double.Parse(temp);
                }
                Console.WriteLine("Input unit:");
                var unit = Console.ReadLine();
                filter(list, time, min, max, unit);
            }
            catch (System.FormatException e)
            {
                Console.WriteLine("Wrong format used!");
            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine("Wrong format used!");
            }
        }

        public void showData(List<Data> list)
        {
            if(list != null) 
            {
                int counter = 0;
                foreach (var data in list)
                {
                    Console.WriteLine(data);
                    counter++;

                    if(counter % 5 == 0)
                    {
                        Console.WriteLine("Press enter to continue, q to exit.");
                        while(true)
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
            else
            {
                Console.WriteLine("List is empty!");
            }
        }

        public void analyseData(List<Data> list)
        {
            string temp;
            Console.WriteLine("Analysing data...\nMaking statistics of minimum, maximum, average, size(number of data):\nDo you want day by day statistic?");
            bool dayly = false;
            while (true)
            {
                temp = Console.ReadLine();
                if (temp == "yes" || temp == "y")
                {
                    dayly = true;
                    break;
                }
                else if (temp == "no" || temp == "n")
                {
                    dayly = false;
                    break;
                }
                else if (temp == "back")
                {
                    return;
                }
                else if (temp == "help")
                {
                    Console.WriteLine("\nyes/y\nno/n\nback\n");
                }
                else
                {
                    Console.WriteLine("Invalid Command!");
                }
            }
            IStatistics statistics = new Statistic();
            statistics.statistics(dayly, list);
        }

        public void filter(List<Data> list, DateTime[] time, double? min, double? max, string unit)
        {
            List<Data> filter = list;

            if (time != null)
            {
                if (time[0] != default)
                {
                    filter = filter.Where(data => (data.timestamp > time[0])).ToList();
                }
                if (time[1] != default)
                {
                    filter = filter.Where(data => (data.timestamp < time[1])).ToList();
                }
            }
            if (min.HasValue)
            {
                filter = filter.Where(data => (data.value > min)).ToList();
            }
            if (max.HasValue)
            {
                filter = filter.Where(data => (data.value < max)).ToList();
            }
            if (!string.IsNullOrEmpty(unit))
            {
                filter = filter.Where(data => (data.unit == unit)).ToList();
            }

            if (filter != null && filter.Count > 0)
            {
                int counter = 0;
                foreach (var data in filter)
                {
                    Console.WriteLine(data);
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
            else
            {
                Console.WriteLine("List is empty!");
            }
        }
    }
}
