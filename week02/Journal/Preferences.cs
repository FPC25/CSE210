using System.IO;
using System.Text.Json;

class Preferences
{
    public string _filePath, _userName, _dateFormat, _journalExtension;

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

    static private int Decision(List<string> options)
    {
        // Display numbered choices
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        int choice;
        string input;
        // Prompt until user enters a valid number
        do
        {
            Console.Write("Please select an option by number: ");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out choice) || choice < 1 || choice > options.Count);

        // Return zero-based index
        return choice - 1;
    }

    public List<string> CreatePreferencesFile()
    {
        Console.Write("What is your name: ");
        _userName = Console.ReadLine();

        Console.WriteLine($"{_userName}, do you prefer to to use the Default preferences or do you want to choose yourself (Advanced)?");
        List<string> options = new List<string>() { "Default (USA time format and journal format is '.json')", "Customized" };

        if (options[Decision(options)] != "Customized")
        {
            _dateFormat = "mm/dd/yyyy HH:mm";
            _journalExtension = ".json";
        }
        else
        {
            Console.WriteLine($"{_userName}, do you want to use the USA time format (month/day/year) or the standard time format that the rest of the world uses (day/month/year)?");
            options = new List<string> { "USA (default)", "Standard" };

            if (options[Decision(options)] == "Standard")
            {
                _dateFormat = "dd/MM/yyyy HH:mm";
            }
            else
            {
                _dateFormat = "MM/dd/yyyy HH:mm";
            }

            Console.WriteLine($"{_userName}, do you want to use JSON or CSV format to save your journal?");
            options = new List<string> { "JSON (default)", "CSV" };
            if (options[Decision(options)] == "CSV")
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
            _journalExtension
        };

        var opts = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(prefsList, opts);

        File.WriteAllText(_filePath, json);

        return prefsList;
    } 

    private List<string> ReadPreferencesFile(string fileName)
    {
            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<string>>(json);

    }

}