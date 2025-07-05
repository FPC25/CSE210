using System;
using System.Text.Json;

class Preferences
{
    private string _filePath;
    public string _userName, _dateFormat, _journalExtension;

    public Preferences(string filePath)
    {
        _filePath = filePath;

        string dir = Path.GetDirectoryName(filePath);
        string baseName = Path.GetFileName(filePath);
        var matches = Directory.GetFiles(dir, baseName);
        if (matches.Length == 0)
        {
            List<string> prefs = CreatePreferencesFile();
            _userName = prefs[0];
            _dateFormat = prefs[1];
            _journalExtension = prefs[2];
        }
        else
        {
            List<string> prefs = ReadPreferencesFile(matches[0]);
            _userName = prefs[0];
            _dateFormat = prefs[1];
            _journalExtension = prefs[2];
        }
    }

    public List<string> CreatePreferencesFile()
    {
        Console.Write("What is your name: ");
        _userName = Console.ReadLine();

        Console.WriteLine($"{_userName}, do you prefer to to use the Default preferences or do you want to choose yourself (Advanced)?");
        List<string> options = new List<string>() { "Default (USA time format and journal format is '.json')", "Customized" };

        if (Utils.Decision(options) != "Customized")
        {
            _dateFormat = "mm/dd/yyyy HH:mm";
            _journalExtension = ".json";
        }
        else
        {
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

            Console.WriteLine($"{_userName}, do you want to use JSON, CSV or TXT format to save your journal?");
            options = new List<string> { "JSON (default)", "CSV", "TXT" };
            if (Utils.Decision(options) == "CSV")
            {
                _journalExtension = ".csv";
            }
            else if (Utils.Decision(options) == "TXT")
            {
                _journalExtension = ".txt";
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
            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<string>>(json);

    }

}