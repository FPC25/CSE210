using System;
using System.Collections.Generic;

class Book
{
    private string _book;
    private List<Chapter> _chapters;

    public string book { get => _book; set => _book = value; }
    public List<Chapter> chapters { get => _chapters; set => _chapters = value; }

    public Book()
    {
        _chapters = new List<Chapter>();
    }

    public string GetBook()
    {
        return _book;
    }

    public void SetBook(string book)
    {
        _book = book;
    }

    public List<Chapter> GetChapters()
    {
        return _chapters;
    }

    public void SetChapters(List<Chapter> chapters)
    {
        _chapters = chapters;
    }

    public Chapter GetChapter(int chapterNumber)
    {
        foreach (Chapter chapter in _chapters)
        {
            if (chapter.GetChapter() == chapterNumber)
            {
                return chapter;
            }
        }
        return null; // Chapter not found
    }

    public int GetChapterCount()
    {
        return _chapters.Count;
    }
}