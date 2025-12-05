using Meterology;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;

namespace MeterologyTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadFileTest()
        {
            IFileManager manager = new XmlManager();
            var list = manager.importFromFile("DataTest.xml");

            Assert.IsNotNull(list);
            Assert.AreEqual(4, list.Count(), "Should load exectly 4 items");

            Assert.AreEqual(3.2, list[0].value);
            Assert.AreEqual("°c", list[0].unit);
            Assert.AreEqual("north_sensor", list[0].sensor);
            Assert.AreEqual(true , list[0].imported);
            Assert.AreEqual(DateTime.Parse("2025-01-01T08:00:00") , list[0].timestamp);

            Assert.AreEqual(4.1, list[1].value);
            Assert.AreEqual(DateTime.Parse("2025-01-01T10:00:00"), list[2].timestamp);
            Assert.AreEqual("hpa", list[3].unit);

            StringWriter stringwrite = new StringWriter();
            Console.SetOut(stringwrite);

            list = manager.importFromFile("Nosuchfile");

            string output = stringwrite.ToString();
            StringAssert.Contains(output, "File not found!");
            stringwrite.GetStringBuilder().Clear();


            list = manager.importFromFile("DataWrong.xml");

            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count(), "Should load 3 items, one failed");

            output = stringwrite.ToString();
            StringAssert.Contains(output, "Wrong format error!\nSuccessfully loaded data: 3\nFailed data: 1");
            
        }

        [TestMethod]
        public void DataGenerationTest()
        {
            IDataGenerator generator = new RandomGenerator();

            DateTime[] time = { DateTime.Now, DateTime.Now.AddDays(3) };
            int num = 50;
            double[] range = { 2.3, 100.4 };

            var list = generator.generate(time, num, range);

            Assert.IsNotNull(list);
            Assert.AreEqual(50, list.Count, "Should generate exactly 50 items");

            foreach (var data in list)
            {
                Assert.IsTrue(data.value >= range[0], $"Value {data.value} is below minimum {range[0]}");
                Assert.IsTrue(data.value <= range[1], $"Value {data.value} is above maximum {range[1]}");

                Assert.IsTrue(data.timestamp >= time[0], $"Time {data.timestamp} is before start");
                Assert.IsTrue(data.timestamp <= time[1], $"Time {data.timestamp} is after end");

                Assert.IsFalse(data.imported, "Generated data should have imported = false");
                Assert.IsNull(data.sensor, "Sensor should be null for generated data");
            }

            list = generator.generate(time, 0, range);

            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count, "Should return empty list");
        }
    }
}