using Meterology;

FileManager fileManager = new FileManager();
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

        case "import":
            fileManager.importFromFile();
            break;
    }
}
Console.WriteLine("Bye");

