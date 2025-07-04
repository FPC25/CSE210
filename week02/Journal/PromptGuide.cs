using System;
using System.Text.Json;

class PromptGuide
{
    public string _promptsFilePath;
    public List<string> _prompts = new List<string>();

    public PromptGuide(string pathToFile)
    {
        _promptsFilePath = pathToFile;
        _prompts = ReadPromptsFile();
    }

    private List<string> ReadPromptsFile()
    {
        //var infer the type of data from the right side of the expression
        var json = File.ReadAllText(_promptsFilePath);
        return JsonSerializer.Deserialize<List<string>>(json);
    }

    public string SelectPrompt()
    {
        var random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}