using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

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

    public Scripture SelectScriptureManually()
    {
        // Step 1: Choose collection
        Console.WriteLine("\nChoose a Scripture Collection:");
        List<string> titles = GetCollectionTitles();
        int collectionChoice = Utils.Decision(titles);
        string selectedFile = _availableFiles[collectionChoice];
        
        Console.WriteLine($"Selected: {titles[collectionChoice]}");
        
        // Step 2: Load collection and choose book
        ScriptureCollection collection = LoadCollection(selectedFile);
        var books = collection.GetAllBooks();
        
        if (books.Count == 0)
        {
            Console.WriteLine("No books found in this collection!");
            return null;
        }
        
        Console.WriteLine($"\nBooks in {titles[collectionChoice]}:");
        List<string> bookNames = new List<string>();
        foreach (var book in books)
        {
            bookNames.Add(book.GetBook());
        }
        
        int bookChoice = Utils.Decision(bookNames);
        Book selectedBook = books[bookChoice];
        
        Console.WriteLine($"Selected book: {selectedBook.GetBook()}");
        
        // Step 3: Choose chapter
        var chapters = selectedBook.GetChapters();
        
        if (chapters.Count == 0)
        {
            Console.WriteLine("No chapters found in this book!");
            return null;
        }
        
        Console.WriteLine($"\nChapters in {selectedBook.GetBook()}:");
        List<string> chapterNumbers = new List<string>();
        foreach (var chapter in chapters)
        {
            chapterNumbers.Add($"Chapter {chapter.GetChapter()}");
        }
        
        int chapterChoice = Utils.Decision(chapterNumbers);
        Chapter selectedChapter = chapters[chapterChoice];
        
        Console.WriteLine($"Selected: Chapter {selectedChapter.GetChapter()}");
        
        // Step 4: Choose verse or verse range
        return SelectVerseFromChapter(selectedChapter, selectedBook.GetBook());
    }

    // Add this new method to handle verse selection:
    private Scripture SelectVerseFromChapter(Chapter chapter, string bookName)
    {
        var verses = chapter.GetVerses();
        
        if (verses.Count == 0)
        {
            Console.WriteLine("No verses found in this chapter!");
            return null;
        }
        
        Console.WriteLine($"\nChapter {chapter.GetChapter()} has {verses.Count} verses");
        Console.WriteLine("How would you like to select verses?");
        
        List<string> options = new List<string> { "Single verse", "Range of verses" };
        int choice = Utils.Decision(options);
        
        if (choice == 0) // Single verse
        {
            return SelectSingleVerse(verses, chapter, bookName);
        }
        else // Range of verses
        {
            return SelectVerseRange(verses, chapter, bookName);
        }
    }

    private Scripture SelectSingleVerse(List<Verse> verses, Chapter chapter, string bookName)
    {
        int verseNum;
        do
        {
            Console.Write($"Enter verse number (1-{verses.Count}): ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out verseNum) && verseNum >= 1 && verseNum <= verses.Count)
            {
                break; // Valid input, exit loop
            }
            else
            {
                Console.WriteLine($"Invalid verse number! Please enter a number between 1 and {verses.Count}.");
            }
        } while (true);
        
        var selectedVerse = verses[verseNum - 1];
        Reference reference = new Reference(bookName, chapter.GetChapter(), selectedVerse.GetVerse());
        return new Scripture(reference, selectedVerse.GetText());
    }

    private Scripture SelectVerseRange(List<Verse> verses, Chapter chapter, string bookName)
    {
        int startVerse;
        do
        {
            Console.Write($"Enter start verse (1-{verses.Count}): ");
            string startInput = Console.ReadLine();
            
            if (int.TryParse(startInput, out startVerse) && startVerse >= 1 && startVerse <= verses.Count)
            {
                break; // Valid input, exit loop
            }
            else
            {
                Console.WriteLine($"Invalid start verse! Please enter a number between 1 and {verses.Count}.");
            }
        } while (true);
        
        int endVerse;
        do
        {
            Console.Write($"Enter end verse ({startVerse}-{verses.Count}): ");
            string endInput = Console.ReadLine();
            
            if (int.TryParse(endInput, out endVerse) && endVerse >= startVerse && endVerse <= verses.Count)
            {
                break; // Valid input, exit loop
            }
            else
            {
                Console.WriteLine($"Invalid end verse! Please enter a number between {startVerse} and {verses.Count}.");
            }
        } while (true);
        
        // Get the selected verses
        var selectedVerses = new List<Verse>();
        for (int i = startVerse - 1; i < endVerse; i++)
        {
            selectedVerses.Add(verses[i]);
        }
        
        // Combine the text
        string combinedText = "";
        for (int i = 0; i < selectedVerses.Count; i++)
        {
            if (i > 0) combinedText += " ";
            combinedText += selectedVerses[i].GetText();
        }
        
        // Create reference
        Reference reference = new Reference(bookName, chapter.GetChapter(), startVerse, endVerse);
        return new Scripture(reference, combinedText);
    }

    public Scripture SearchByReference()
    {
        Console.Write("\nEnter scripture reference (e.g., 'John 3:16' or '1 Nephi 3:7-8'): ");
        string referenceInput = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(referenceInput))
        {
            Console.WriteLine("Invalid reference entered.");
            return null;
        }
        
        Console.WriteLine($"Searching for: {referenceInput}");
        Console.WriteLine("(Search functionality will be implemented next...)");
        return null;
    }
    
}
