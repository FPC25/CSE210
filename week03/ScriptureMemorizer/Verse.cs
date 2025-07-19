using System;
using System.Collections.Generic;

class Verse
{
    private string _reference, _text;
    private int _verse;

    public int verse { get => _verse; set => _verse = value; }
    public string reference { get => _reference; set => _reference = value; }
    public string text { get => _text; set => _text = value; }

    public Verse()
    {
    }

    public string GetReference()
    {
        return _reference;
    }
    public void SetReference(string reference)
    {
        _reference = reference;
    }

    public string GetText()
    {
        return _text;
    }

    public void SetText(string text)
    {
        _text = text;
    }

    public int GetVerse()
    {
        return _verse;
    }

    public void SetVerse(int verse)
    {
        _verse = verse;
    }
}