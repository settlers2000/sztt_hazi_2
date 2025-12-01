using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Meterology
{
    internal class FileManager
    {

        public void importFromFile()
        {
            XDocument data = XDocument.Load("Data.xml");

            foreach(var node in data.Root.Elements("Data"))
            {
                var timestamp = node.Element("Timestamp").Value;
                Console.WriteLine(timestamp);
            }
            
        }
    }
}
