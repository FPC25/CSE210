using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Scripture Memorizer");
        Console.WriteLine("===================\n");

        // Initialize the ScriptureManager
        ScriptureManager manager = new ScriptureManager("scriptures");

        // Check if we have scripture files
        if (manager.GetAvailableFiles().Count == 0)
        {
            Console.WriteLine("No scripture files found in 'scriptures' folder!");
            Console.WriteLine("Please add JSON files to the scriptures directory.");
            return;
        }

        List<string> options = new List<string> 
        { 
            "Browse and select a scripture", 
            "Enter a scripture reference to search", 
            "Quit" 
        };

        // Main menu loop
        while (true)
        {
            Console.WriteLine("\nHow would you like to find a scripture?"); 
            int choice = Utils.Decision(options);
            
            Scripture scripture = null;
            
            try
            {
                switch (choice)
                {
                    case 0: // Manual browsing
                        scripture = manager.SelectScriptureManually();
                        break;
                        
                    case 1: // Search by reference
                        scripture = manager.SearchByReference();
                        break;
                        
                    case 2: // Quit
                        Console.WriteLine("Thank you for using the Scripture Memorizer!");
                        return;
                }
                
                if (scripture != null)
                {                  
                    Run(scripture);
                }
                else
                {
                    Console.WriteLine("No scripture selected. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
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
                Console.WriteLine();
                break;
            }

            for (int i = 0; i < random_amount; i++)
            {
                scripture.HideRandomWord();
            }
            

            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                DisplayMessage(scripture, endLine);
                Console.WriteLine();
                break;
            }
        }
    }

}