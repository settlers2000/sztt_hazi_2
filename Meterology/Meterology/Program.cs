using Meterology;

User user = new User();
List<Data> list;
Console.WriteLine("Hello, World!");

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
    }
}
Console.WriteLine("Bye");

