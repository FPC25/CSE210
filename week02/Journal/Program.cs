using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Journal Project.");

        // Change the preferences config file if needed
        string prefsPath = "./JournalConfig.json";

        // Don't touch nothing under here
        var prefs = new Preferences(prefsPath);

        string userName = prefs._userName;
        string dateFormat = prefs._dateFormat;
        string ext = prefs._journalExtension;

        var promptObg = new PromptGuide("./prompts.json");

        var newEntry = new Entry(promptObg.SelectPrompt(), dateFormat);
        newEntry.Display();

    }
}