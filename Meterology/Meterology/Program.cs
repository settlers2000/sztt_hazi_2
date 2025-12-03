using Meterology;

List<Data> list;
List<User> users = new List<User>();
User user;

Console.WriteLine("Meterology Database\nInput username:");

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
        Console.WriteLine("Welcome " + user.name + "!");
        break;
    }
}



string command;
while ((command = Console.ReadLine().ToLower()) != "exit")
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
                if(command2 == "change")
                {
                    Console.WriteLine("Input username:");
                    string username = Console.ReadLine();
                    if (!(string.IsNullOrEmpty(username)))
                    {
                        if(users.Find(x => x.name == username) == null)
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
                else if(command2 == "password")
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

        case "back":
            break;

        default:
            Console.WriteLine("Invalid Command!");
            break;
    }
}
Console.WriteLine("Bye");

