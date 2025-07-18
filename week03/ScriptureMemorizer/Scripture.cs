using System;
using System.Text;
using System.Text.RegularExpressions;

class Scripture
{
    private List<Word> _scripture;
    private Reference _reference;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _scripture = ConvertingStringToWord(SplittingText(text));
    }

    static private List<string> SplittingText(string text)
    {
        // Split on word boundaries, keeping both words and punctuation
        return Regex.Split(text, @"(\W+)")
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();
    }

    static private List<Word> ConvertingStringToWord(List<string> initialList)
    {
        List<Word> finalList = new List<Word> { };
        foreach (string item in initialList)
        {
            finalList.Add(new Word(item));
        }
        return finalList;
    }

    public void Display()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{_reference.GetReference} ");

        for (int i = 0; i < _scripture.Count; i++)
        {
            Word currentWord = _scripture[i];
            sb.Append(currentWord.GetWord());
            
            if (i < _scripture.Count - 1)
            {
                Word nextWord = _scripture[i + 1];

                if (!Regex.IsMatch(nextWord.GetWord(), @"^\W+$"))
                {
                    sb.Append(" ");
                }
            }
        }

        Console.WriteLine(sb.ToString());
    }

    public void HideRandomWord()
    {

    }

    public bool IsCompletelyHidden()
    {
        return false;
    }
}