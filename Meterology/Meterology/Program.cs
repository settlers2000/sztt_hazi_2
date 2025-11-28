using Meterology;
using System.Linq.Expressions;

List<Data> list = new List<Data>();
Console.WriteLine("Hello, World!");

string command;
while ((command = Console.ReadLine().ToLower()) != "exit")
{
    switch (command)
    {
        case "start":
            Console.WriteLine("Hello There!");
            break;
    }
}
Console.WriteLine("Bye");

