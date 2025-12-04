using Meterology;
using System.Data;
using System.Runtime.CompilerServices;

List<Data> list = new List<Data>();
List<User> users = new List<User>();
User user;

Dictionary<string, (string category, string baseunit, Func<double, double> logic)> map = new Dictionary<string, (string category, string baseunit, Func<double, double> logic)>()
{
    {"c", ("temperature", "°C", val => val)},
    {"°c" , ("temperature", "°C", val => val)},
    {"f" , ("temperature", "°C", val => (val - 32) / (5/9))},
    {"°f" , ("temperature", "°C", val => (val - 32) / (5/9))},
    {"k" , ("temperature", "°C", val => val - 273.15)},

    {"m" , ("size", "m", val => val)},
    {"dm" , ("size", "m", val => val / 10)},
    {"cm" , ("size", "m", val => val / 100)},
    {"mm" , ("size", "m", val => val / 1000)},
    {"km" , ("size", "m", val => val *1000)},
    {"ft" , ("size", "m", val => val * 0.3048)},
    {"in" , ("size", "m", val => val * 0.0254)},

    {"hpa", ("pressure", "hpa", val => val)},
    {"mb" , ("pressure", "hpa", val => val)},
    {"pa" , ("pressure", "hpa", val => val / 100)},
    {"bar" , ("pressure", "hpa", val => val / 1000)},
    {"atm" , ("pressure", "hpa", val => val * 1013.25)},
    {"mmhg" , ("pressure", "hpa", val => val * 1.33322)},
    {"psi" , ("pressure", "hpa", val => val * 68.9476)},

    {"m/s" , ("wind", "m/s", val => val)},
    {"km/h" , ("wind", "km/h", val => val / 3.6)},
    {"mph" , ("wind", "mph", val => val * 0.44704)},
    {"kt" , ("wind", "hpa", val => val * 0.51444)},
    {"kn" , ("wind", "hpa", val => val * 0.51444)},

    {"%" , ("rain", "%", val => val)},
};







Console.WriteLine("Meterology Database");

string command;
while ((command = Console.ReadLine().ToLower()) != "exit")
{
    if (command == "login")
    {
        while (true)
        {
            Console.WriteLine("Input username:");
            string username = Console.ReadLine();
            if (!(string.IsNullOrEmpty(username)))
            {
                if (users.Find(x => x.name == username) == null)
                {
                    user = new User(username);
                    users.Add(user);
                }
                else
                {
                    user = users.Find(x => x.name == username);
                }
                break;
            }
            else { Console.WriteLine("Invalid Command!"); }
        }
        Console.WriteLine("Welcome " + user.name + "!");


        while ((command = Console.ReadLine().ToLower()) != "logout")
        {
            switch (command)
            {
                case "load":
                    list.AddRange(user.loadData());
                    list.Sort(delegate (Data x, Data y)
                    {
                        return x.timestamp.CompareTo(y.timestamp);
                    });
                    break;

                case "list":
                    user.showData(list);
                    break;

                case "filter":
                    user.filterData(list);
                    break;

                case "analyse":
                    user.analyseData(list);
                    break;

                case "user":
                    Console.WriteLine(user.name + "\nChange user or input password?");
                    while (true)
                    {
                        string command2 = Console.ReadLine();
                        if (command2 == "change")
                        {
                            Console.WriteLine("Input username:");
                            string username = Console.ReadLine();
                            if (!(string.IsNullOrEmpty(username)))
                            {
                                if (users.Find(x => x.name == username) == null)
                                {
                                    user = new User(username);
                                    users.Add(user);
                                }
                                else
                                {
                                    user = users.Find(x => x.name == username);
                                }
                                Console.WriteLine("Welcome " + user.name + "!");
                                break;
                            }
                        }
                        else if (command2 == "password")
                        {
                            Console.WriteLine("Input password:");
                            string password = Console.ReadLine();
                            if(password == "admin123")
                            {
                                string temp = user.name;
                                users.Remove(users.Find(x => x.name == user.name));
                                user = new Admin(temp);
                                users.Add(user);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect password!");
                            }
                        }
                        else if (command2 == "back")
                        {
                            break;
                        }
                        else if (command2 == "help")
                        {
                            Console.WriteLine("\nchange\npassword\nback\n");
                        }
                        else
                        {
                            Console.WriteLine("Invalid command!");
                        }
                    }
                    break;

                case "delete":
                    if(user is Admin admin)
                    {
                        admin.deleteAll(list);
                    }
                    else
                    {
                        Console.WriteLine("You dont have permission!");
                    }
                    break;

                case "unit":
                    if (user is Admin admin2)
                    {
                        admin2.changeUnit(list);
                    }
                    else
                    {
                        Console.WriteLine("You dont have permission!");
                    }
                    break;

                case "help":
                    Console.WriteLine("\nload\nlist\nfilter\nanalyse\nuser\ndelete\nunit\nlogout\n");
                    break;

                default:
                    Console.WriteLine("Invalid Command!");
                    break;
            }
        }
    }
    else if(command == "help")
    {
        Console.WriteLine("\nlogin\nexit\n");
    }
    else
    {
        Console.WriteLine("Invalid command!");
    }
}
Console.WriteLine("Bye"); 