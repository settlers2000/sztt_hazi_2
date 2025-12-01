using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    internal class FileManager
    {
        public void readTxtFile()
        {
            String temp;
            StreamReader sr = new StreamReader("data.txt");
            temp = sr.ReadLine();
            while(temp != null)
            {
                Console.WriteLine(temp);
                temp = sr.ReadLine();
            }
            sr.Close();
            Console.ReadLine();
        }

        public void writeTxtFile() { }

        public void importFromFile()
        {

        }
    }
}
