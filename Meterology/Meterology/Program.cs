using Meterology;
using System.Data;
using System.Runtime.CompilerServices;

List<Data> list;
List<User> users = new List<User>();
User user;

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
                case "start":
                    Console.WriteLine("Hello There!");
                    break;

                case "load":
                    user.loadData();
                    break;

                case "list":
                    user.showData();
                    break;

                case "filter":
                    user.filterData();
                    break;

                case "analyse":
                    user.analyseData();
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
                        else
                        {
                            Console.WriteLine("Invalid command!");
                        }
                    }
                    break;

                case "help":
                    Console.WriteLine("\nload\nlist\nfilter\nanalyse\nuser\nlogout\n");
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