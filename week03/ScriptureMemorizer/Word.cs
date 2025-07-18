using System;

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
        if (!this.IsHidden())
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