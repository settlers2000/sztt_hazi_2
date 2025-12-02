using Meterology;

List<Data> list;
User user;
Console.WriteLine("Meterology Database\nInput username:");

while (true)
{
    string username = Console.ReadLine();
    if (!(string.IsNullOrEmpty(username)))
    {
        user = new User(username);
        Console.WriteLine("Welcome" + username + "!");
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

        default:
            Console.WriteLine("Invalid Command!");
            break;
    }
}
Console.WriteLine("Bye");

