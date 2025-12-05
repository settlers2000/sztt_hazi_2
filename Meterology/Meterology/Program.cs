using Meterology;
using System.Data;
using System.Runtime.CompilerServices;

List<Data> list = new List<Data>();
List<User> users = new List<User>();
User user = null;
IAdministration administration = new Simpleadmin();


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
            if(administration.changeUser(user, users, username))
            {
                break;
            }
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
                            if (administration.changeUser(user, users, username))
                            {
                                Console.WriteLine("Welcome " + user.name + "!");
                                break;
                            }
                        }
                        else if (command2 == "password")
                        {
                            Console.WriteLine("Input password:");
                            string password = Console.ReadLine();
                            if(administration.makeAdmin(user, users, password))
                            {
                                Console.WriteLine($"User {user.name} is admin!");
                                break;
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