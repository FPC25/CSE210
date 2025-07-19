using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class ScriptureManager
{
    private string _path;
    private List<string> _availableFiles;

    public ScriptureManager(string pathToFolder)
    {
        _path = pathToFolder;
        LoadAvailableFiles();
    }

    private void LoadAvailableFiles()
    {
        _availableFiles = new List<string>();
        
        if (Directory.Exists(_path))
        {
            var jsonFiles = Directory.GetFiles(_path, "*.json");
            foreach (var file in jsonFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                _availableFiles.Add(fileName);
            }
        }
    }

    public List<string> GetAvailableFiles()
    {
        return new List<string>(_availableFiles);
    }

    public List<string> GetCollectionTitles()
    {
        List<string> titles = new List<string>();
        
        foreach (string fileName in _availableFiles)
        {
            string title = GetTitleFromFile(fileName);
            titles.Add(title);
        }
        
        return titles;
    }

    private string GetTitleFromFile(string fileName)
    {
        try
        {
            string filePath = Path.Combine(_path, $"{fileName}.json");
            string jsonContent = File.ReadAllText(filePath);
            
            using JsonDocument document = JsonDocument.Parse(jsonContent);
            JsonElement root = document.RootElement;
            
            // Try to get the title from JSON
            if (root.TryGetProperty("title", out JsonElement titleElement))
            {
                return titleElement.GetString();
            }
            
            // Fallback to formatted filename
            return Utils.ToTitleCase(fileName.Replace("-", " "));
        }
        catch
        {
            // If anything fails, just format the filename
            return Utils.ToTitleCase(fileName.Replace("-", " "));
        }
    }
    
    public ScriptureCollection LoadCollection(string fileName)
    {
        string filePath = Path.Combine(_path, $"{fileName}.json");
        ScriptureCollection collection = new ScriptureCollection();
        collection.LoadFromFile(filePath);
        return collection;
    }
}
