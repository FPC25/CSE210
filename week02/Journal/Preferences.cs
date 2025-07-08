using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

class Preferences
{
    private string _filePath;
    public string _userName, _dateFormat, _journalName, _journalExtension;

    public Preferences(string filePath)
    {
        _filePath = filePath;

        List<string> prefs = LoadPreferences();
        _userName = prefs[0];
        _dateFormat = prefs[1];
        _journalName = prefs[2];
        _journalExtension = prefs[3];
    }

    public List<string> CreatePreferencesFile()
    {
        Console.Write("What is your name: ");
        _userName = Console.ReadLine();

        Console.WriteLine($"{_userName}, do you prefer to to use the Default preferences or do you want to choose yourself (Advanced)?");
        List<string> options = new List<string>() { $"Default (USA time format, {_userName}_journal is the default name for the journal file, journal format is '.json')", "Customized" };

        if (Utils.Decision(options) != "Customized")
        {
            _dateFormat = "MM/dd/yyyy HH:mm";
            _journalName = $"{_userName}_journal";
            _journalExtension = ".json";
        }
        else
        {
            // Customizing Date format
            Console.WriteLine($"{_userName}, do you want to use the USA time format (month/day/year) or the standard time format that the rest of the world uses (day/month/year)?");
            options = new List<string> { "USA (default)", "Standard" };

            if (Utils.Decision(options) == "Standard")
            {
                _dateFormat = "dd/MM/yyyy HH:mm";
            }
            else
            {
                _dateFormat = "MM/dd/yyyy HH:mm";
            }

            // Customizing Journal name
            Console.WriteLine($"{_userName}, do you want to use the default '{_userName}_journal' name to save your journal, or do you want to customize it?");
            options = new List<string> { "Default", "Customized" };
            if (Utils.Decision(options) == "Default")
            {
                _journalName = $"{_userName}_journal";
            }
            else
            {
                List<string> yesNo = new List<string> { "Yes", "No" };
                do
                {
                    Console.Write("Enter the new name to your journal: ");
                    string input = Console.ReadLine();
                    Console.WriteLine($"Perfect! Do you confirm that {input} is the new name to your journal?");

                    if (Utils.Decision(yesNo) == "Yes")
                    {
                        _journalName = input;
                        break;
                    }
                } while (true);
            }

            // Customizing the Journal format
            Console.WriteLine($"{_userName}, do you want to use JSON or CSV format to save your journal?");
            options = new List<string> { "JSON (default)", "CSV" };
            if (Utils.Decision(options) == "CSV")
            {
                _journalExtension = ".csv";
            }
            else
            {
                _journalExtension = ".json";
            }
        }

        List<string> prefsList = new List<string>
        {
            _userName,
            _dateFormat,
            _journalName,
            _journalExtension
        };

        var opts = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(prefsList, opts);

        File.WriteAllText(_filePath, json);

        Console.Clear();

        return prefsList;
    }

    public List<string> ReadPreferencesFile(string fileName)
    {
        try
        {
            var json = File.ReadAllText(fileName);
            var prefs = JsonSerializer.Deserialize<List<string>>(json);
            
            // Validate that we have all 4 preferences
            if (prefs != null && prefs.Count >= 4)
            {
                return prefs;
            }
            else
            {
                Console.WriteLine("Invalid preferences file. Creating new one...");
                return CreatePreferencesFile();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading preferences: {ex.Message}. Creating new file...");
            return CreatePreferencesFile();
        }
    }

    public List<string> LoadPreferences()
    {
        string dir = Path.GetDirectoryName(_filePath);
        string baseName = Path.GetFileName(_filePath);
        var matches = Directory.GetFiles(dir, baseName);
        
        if (matches.Length == 0)
        {
            // No preferences file found, create one
            return CreatePreferencesFile();
        }
        else
        {
            // Preferences file exists, read it
            return ReadPreferencesFile(matches[0]);
        }
    }

}