using System;

class MathAssignment : Assignment
{
    private string _textbookSection, _problems;
    public MathAssignment(string name, string topic, string textbookSection, string problems) : base(name, topic)
    {
        _textbookSection = textbookSection;
        _problems = problems;
    }

    public string GetHomeworkList()
    {
        return $"{this.GetSummary()}\nSection {_textbookSection} Problems {_problems}\n";
    }
}
