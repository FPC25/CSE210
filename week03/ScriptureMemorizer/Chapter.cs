using System;
using System.Collections.Generic;

class Chapter
{
    private int _chapter;
    private string _reference;
    private List<Verse> _verses;

    //adding this properties so the JSON parser can be used
    public int chapter { get => _chapter; set => _chapter = value; }
    public string reference { get => _reference; set => _reference = value; }
    public List<Verse> verses { get => _verses; set => _verses = value; }

    public Chapter()
    {
        _verses = new List<Verse>();
    }

    public int GetChapter()
    {
        return _chapter;
    }

    public void SetChapter(int chapter)
    {
        _chapter = chapter;
    }

    public string GetReference()
    {
        return _reference;
    }

    public void SetReference(string reference)
    {
        _reference = reference;
    }

    public List<Verse> GetVerses()
    {
        return _verses;
    }

    public void SetVerses(List<Verse> verses)
    {
        _verses = verses;
    }

    public Verse GetVerse(int verseNumber)
    {
        foreach (Verse verse in _verses)
        {
            if (verse.GetVerse() == verseNumber)
            {
                return verse;
            }
        }
        return null; //Verse not Found
    }

    public int GetVerseCount()
    {
        return _verses.Count;
    }
}