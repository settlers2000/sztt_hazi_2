using Meterology;
using System.Linq.Expressions;

List<Data> list = new List<Data>();
Console.WriteLine("Hello, World!\n");

string command;
while ((command = Console.ReadLine().ToLower()) != "exit")
{
    switch (command)
    {
        case "start":
            Console.WriteLine("Hello There!\n");
            break;
    }
}
Console.WriteLine("Bye");

