using System;

class WritingAssignment : Assignment
{
    private string _title;
    public WritingAssignment(string name, string topic, string title) : base(name, topic)
    {
        _title = title;
    }

    public string GetWritingInfo()
    {
        return $"{this.GetSummary()}\n{_title} by {this.GetStudentName()}\n";
    }
}