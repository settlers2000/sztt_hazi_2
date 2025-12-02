using System;
using System.Collections.Generic;
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
                    Console.WriteLine("Wich file? (Now usable for test: Data, Dataempty, Datawrong.xml)");
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

        public void filerData()
        {

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
                TimeSpan newspan = new TimeSpan(rand.Next(0, (int)timespan.TotalMinutes));
                var timestamp = time[0] + newspan;
                var value = rand.NextDouble() * (range[1] - range[0]) + range[0];
                var unit = "idk yet but im dead";
                var source = false;
                var sensor = "does it need one?";

                Data data = new Data(timestamp, value, unit, source, sensor);
                list.Add(data);
            }
            Console.WriteLine("Generated successfully!");
        }
    }
}
