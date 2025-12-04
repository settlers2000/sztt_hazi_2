using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    internal class Admin : User
    {
        public Admin(string name) : base(name)
        {
        }

        public void deleteAll(List<Data> list)
        {
            list.Clear();
            Console.WriteLine("All data has been deleted!");
        }

        public void changeUnit(List<Data> list)
        {
            if (list != null && list.Count > 0)
            {
                Console.WriteLine("Input new default unit:");
                String unit = Console.ReadLine();
                foreach(var data in list)
                {
                    data.unit = unit;
                }
                Console.WriteLine($"The new default unit is {unit}!");
            }
            else
            {
                Console.WriteLine("The list was empty!");
            }
        }
    }
}
