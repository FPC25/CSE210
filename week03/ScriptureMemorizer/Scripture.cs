using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

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
        return Regex.Split(text, @"(\p{P}|\s+)")
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
        sb.Append(_reference.GetReference());
        sb.Append(" ");

        for (int i = 0; i < _scripture.Count; i++)
        {
            Word currentWord = _scripture[i];
            //Console.WriteLine(currentWord.GetWord());
            sb.Append(currentWord.GetWord());
            
            if (i < _scripture.Count - 1)
            {
                Word nextWord = _scripture[i + 1];

                if (!Regex.IsMatch(nextWord.GetWord(), @"^\p{P}$"))
                {
                    sb.Append(" ");
                }
            }
        }

        Console.WriteLine(sb.ToString());
    }

    public void HideRandomWord()
    {
        Random random = new Random();

        List<Word> availableWords = _scripture
            .Where(word => !Regex.IsMatch(word.GetWord(), @"^\p{P}$") && !word.IsHidden())
            .ToList();

        if (availableWords.Count > 0)
        {
            int randomIndex = random.Next(availableWords.Count);
            availableWords[randomIndex].HideWord();
        }
    }

    public bool IsCompletelyHidden()
    {
        return _scripture
            .Where(word => !Regex.IsMatch(word.GetWord(), @"^\p{P}$")) // Filter out punctuation)
            .All(word => word.IsHidden()); // Check if all remaining words are hidden
    }
}