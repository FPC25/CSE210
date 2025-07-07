using System;
using System.Text;
using System.Text.Json.Serialization;

class Entry
{
    public string _prompt, _humor, _entry, _dateFormat;
    public DateTime _entryTime;

    // JSON serialization properties
    [JsonPropertyName("date")]
    public string Date 
    { 
        get => _entryTime.ToString(_dateFormat ?? "MM/dd/yyyy HH:mm");
        set => _entryTime = DateTime.Parse(value);
    }

    [JsonPropertyName("humor")]
    public string Humor 
    {
        get => _humor;
        set => _humor = value;
    }

    [JsonPropertyName("prompt")]
    public string Prompt 
    {
        get => _prompt;
        set => _prompt = value;
    }

    [JsonPropertyName("entry")]
    public string EntryText 
    {
        get => _entry;
        set => _entry = value;
    }

    public Entry()
    {
        // Empty constructor for deserialization
    }

    public Entry(string prompt, string dateFormat)
    {
        _prompt = prompt;
        _entryTime = DateTime.Now;
        _dateFormat = dateFormat;
        _humor = AskHumor();
        _entry = MakeEntry(prompt);
    }

    public string AskHumor()
    {
        Console.Write("Please state your humor while making this entry: ");
        return Console.ReadLine().CapitalizeFirst();
    }

    public string MakeEntry(string prompt)
    {
        Console.WriteLine(prompt);
        string endMessage = "# Type 'End log' or '\\EL' on its own line to finish your entry. #";
        string box_lines = new string('#', endMessage.Length);
        Console.WriteLine($"\n{box_lines}");
        Console.WriteLine($"{endMessage}");
        Console.WriteLine($"{box_lines}\n");

        var builder = new StringBuilder();
        string line;
        while (true)
        {
            line = Console.ReadLine();
            if (line.Equals("End log", StringComparison.OrdinalIgnoreCase) || line.Equals(@"\EL", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            builder.AppendLine(line);
        }
        
        return builder.ToString();
    }

    public void Display()
    {
        string when = _entryTime.ToString(_dateFormat);
        string display = $"\nEntry Time: {when}\nHumor: {_humor}\nPrompt Question: {_prompt}\nEntry:\n{_entry}\n";
        Console.WriteLine(display);
    }
}