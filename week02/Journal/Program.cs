using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Journal Project.");

        //string prefsPath = "./preferences.json";

        string pathToPrompts = "./prompts.json";
        var promptObg = new PromptGuide(pathToPrompts);

        var newEntry = new Entry(promptObg.SelectPrompt());
        newEntry.Display();

    }
}