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
        Console.WriteLine("Input username:");
        while (true)
        {
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


        while ((command = Console.ReadLine().ToLower()) != "back")
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

                        }
                        else if (command2 == "back")
                        {

                        }
                        else
                        {
                            Console.WriteLine("Invalid command!");
                        }
                    }
                    break;

                case "help":
                    Console.WriteLine("load\nlist\nfilter\nanalyse\nuser\nback");
                    break;

                default:
                    Console.WriteLine("Invalid Command!");
                    break;
            }
        }
    }
    else if(command == "help")
    {
        Console.WriteLine("login\nexit");
    }
}
Console.WriteLine("Bye");