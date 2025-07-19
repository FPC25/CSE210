using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the ScriptureMemorizer Project.");

        string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding; In all thy ways acknowledge him, and he shall direct thy paths.";

        Scripture test = new Scripture(new Reference("Proverbs", 3, 5, 6), text);

        Run(test);
    }

    static void DisplayMessage(Scripture scripture, string text)
    {
        scripture.Display();
        Console.WriteLine();
        Console.WriteLine(text);
    }

    static void Run(Scripture scripture)
    {
        string endLine = "Press enter to continue or type 'quit' to finish:";
        int random_amount = 3;

        string line;

        while (true)
        {
            Console.Clear();
            DisplayMessage(scripture, endLine);

            line = Console.ReadLine();

            if (line.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            for (int i = 0; i < random_amount; i++)
            {
                scripture.HideRandomWord();
            }
            

            if (scripture.IsCompletelyHidden())
            {
                break;
            }
        }
    }

}