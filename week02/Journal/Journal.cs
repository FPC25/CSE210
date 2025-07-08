using System;
using System.Text;
using System.IO;   
using System.Text.Json;
using System.Collections.Generic;

class Journal
{
    public string _userName, _journalName, _preferredExtension, _dateFormat;
    public List<Entry> _entries;
    private List<string> _options;

    public Journal(string name, string fileName, string extension, string dateFormat)
    {
        _userName = name;
        _journalName = fileName;
        _preferredExtension = extension;
        _dateFormat = dateFormat;
        _entries = new List<Entry>();
    }

    public bool ShowMenu()
    {
        Console.Clear();
        Console.WriteLine($"Hello {_userName}! Please select an option:");
        _options = new List<string>() { "Load a Journal", "Write a new entry", "Display the Journal", "Save Journal", "Quit" };

        string selectedOption = Utils.Decision(_options);

        switch (selectedOption)
        {
            case "Load a Journal":
                LoadJournalFlow();
                break;
            case "Write a new entry":
                WriteNewEntryFlow();
                break;
            case "Display the Journal":
                DisplayJournal();
                break;
            case "Save Journal":
                SaveJournal();
                break;
            case "Quit":
                return false; // Signal to exit
        }

        return true; // Continue running
    }

    public void LoadJournalFlow()
    {
        Console.Clear();
        Console.WriteLine($"I see {_userName} that you want to load your journal!");

        string fileToLoad = null;
        string defaultFile = $"{_journalName}{_preferredExtension}";

        // If default exists, offer it as option
        if (FindFile(defaultFile))
        {
            Console.WriteLine($"Do you want to load your journal {defaultFile} or choose another file?");
            List<string> options = new List<string>() { $"Load {defaultFile}", "Choose different file" };

            if (Utils.Decision(options).Contains(defaultFile))
            {
                fileToLoad = defaultFile;
            }
        }

        // If no file selected yet, prompt for one
        if (fileToLoad == null)
        {
            fileToLoad = PromptForValidFile();
        }

        _entries = LoadJournal(fileToLoad);
        Console.WriteLine("Journal loaded successfully! Press any key to continue...");
        Console.ReadKey();
    }

    public void WriteNewEntryFlow()
    {
        Console.Clear();
        var promptObg = new PromptGuide("./prompts.json");
        Entry newEntry = new Entry(promptObg.SelectPrompt(), _dateFormat);
        DecisionNewEntry(newEntry);
    }

    private string PromptForValidFile()
    {
        string fileName;
        do
        {
            Console.Write("Please enter the name of your journal file: ");
            fileName = Console.ReadLine();
            
            if (!FindFile(fileName))
            {
                Console.WriteLine($"Sorry, the file {fileName} was not found. Please try again.");
            }
        } while (!FindFile(fileName));
        
        return fileName;
    }

    public string GetFilePath(string fileName)
    {
        string dir = Path.GetDirectoryName(fileName);
        string baseName = Path.GetFileNameWithoutExtension(fileName);
        string originalExt = Path.GetExtension(fileName);
        
        if (string.IsNullOrEmpty(originalExt))
        {
            originalExt = _preferredExtension;
        }

        // Create list without duplicates
        List<string> extensionsToTry = new List<string> { originalExt };
        if (originalExt != _preferredExtension)
        {
            extensionsToTry.Add(_preferredExtension); // Only add if different
        }

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
            PropertyNameCaseInsensitive = true
        };
    }

    public List<Entry> LoadJournal(string fileName)
    {
        try
        {
            string actualPath = GetFilePath(fileName);
            string ext = Path.GetExtension(actualPath).ToLower();

            if (ext == ".json")
            {
                var json = File.ReadAllText(actualPath);
                var entries = JsonSerializer.Deserialize<List<Entry>>(json, GetJsonOptions());

                // Set the date format for all loaded entries
                foreach (var entry in entries)
                {
                    entry._dateFormat = _dateFormat;
                }

                return entries;
            }
            else if (ext == ".csv")
            {
                return LoadCsvJournal(actualPath);
            }
            else
            {
                throw new NotSupportedException($"File format {ext} is not supported.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
            return new List<Entry>();
        }
    }

    public void SaveJournal()
    {
        // Save to file
        string ext = _preferredExtension;
        string defaultFileName = $"{_journalName}{ext}";
        
        Console.WriteLine($"The default file name is {defaultFileName}. Do you want to use it?");
        _options = new List<string>() { "Yes", "No" };
        string entryOption = Utils.Decision(_options);
        string fileName;
        
        if (entryOption == "No")
        {
            Console.Write("What is the new name? ");
            string userInput = Console.ReadLine();
            string userExt = Path.GetExtension(userInput);
            
            if (!string.IsNullOrEmpty(userExt))
            {
                ext = userExt;
                fileName = userInput;  // User provided full name with extension
            }
            else
            {
                fileName = userInput + ext;  // Add preferred extension
            }
        }
        else
        {
            fileName = defaultFileName;  // Use the preference-based default
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
                // CSV Implementation
                var csvLines = new List<string>();
                
                // Add header row
                csvLines.Add("Date,Humor,Prompt,Entry");
                
                // Add each entry as a CSV row
                foreach (var entry in _entries)
                {
                    string date = entry._entryTime.ToString(_dateFormat);
                    string humor = EscapeCsvField(entry._humor);
                    string prompt = EscapeCsvField(entry._prompt);
                    string entryText = EscapeCsvField(entry._entry);
                    
                    csvLines.Add($"{date},{humor},{prompt},{entryText}");
                }
                
                File.WriteAllLines(fileName, csvLines);
                Console.WriteLine($"Journal saved to {fileName} in CSV format. Press any key to continue...");
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
            return;
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
                Console.WriteLine($"\nEntry {_entries.Count - i}:");
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
    }

    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
            return "";

        // If field contains comma, newline, or quotes, wrap in quotes and escape internal quotes
        if (field.Contains(",") || field.Contains("\n") || field.Contains("\""))
        {
            field = field.Replace("\"", "\"\""); // Escape quotes by doubling them
            return $"\"{field}\""; // Wrap in quotes
        }

        return field;
    }

    private List<Entry> LoadCsvJournal(string filePath)
    {
        var entries = new List<Entry>();
        var lines = File.ReadAllLines(filePath);
        
        // Skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            var fields = ParseCsvLine(lines[i]);
            
            if (fields.Length >= 4)
            {
                var entry = new Entry(); // Use parameterless constructor
                if (DateTime.TryParse(fields[0], out DateTime parsedDate))
                {
                    entry._entryTime = parsedDate;
                }
                else
                {
                    entry._entryTime = DateTime.Now; // Fallback
                }
                entry._humor = fields[1];
                entry._prompt = fields[2];
                entry._entry = fields[3];
                entry._dateFormat = _dateFormat;
                
                entries.Add(entry);
            }
        }
        
        return entries;
    }

    private string[] ParseCsvLine(string line)
    {
        // Simple CSV parser - for production, consider using a CSV library
        var fields = new List<string>();
        bool inQuotes = false;
        var currentField = new StringBuilder();
        
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    currentField.Append('"'); // Escaped quote
                    i++; // Skip next quote
                }
                else
                {
                    inQuotes = !inQuotes; // Toggle quote state
                }
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                currentField.Append(c);
            }
        }
        
        fields.Add(currentField.ToString()); // Last field
        return fields.ToArray();
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
            return; // Don't save, just exit
        }
        else if (entryOption == "Save")
        {
            SaveEntry(newEntry);
            return; // Exit after saving
        }
        else if (entryOption == "Edit")
        {
            EditEntry(newEntry);
            return; // EditEntry handles its own flow
        }
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