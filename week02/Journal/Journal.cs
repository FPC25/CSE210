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
                Console.Write($"What is the name of your file with the extension {_preferredExtension}?");
                fileName = Console.ReadLine();
            } while (!FindFile(fileName));

            _entries = LoadJournal(fileName);
            Menu();
        }
        else if (selectedOption == "Write a new entry")
        {
            var promptObg = new PromptGuide("./prompts.json");
            Entry newEntry = new Entry(promptObg.SelectPrompt(), _dateFormat);
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
                SaveEntry();

            }
            else if (entryOption == "Edit")
            {
                EditEntry();
            }
            Menu();

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

    public bool FindFile(string fileName)
    {
        var dir = Path.GetDirectoryName(fileName);
        var baseName = Path.GetFileNameWithoutExtension(fileName);
        var originalExt = Path.GetExtension(fileName);
        
        // Try original first, then preferred
        var extensionsToTry = new List<string> { originalExt, _preferredExtension };

        foreach (string ext in extensionsToTry)
        {
            var fullPath = Path.Combine(dir, baseName + ext);
            if (File.Exists(fullPath))
                return true;
        }

        return false;
    }

    public List<Entry> LoadJournal(string fileName)
    {

    }

    public Entry SaveEntry()
    {

    }

    public Entry EditEntry()
    {

    }

    public void SaveJournal()
    {

    }

    public void DisplayJournal()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

}