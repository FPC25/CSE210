using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

class ScriptureCollection
{
    private List<Book> _books;

    public ScriptureCollection()
    {
        _books = new List<Book>();
    }

    public void LoadFromFile(string filePath)
    {
        try
        {
            string jsonContent = File.ReadAllText(filePath);

            // Parse the JSON - assuming the root has a "books" property
            using JsonDocument document = JsonDocument.Parse(jsonContent);
            JsonElement root = document.RootElement;
            
            if (root.TryGetProperty("books", out JsonElement booksElement))
            {
                foreach (JsonElement bookElement in booksElement.EnumerateArray())
                {
                    Book book = JsonSerializer.Deserialize<Book>(bookElement.GetRawText());
                    _books.Add(book);
                }
            }
            else if (root.TryGetProperty("sections", out JsonElement sectionsElement))
            {
                Book book = new Book();
                if (root.TryGetProperty("book", out JsonElement titleElement))
                {
                    book.SetBook(titleElement.GetString());
                }
                else
                {
                    book.SetBook("Doctrine and Covenants"); // Default name
                }

                List<Chapter> chapters = new List<Chapter>();
                
                foreach (JsonElement sectionElement in sectionsElement.EnumerateArray())
                {   
                    Chapter chapter = new Chapter();

                    if (sectionElement.TryGetProperty("section", out JsonElement sectionNum))
                    {
                        chapter.SetChapter(sectionNum.GetInt32());
                        chapter.SetReference($"D&C {sectionNum.GetInt32()}");
                    }
                    
                    if (sectionElement.TryGetProperty("verses", out JsonElement versesElement))
                    {
                        List<Verse> verses = JsonSerializer.Deserialize<List<Verse>>(versesElement.GetRawText());
                        chapter.SetVerses(verses);
                    }
                    
                    chapters.Add(chapter);
                }
                book.SetChapters(chapters);
                _books.Add(book);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scripture file: {ex.Message}");
        }        
    }

    public Book GetBook(string bookName)
    {
        foreach (Book book in _books)
        {
            if (book.GetBook().Equals(bookName, StringComparison.OrdinalIgnoreCase))
            {
                return book;
            }
        }
        return null;
    }

    public List<Book> GetAllBooks()
    {
        return _books;
    }

    public int GetBookCount()
    {
        return _books.Count;
    }
}
