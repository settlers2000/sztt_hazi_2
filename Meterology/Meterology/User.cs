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
                    Console.WriteLine("Generating Data...\nInput Time Interval:");
                    var time = Console.ReadLine();
                    Console.WriteLine("Input Number Of Data To Generate:");
                    var num = int.Parse(Console.ReadLine());
                    Console.WriteLine("Input Value Range:");
                    var range = Console.ReadLine();

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
    }
}
