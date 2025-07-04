using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Journal Project.");

        // Change the preferences config file if needed
        string prefsPath = "./JournalConfig.json";

        // Don't touch nothing under here
        var prefs = new Preferences(prefsPath);

        Console.WriteLine(prefs._userName);

        var promptObg = new PromptGuide("./prompts.json");

        var newEntry = new Entry(promptObg.SelectPrompt());
        newEntry.Display();

    }
}