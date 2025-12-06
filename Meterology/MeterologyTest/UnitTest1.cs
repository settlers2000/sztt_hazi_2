using Meterology;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;

namespace MeterologyTest
{
    [TestClass]
    public class InterfaceTests
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
            Assert.AreEqual(true, list[0].imported);
            Assert.AreEqual(DateTime.Parse("2025-01-01T08:00:00"), list[0].timestamp);

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

        [TestMethod]
        public void ChangeUserTest()
        {
            IAdministration admin = new Simpleadmin();
            User user = null;
            var users = new List<User>();
            bool userchanged;

            userchanged = admin.changeUser(ref user, users, "user1");

            Assert.IsTrue(userchanged, "Should return true for valid new name");
            Assert.IsNotNull(user, "User reference should be updated");
            Assert.AreEqual(user.name, "user1");
            Assert.AreEqual(1, users.Count(), "List should now contain 1 user");

            userchanged = admin.changeUser(ref user, users, "user2");

            Assert.IsTrue(userchanged, "Should return true for valid new name");
            Assert.IsNotNull(user, "User reference should be updated");
            Assert.AreEqual(user.name, "user2");
            Assert.AreEqual(2, users.Count(), "List should now contain 2 user");

            userchanged = admin.changeUser(ref user, users, "user1");

            Assert.IsTrue(userchanged, "Should return true for valid new name");
            Assert.IsNotNull(user, "User reference should be updated");
            Assert.AreEqual(user.name, "user1");
            Assert.AreEqual(2, users.Count(), "List should now contain 2 user");

            userchanged = admin.changeUser(ref user, users, "");
            Assert.IsFalse(userchanged, "Should return false for not valid new name");
            Assert.AreEqual(user.name, "user1");
            Assert.AreEqual(2, users.Count(), "List should now contain 2 user");
        }

        [TestMethod]
        public void ToAdmin()
        {
            IAdministration admin = new Simpleadmin();
            User user = null;
            var users = new List<User>();
            bool userchanged;

            userchanged = admin.changeUser(ref user, users, "user1");

            userchanged = admin.makeAdmin(ref user, users, "admin123");

            Assert.IsTrue(userchanged, "Should return true for correct password");

            Assert.IsInstanceOfType(user, typeof(Admin), "User variable should now hold an Admin object");

            Assert.AreEqual(1, users.Count);
            Assert.IsInstanceOfType(users[0], typeof(Admin), "The user inside the list should also be an Admin");

            userchanged = admin.changeUser(ref user, users, "user2");

            userchanged = admin.makeAdmin(ref user, users, "wrong");
            Assert.IsFalse(userchanged, "Should return false for wrong password");
            Assert.IsInstanceOfType(user, typeof(User), "Should still be a normal User");
            Assert.IsNotInstanceOfType(user, typeof(Admin), "Should NOT be an Admin");
            Assert.IsInstanceOfType(users[1], typeof(User), "The user inside the list should be a User");
        }
    }

    [TestClass]
    public class AdminPrivilageTest
    {
        private List<Data> list;

        [TestInitialize]
        public void Initialize()
        {
            IFileManager manager = new XmlManager();
            list = manager.importFromFile("DataUnitTest.xml");
        }

        [TestMethod]
        public void DeleteAllDataTest()
        {
            Assert.AreEqual(40, list.Count, "List should have 40 data before Admin deletes all");

            Admin admin1 = new Admin("Admin");

            admin1.deleteAll(list);

            Assert.AreEqual(0, list.Count, "List should be empty after Admin deletes all");
        }

        [TestMethod]
        public void ChangeUnitTest()
        {
            Assert.AreEqual(40, list.Count, "List should have 40 data");

            var tempitem1 = list[0];
            var tempitem2 = list[2];

            Admin admin1 = new Admin("Admin");

            string simulatedInput = "°f\n";
            Console.SetIn(new StringReader(simulatedInput));


            admin1.changeUnit(list);

            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            Assert.AreEqual("°f", tempitem1.unit);
            Assert.AreEqual(37.76, tempitem1.value, 0.01, "3.2°C should become approx 37.76°F");

            Assert.AreEqual("hPa", tempitem2.unit);
            Assert.AreEqual(1013.2, tempitem2.value, "Pressure should not change when converting temp");

            int count = list.Count(x => x.unit == "°f" || x.unit == "°F");
            Assert.AreEqual(13, count, "All 13 temperature items should now be Fahrenheit");

            // Clean up console
            sw.Dispose();
        }
    }

    [TestClass]
    public class FilterTests
    {
        List<Data> list;
        User user;

        [TestInitialize]
        public void Initialize()
        {
            user = new User("user1");
            list = new List<Data>
            {
                new Data(DateTime.Parse("2025-01-01T08:00:00"), 5.0, "m", true, "sensor1"),
                new Data(DateTime.Parse("2025-01-01T12:00:00"), 50.0, "m", true, "sensor2"),
                new Data(DateTime.Parse("2025-01-01T12:00:00"), 5.0, "m", true, "sensor3"),
                new Data(DateTime.Parse("2025-01-02T08:00:00"), 15.0, "m", true, "ssensor4")
            };
        }

        [TestMethod]
        public void DateTimeFilter()
        {
            DateTime[] time1 = {
                DateTime.Parse("2025-01-01T10:00:00"),
                DateTime.Parse("2025-01-01T14:00:00")
            };

            DateTime[] time2 = {
                DateTime.Parse("2025-01-01T10:00:00"),
                default
            };

            DateTime[] time3 = {
                default,
                DateTime.Parse("2025-01-01T14:00:00")
            };

            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            user.filter(list, time1, null, null, null);

            string output = sw.ToString();

            Assert.IsFalse(output.Contains("Timestamp: 01/01/2025 08:00:00"), "Wrong time shouldnt show.");
            Assert.IsFalse(output.Contains("Timestamp: 02/01/2025"), "Wrong time shouldnt show.");
            sw.GetStringBuilder().Clear();

            user.filter(list, time2, null, null, null);

            output = sw.ToString();

            Assert.IsFalse(output.Contains("Timestamp: 01/01/2025 08:00:00"), "Wrong time shouldnt show.");
            StringAssert.Contains(output, "Timestamp: 02/01/2025 08:00:00", "Right time should show.");
            sw.GetStringBuilder().Clear();

            user.filter(list, time3, null, null, null);

            output = sw.ToString();

            Assert.IsFalse(output.Contains("Timestamp: 02/01/2025"), "Wrong time shouldnt show.");
            Assert.IsTrue(output.Contains("Timestamp: 01/01/2025 08:00:00"), "Right time should show.");

            sw.Dispose();
        }

        [TestMethod]
        public void ValueFilter()
        {
            DateTime[] time = {
                default,
                default
            };

            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            user.filter(list, time, 20, null, null);

            string output = sw.ToString();

            Assert.IsFalse(output.Contains("Value : 5"), "Wrong value shouldnt show.");
            Assert.IsFalse(output.Contains("Value : 15"), "Wrong value shouldnt show.");
            sw.GetStringBuilder().Clear();

            user.filter(list, time, null, 20, null);

            output = sw.ToString();

            Assert.IsFalse(output.Contains("Value: 50"), "Wrong value shouldnt show.");
            StringAssert.Contains(output, "Value: 5", "Right value should show.");
            StringAssert.Contains(output, "Value: 15", "Right value should show.");
            sw.GetStringBuilder().Clear();

            user.filter(list, time, 10, 20, null);

            output = sw.ToString();

            Assert.IsFalse(output.Contains("Value: 50"), "Wrong value shouldnt show.");
            Assert.IsFalse(output.Contains("Value: 5"), "Wrong value shouldnt show.");
            Assert.IsTrue(output.Contains("Value: 15"), "Right value should show.");

            sw.Dispose();
        }
    }
}