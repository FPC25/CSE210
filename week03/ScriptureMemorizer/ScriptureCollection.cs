using System;

class ScriptureCollection
{
    private List<Book> _books;

    public ScriptureCollection()
    {
        _books = new List<Book>();
    }

    public void LoadFromFile(string filePath)
    {

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
