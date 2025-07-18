using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the ScriptureMemorizer Project.");

        string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding; In all thy ways acknowledge him, and he shall direct thy paths.";

        Scripture test = new Scripture(new Reference("Proverbs", 3, 5, 6), text);
        test.Display();
    }
}