using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Journal Project.");

        // Change the preferences config file if needed
        string prefsPath = "./JournalConfig.json";

        // Don't touch nothing under here
        Preferences prefs = new Preferences(prefsPath);

        Journal personalLog = new Journal(prefs._userName, prefs._journalName, prefs._journalExtension, prefs._dateFormat);
        personalLog.Menu();

    }
}