using Meterology;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterologyTest
{
    [TestClass]
    public class StressTest
    {
        [TestMethod]
        public void ImportTest()
        {
            List<Data> list = new List<Data>();
            int numOfData = 10000;
            string filename = "Datastresstest.xml";

            StreamWriter writer = new StreamWriter(filename);

            writer.WriteLine("<Database>");
            for (int i = 0; i < numOfData; i++)
            {
                writer.WriteLine("<Data>");
                writer.WriteLine($"<Timestamp>{DateTime.Now.ToString()}</Timestamp>");
                writer.WriteLine($"<Value>{i}</Value>");
                writer.WriteLine("<Unit>bar</Unit>");
                writer.WriteLine("<Source>imported</Source>");
                writer.WriteLine("<Sensor>sensor1</Sensor>");
                writer.WriteLine("</Data>");
            }
            writer.WriteLine("</Database>");
            writer.Close();
            IFileManager manager = new XmlManager();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            list = manager.importFromFile(filename);
            stopwatch.Stop();

            if (File.Exists(filename)) { File.Delete(filename); }

            Assert.AreEqual(numOfData, list.Count());
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 500, "Too slow");
        }

        [TestMethod]
        public void GenerateTest()
        {
            IDataGenerator generator = new RandomGenerator();

            DateTime[] time = { DateTime.Now, DateTime.Now.AddDays(3) };
            int num = 10000;
            double[] range = { 2.3, 100.4 };
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var list = generator.generate(time, num, range);

            stopwatch.Stop();
            Assert.IsNotNull(list);
            Assert.AreEqual(10000, list.Count, "Should generate exactly 10000 items");

            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 500, "Too slow");
        }
    }
}
