using System;
using System.Text.RegularExpressions;
class Word
{
    private string _text;
    private bool _isHidden = false;

    public Word(string text)
    {
        _text = text;
    }

    public void HideWord()
    {
        if (!this.IsHidden() && !Regex.IsMatch(_text, @"^\p{P}$"))
        {
            _text = new string('_', _text.Length);
            this.Hide();
        }
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    private void Hide()
    {
        _isHidden = true;
    }

    public string GetWord()
    {
        return _text;
    }

    public void Display()
    {
        Console.Write(_text);
    }
}