using Meterology;

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
            Assert.AreEqual(4, list.Count(), "Load exectly 4 items");

            Assert.AreEqual(3.2, list[0].value);
            Assert.AreEqual("°c", list[0].unit);
            Assert.AreEqual("north_sensor", list[0].sensor);
            Assert.AreEqual(true , list[0].imported);
            Assert.AreEqual(DateTime.Parse("2025-01-01T08:00:00") , list[0].timestamp);

            Assert.AreEqual(4.1, list[1].value);
            Assert.AreEqual(DateTime.Parse("2025-01-01T10:00:00"), list[2].timestamp);
            Assert.AreEqual("hpa", list[3].unit);
        }
    }
}