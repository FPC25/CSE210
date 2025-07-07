using System;
using System.Text.Json;

class Journal
{
    public string _userName, _preferredExtension, _dateFormat;
    public List<Entry> _entries;
    private List<string> _options;

    public Journal(string name, string extension, string dateFormat)
    {
        _userName = name;
        _preferredExtension = extension;
        _dateFormat = dateFormat;
        _entries = new List<Entry>();
    }

    public void Menu()
    {
        Console.Clear();
        Console.WriteLine($"Hello {_userName}! Please Select one of the following options: ");
        _options = new List<string>() { "Load a Journal", "Write a new entry", "Display the Journal", "Save Journal", "Quit" };

        string selectedOption = Utils.Decision(_options);

        if (selectedOption == "Load a Journal")
        {
            Console.WriteLine($"I see {_userName} that you want to load your journal!");
            string fileName;
            do
            {
                Console.Write($"Please enter the name of your journal file (with extension {_preferredExtension}): ");
                fileName = Console.ReadLine();
                if (!FindFile(fileName))
                {
                    Console.WriteLine("Sorry, the file was not found. Please try again.");
                }
            } while (!FindFile(fileName));

            _entries = LoadJournal(fileName);
            Menu();
        }
        else if (selectedOption == "Write a new entry")
        {
            var promptObg = new PromptGuide("./prompts.json");
            Entry newEntry = new Entry(promptObg.SelectPrompt(), _dateFormat);
            DecisionNewEntry(newEntry);
        }
        else if (selectedOption == "Display the Journal")
        {
            DisplayJournal();
            Menu();
        }
        else if (selectedOption == "Save Journal")
        {
            SaveJournal();
            Menu();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    public void DecisionNewEntry(Entry newEntry)
    {
        Console.Clear();
        Console.WriteLine("Current entry:");
        newEntry.Display();

        Console.WriteLine($"What do you want to do with your new Journal Entry?");
        _options = new List<string>() { "Save", "Edit", "Discard" };
        string entryOption = Utils.Decision(_options);
        if (entryOption == "Discard")
        {
            Menu();
        }
        if (entryOption == "Save")
        {
            SaveEntry(newEntry);
        }
        else if (entryOption == "Edit")
        {
            EditEntry(newEntry);
        }
        Menu();
    }

    public string GetFilePath(string fileName)
    {
        string dir = Path.GetDirectoryName(fileName);
        string baseName = Path.GetFileNameWithoutExtension(fileName);
        string originalExt = Path.GetExtension(fileName);
        
        // Try original first, then preferred
        List<string> extensionsToTry = new List<string> { originalExt, _preferredExtension };

        foreach (string ext in extensionsToTry)
        {
            string fullPath = Path.Combine(dir, baseName + ext);
            if (File.Exists(fullPath))
                return fullPath;
        }

        return null; //No file found
    }

    public bool FindFile(string fileName)
    {
        return GetFilePath(fileName) != null;
    }

    private JsonSerializerOptions GetJsonOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        };
    }

    public List<Entry> LoadJournal(string fileName)
    {
        string actualPath = GetFilePath(fileName);
        var json = File.ReadAllText(actualPath);
        return JsonSerializer.Deserialize<List<Entry>>(json, GetJsonOptions());
    }

    public void SaveJournal()
    {
        // Save to file
        string ext = _preferredExtension;
        Console.WriteLine($"The default file name is {_userName}_journal{ext}. Do you want to change it?");
        _options = new List<string>() { "Yes", "No" };
        string entryOption = Utils.Decision(_options);
        string fileName;
        if (entryOption == "Yes")
        {
            Console.Write("What is the new name you would prefer? ");
            string userInput = Console.ReadLine();
            string ext_new = Path.GetExtension(userInput);
            if (!string.IsNullOrEmpty(ext_new))
            {
                ext = ext_new;
            }
            fileName = userInput + ext;
        }
        else
        {
            fileName = $"{_userName}_journal{ext}";
        }

        try
        {
            if (ext == ".json")
            {
                string json = JsonSerializer.Serialize(_entries, GetJsonOptions());
                File.WriteAllText(fileName, json);
                Console.WriteLine($"Journal saved to {fileName}. Press any key to continue...");
            }
            else if (ext == ".csv")
            {
                Console.WriteLine("CSV format not implemented yet.");
            }
            else if (ext == ".txt")
            {
                Console.WriteLine("TXT format not implemented yet.");
            }
            else
            {
                Console.WriteLine("Unsupported file format.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}. Press any key to continue...");
        }
        Console.ReadKey();                 
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries in your journal yet.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Menu();
        }

        int entriesPerPage = 5;
        int currentPage = 0;
        int totalPages = (_entries.Count + entriesPerPage - 1) / entriesPerPage;

        while (currentPage < totalPages)
        {
            Console.Clear();
            Console.WriteLine($"Journal Entries - Page {currentPage + 1} of {totalPages}");
            Console.WriteLine(new string('=', 50));

            int startIndex = currentPage * entriesPerPage;
            int endIndex = Math.Min(startIndex + entriesPerPage, _entries.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                Console.WriteLine($"\nEntry {i + 1}:");
                _entries[i].Display();
                Console.WriteLine(new string('-', 30));
            }

            if (currentPage < totalPages - 1)
            {
                Console.WriteLine("\nPress any key for next page, 'q' to quit...");
                var key = Console.ReadKey();
                if (key.KeyChar == 'q' || key.KeyChar == 'Q')
                    break;
                currentPage++;
            }
            else
            {
                Console.WriteLine("\nEnd of journal. Press any key to continue...");
                Console.ReadKey();
                break;
            }
        }
        Menu();
    }  

    public void SaveEntry(Entry newEntry)
    {
        _entries.Insert(0, newEntry);
    }

    public void EditEntry(Entry entryToEdit)
    {        
        Console.WriteLine("\nWhat would you like to edit?");
        List<string> editOptions = new List<string> { "Entry text", "Humor", "Nothing (keep as it is)" };
        
        string choice = Utils.Decision(editOptions);
        
        if (choice.Contains("Entry"))
        {
            Console.WriteLine($"Current entry: {entryToEdit._entry}");
            Console.WriteLine("Enter new text (type END on a new line when finished):");
            entryToEdit._entry = entryToEdit.MakeEntry(entryToEdit._prompt);
        }
        else if (choice.Contains("Humor"))
        {
            Console.WriteLine($"Current humor: {entryToEdit._humor}");
            Console.Write("Enter new humor: ");
            entryToEdit._humor = Console.ReadLine();
        }
        
        DecisionNewEntry(entryToEdit);
    }
}