using System;

class Reference
{
    private string _book;
    private int _chapter, _verse;
    private int? _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public void Display()
    {
        if (_endVerse.HasValue)
        {
            Console.Write($"{_book} {_chapter}:{_verse}-{_endVerse}");
        }
        else
        {
            Console.Write($"{_book} {_chapter}:{_verse}");
        }
    }
}