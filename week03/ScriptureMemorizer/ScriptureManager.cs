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
        string referenceInput;
        do
        {
            Console.Write("\nEnter scripture reference (e.g., 'John 3:16' or '1 Nephi 3:5-6'): ");
            referenceInput = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(referenceInput))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid reference entered. Please try again.");
            }
        } while (true);
        
        // Parse the reference
        var parsedRef = ParseReference(referenceInput);
        if (parsedRef == null)
        {
            Console.WriteLine("Could not parse the reference. Please check the format and try again.");
            return null;
        }
        
        Console.WriteLine($"Searching for: {referenceInput}");
        
        // Find the right collection based on book name
        string targetCollection = FindCollectionByBookName(parsedRef.Value.bookName);
        if (targetCollection == null)
        {
            Console.WriteLine("Book not found in any collection.");
            return null;
        }
        
        // Search in the specific collection
        try
        {
            ScriptureCollection collection = LoadCollection(targetCollection);
            Scripture found = SearchInCollection(collection, parsedRef.Value);
            if (found != null)
            {
                Console.WriteLine($"Found in {GetTitleFromFile(targetCollection)}");
                return found;
            }
            else
            {
                Console.WriteLine("Scripture reference not found.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching: {ex.Message}");
            return null;
        }
    }
    
    private (string bookName, int chapter, List<int> verses)? ParseReference(string reference)
    {
        try
        {
            // Split into words: "1 Nephi 3:5-6" -> ["1", "Nephi", "3:5-6"]
            string[] parts = reference.Trim().Split(' ');
            if (parts.Length < 2) return null;
            
            // Last part contains chapter:verse(s)
            string chapterVersePart = parts[parts.Length - 1];
            
            // Everything before is the book name
            string bookName = string.Join(" ", parts.Take(parts.Length - 1));
            
            // Parse chapter:verse - "3:5-6" -> chapter=3, verses=[5,6]
            if (!chapterVersePart.Contains(':')) return null;
            
            string[] chapterVerse = chapterVersePart.Split(':');
            if (chapterVerse.Length != 2) return null;
            
            if (!int.TryParse(chapterVerse[0], out int chapter)) return null;
            
            string versePart = chapterVerse[1];
            List<int> verses = new List<int>();
            
            if (versePart.Contains('-'))
            {
                // Range: "5-6" -> [5, 6]
                string[] verseRange = versePart.Split('-');
                if (verseRange.Length != 2) return null;
                
                if (!int.TryParse(verseRange[0], out int startVerse) || 
                    !int.TryParse(verseRange[1], out int endVerse)) return null;
                
                for (int i = startVerse; i <= endVerse; i++)
                {
                    verses.Add(i);
                }
            }
            else
            {
                // Single verse: "5" -> [5]
                if (!int.TryParse(versePart, out int singleVerse)) return null;
                verses.Add(singleVerse);
            }
            
            return (bookName, chapter, verses);
        }
        catch
        {
            return null;
        }
    }

    private string FindCollectionByBookName(string bookName)
    {
        string lowerBookName = bookName.ToLower();
        
        // Define book keywords for each collection
        List<string> bookOfMormonKeywords = new List<string> { "nephi", "jacob", "enos", "jarom", "omni", 
                                        "words of mormon", "mosiah", "alma", "helaman", "mormon", "ether", 
                                        "moroni" };
        
        List<string> oldTestamentKeywords = new List<string> { "genesis", "exodus", "leviticus", "numbers", 
                                        "deuteronomy", "joshua", "judges", "ruth", "samuel", "kings", "chronicles",
                                        "ezra", "nehemiah", "esther", "job", "psalms", "proverbs", "ecclesiastes", 
                                        "song", "isaiah", "jeremiah", "lamentations", "ezekiel", "daniel", "hosea", 
                                        "joel", "amos", "obadiah", "jonah", "micah", "nahum", "habakkuk", "zephaniah", 
                                        "haggai", "zechariah", "malachi" };
        
        List<string> newTestamentKeywords = new List<string> { "matthew", "mark", "luke", "john", "acts", "romans", 
                                        "corinthians", "galatians", "ephesians", "philippians", "colossians", 
                                        "thessalonians", "timothy", "titus", "philemon", "hebrews", "james", 
                                        "peter", "jude", "revelation" };
        
        List<string> doctrineKeywords = new List<string> { "d&c", "doctrine", "covenants" };
        
        List<string> pearlKeywords = new List<string> { "moses", "abraham", "joseph smith-matthew", "joseph smith-history", "articles of faith" };
        
        // Check each collection
        foreach (string keyword in bookOfMormonKeywords)
        {
            if (lowerBookName.Contains(keyword))
            {
                return "book-of-mormon";
            }
        }
        
        foreach (string keyword in oldTestamentKeywords)
        {
            if (lowerBookName.Contains(keyword))
            {
                return "old-testament";
            }
        }
        
        foreach (string keyword in newTestamentKeywords)
        {
            if (lowerBookName.Contains(keyword))
            {
                return "new-testament";
            }
        }
        
        foreach (string keyword in doctrineKeywords)
        {
            if (lowerBookName.Contains(keyword))
            {
                return "doctrine-and-covenants";
            }
        }
        
        foreach (string keyword in pearlKeywords)
        {
            if (lowerBookName.Contains(keyword))
            {
                return "pearl-of-great-price";
            }
        }
        
        return null; // Book not found
    }

    private Scripture SearchInCollection(ScriptureCollection collection, (string bookName, int chapter, List<int> verses) parsedRef)
    {
        List<Book> books = collection.GetAllBooks();
        
        // Find the book (partial matching)
        Book foundBook = null;
        string searchBook = parsedRef.bookName.ToLower();

        foreach (Book book in books)
        {
            string bookTitle = book.GetBook().ToLower();
            
            // Special case for D&C - match "d&c" with "doctrine and covenants"
            if (searchBook.Contains("d&c") || (bookTitle.Contains("doctrine") && bookTitle.Contains("covenants")))
            {
                foundBook = book;
                break;
            }
            else if (bookTitle.Contains(searchBook) || searchBook.Contains(bookTitle))
            {
                foundBook = book;
                break;
            }
        }
        
        if (foundBook == null) return null;
        
        // Find the chapter
        Chapter foundChapter = foundBook.GetChapter(parsedRef.chapter);
        if (foundChapter == null) return null;
        
        // Get verses and find matching references
        var allVerses = foundChapter.GetVerses();
        List<string> texts = new List<string>();
        
        foreach (int verseNum in parsedRef.verses)
        {
            // Look for verse by reference string
            string targetReference = $"{foundBook.GetBook()} {parsedRef.chapter}:{verseNum}";
            
            Verse foundVerse = null;
            foreach (var verse in allVerses)
            {
                if (verse.GetReference() != null && verse.GetReference().Equals(targetReference, StringComparison.OrdinalIgnoreCase))
                {
                    foundVerse = verse;
                    break;
                }
                // Fallback: match by verse number
                else if (verse.GetVerse() == verseNum)
                {
                    foundVerse = verse;
                    break;
                }
            }
            
            if (foundVerse != null && !string.IsNullOrEmpty(foundVerse.GetText()))
            {
                texts.Add(foundVerse.GetText());
            }
            else
            {
                Console.WriteLine($"Warning: Verse {verseNum} not found or has no text");
            }
        }
        
        if (texts.Count == 0) return null;
        
        // Combine texts
        string combinedText = string.Join(" ", texts);
        
        // Create reference
        Reference reference;
        if (parsedRef.verses.Count == 1)
        {
            reference = new Reference(foundBook.GetBook(), parsedRef.chapter, parsedRef.verses[0]);
        }
        else
        {
            reference = new Reference(foundBook.GetBook(), parsedRef.chapter, parsedRef.verses[0], parsedRef.verses[parsedRef.verses.Count - 1]);
        }
        
        return new Scripture(reference, combinedText);
    }
}
