using System;
using System.Text.RegularExpressions;
class Scripture
{
    private List<Word> _scripture;
    private Reference _reference;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        List<string> list = text.Split(' ').ToList();
        foreach (string item in list)
        {

        }
    }

    private List<string> SplittingText(string text)
    {
    // Split on word boundaries, keeping both words and punctuation
    return Regex.Split(text, @"(\W+)")
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .ToList();
    }
}