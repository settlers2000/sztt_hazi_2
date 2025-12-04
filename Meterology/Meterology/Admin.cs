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
        }

        public void changeUnit()
        {

        }
    }
}
