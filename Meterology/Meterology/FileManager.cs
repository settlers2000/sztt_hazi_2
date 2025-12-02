using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Meterology
{
    internal class FileManager
    {

        public List<Data> importFromFile()
        {
            List<Data> list = new List<Data>();
            int numberOfDataLoaded = 0, numberOfDataLost = 0;
            try
            {
                XDocument database = XDocument.Load("Data.xml");
                if (database == null ) { throw new FileLoadException(); }
                numberOfDataLost = database.Descendants("Data").Count();

                foreach (var node in database.Root.Elements("Data"))
                {
                    //var timestamp = DateTime.ParseExact(node.Element("Timestamp").Value, "F", null);
                    var timestamp = DateTime.Parse(node.Element("Timestamp").Value);
                    var value = double.Parse(node.Element("Value").Value);
                    var unit = node.Element("Unit").Value;
                    var source = node.Element("Source").Value;
                    var sensor = node.Element("Sensor")?.Value;

                    Data data = new Data(timestamp, value, unit, source == "imported" ? true : false, sensor);
                    list.Add(data);
                    numberOfDataLoaded++;
                }

            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("File not found!");
            }
            catch(FileLoadException e)
            {
                Console.WriteLine("File is empty!");
            }
            catch(System.Xml.XmlException e)
            {
                Console.WriteLine("File is empty!");
            }
            catch(System.FormatException e)
            {
                numberOfDataLost -= numberOfDataLoaded; 
                Console.WriteLine("Wrong format error!\nSuccessfully loaded data: " + numberOfDataLoaded + "\nFailed data: " +numberOfDataLost);
            }

            return list;
        }
    }
}
