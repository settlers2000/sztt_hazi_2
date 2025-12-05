using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meterology
{
    internal interface IAdministration
    {
        public bool changeUser(User user, List<User> users, string name);
        public bool makeAdmin(User user, List<User> users, string name);
    }

    public class Simpleadmin : IAdministration
    {
        public bool changeUser(User user, List<User> users, string name)
        {
            if (!(string.IsNullOrEmpty(name)))
            {
                if (users.Find(x => x.name == name) == null)
                {
                    user = new User(name);
                    users.Add(user);
                }
                else
                {
                    user = users.Find(x => x.name == name);
                }
                Console.WriteLine("Welcome " + user.name + "!");
                return true;
            }
            return false;
        }

        public bool makeAdmin(User user, List<User> users, string password)
        {
            if (password == "admin123")
            {
                string temp = user.name;
                users.Remove(users.Find(x => x.name == user.name));
                user = new Admin(temp);
                users.Add(user);
                Console.WriteLine($"User {user.name} is admin!");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect password!");
                return false;
            }
        }
    }

}
