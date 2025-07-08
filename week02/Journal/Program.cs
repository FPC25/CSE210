using System;

class Program
{
    static void Main(string[] args)
    {
        // Load preferences
        var prefs = new Preferences("./JournalConfig.json");
        List<string> preferences = prefs.LoadPreferences();
        
        // Create journal with preferences
        var journal = new Journal(preferences[0], preferences[2], preferences[3], preferences[1]);
        
        // Main loop using ShowMenu()
        bool keepRunning = true;
        while (keepRunning)
        {
            keepRunning = journal.ShowMenu(); // Returns false when user quits
        }
        
        Console.WriteLine("Thank you for using the journal!");
    }
}