using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Meterology
{
    internal class FileManager
    {

        public List<Data> importFromFile(string file)
        {
            List<Data> list = new List<Data>();
            int numberOfDataLoaded = 0, numberOfDataLost = 0;
            try
            {
                XDocument database = XDocument.Load(file);
                if (database == null ) { throw new FileLoadException(); }

                foreach (var node in database.Root.Elements("Data"))
                {
                    try
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
                    catch (System.FormatException e)
                    {
                        numberOfDataLost++;
                        continue;
                    }                
                }

                list.Sort(delegate (Data x, Data y)
                {
                    return x.timestamp.CompareTo(y.timestamp);
                });

                if(numberOfDataLost > 0)
                {
                    Console.WriteLine("Wrong format error!\nSuccessfully loaded data: " + numberOfDataLoaded + "\nFailed data: " + numberOfDataLost);
                }
                else { Console.WriteLine("Imported successfully!"); }
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
            return list;
        }
    }
}
