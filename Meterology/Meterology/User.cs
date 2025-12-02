using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    internal class User
    {
        private List<Data> list;
        public User()
        {

        }

        public void loadData()
        {
            FileManager fileManager = new FileManager();
            Console.WriteLine("Do you want to import or generate data?");
            while (true)
            {
                var input = Console.ReadLine().ToLower();
                if (input == "import")
                {
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
                        generate(time, num, range);
                    }
                    catch(System.FormatException e)
                    {
                        Console.WriteLine("Wrong format used!");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }

        public void filterData()
        {
            try
            {
                string temp;
                double min, max;
                Console.WriteLine("Makin a filter...\nInput Time Interval (From,To):\nyy-mm-ddThh:mm:ss,yy-mm-ddThh:mm:ss\nOr (From,-) Or (-,To)");
                temp = Console.ReadLine();
                string[] token = temp.Split(",");
                if (token[0] == "-") { token[0] = null; }
                else if (token[1] == "-") { token[1] = null; }
                DateTime[] time = { token[0] == null ? default : DateTime.Parse(token[0]) , token[1] == null ? default : DateTime.Parse(token[1]) };
                Console.WriteLine("Input Minimal value:");
                temp = Console.ReadLine();
                if(!string.IsNullOrEmpty(temp))
                {
                    min = double.Parse(temp);
                }
                else { min = default; }
                Console.WriteLine("Input Maximal value:");
                temp = Console.ReadLine();
                if (!string.IsNullOrEmpty(temp))
                {
                    max = double.Parse(temp);
                }
                else { max = default; }
                Console.WriteLine("Input unit:");
                var unit = Console.ReadLine();
                filter(time, min, max, unit);
            }
            catch (System.FormatException e)
            {
                Console.WriteLine("Wrong format used!");
            }
        }

        public void showData()
        {
            foreach(var data in list)
            {
                Console.WriteLine(data);
            }
            Console.WriteLine("\n");
        }

        public void analyseData()
        {

        }

        public void generate(DateTime[] time, int num, double[] range)
        {
            list = new List<Data>();
            Random rand = new Random();
            for (int i = 0; i < num; i++)
            {
                TimeSpan timespan = time[1] - time[0];
                TimeSpan newspan = new TimeSpan(0, rand.Next(0, (int)timespan.TotalMinutes), 0);
                var timestamp = time[0] + newspan;
                var value = rand.NextDouble() * (range[1] - range[0]) + range[0];
                var unit = "idk yet but im dead";
                var source = false;
                var sensor = "does it need one?";

                Data data = new Data(timestamp, value, unit, source, sensor);
                list.Add(data);
            }
            list.Sort(delegate (Data x, Data y)
            {
                return x.timestamp.CompareTo(y.timestamp);
            });
            Console.WriteLine("Generated successfully!");
        }

        public void filter(DateTime[] time, double min, double max, string unit)
        {
            List<Data> filter = list;
            foreach (var data in filter)
            {
                if(time != null)
                {
                    if (time[0] != default)
                    {
                        filter = filter.Where(data => (data.timestamp > time[0])).ToList();
                    }
                    else if(time[1] != default)
                    {
                        filter = filter.Where(data => (data.timestamp < time[1])).ToList();
                    }
                }
                if(min != default)
                {
                    filter = filter.Where(data => (data.value > min)).ToList();
                }
                if(max != default)
                {
                    filter = filter.Where(data => (data.value < max)).ToList();
                }
                if(unit != null)
                {
                    filter = filter.Where(data => (data.unit == unit)).ToList();
                }
            }
            foreach (var data in filter)
            {
                Console.WriteLine(data);
            }
            Console.WriteLine("\n");
        }
    }
}
