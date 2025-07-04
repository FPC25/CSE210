using System;
using System.Text;

class Entry
{
    public string _prompt, _humor, _entry;
    public DateTime _entryTime;
    private string _dateFormat;

    public Entry(string prompt, string dateFormat)
    {
        _prompt = prompt;
        _entryTime = DateTime.Now;
        _dateFormat = dateFormat;
        _humor = AskHumor();
        _entry = MakeEntry();
    }

    public string AskHumor()
    {
        Console.Write("Please state your humor while making this entry: ");
        return Console.ReadLine().CapitalizeFirst();
    }

    public string MakeEntry()
    {
        Console.WriteLine(_prompt);
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
            if (line.Equals("End Log", StringComparison.OrdinalIgnoreCase) || line.Equals(@"\EL", StringComparison.OrdinalIgnoreCase))
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