using System;
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
}