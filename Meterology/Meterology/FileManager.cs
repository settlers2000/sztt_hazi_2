using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            List<Data> list = new List<Data>();
            XDocument database = XDocument.Load("Data.xml");

            foreach(var node in database.Root.Elements("Data"))
            {
                //var timestamp = DateTime.ParseExact(node.Element("Timestamp").Value, "F", null);
                var timestamp = DateTime.Parse(node.Element("Timestamp").Value);
                var value = double.Parse(node.Element("Value").Value);
                var unit = node.Element("Unit").Value;
                var source = node.Element("Source").Value;
                var sensor = node.Element("Sensor")?.Value;

                Data data = new Data(timestamp, value, unit, source == "imported" ? true : false , sensor);
                list.Add(data);
            }
            
            foreach(var data in list)
            {
                Console.WriteLine(data);
            }
        }
    }
}
