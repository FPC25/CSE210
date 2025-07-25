using System;
using System.Linq;
class Video
{
    private int _lengthSec;
    private string _author, _title;
    private List<Comment> _comments;
    public Video(string title, string author, int length, List<Comment> comments = null)
    {
        _title = title;
        _author = author;
        _lengthSec = length;
        _comments = comments ?? new List<Comment>();
    }

    public int CountComment()
    {

        return _comments.Count();
    }

    public (int minutes, int seconds) SecToMin()
    {
        //this method returns a tuple with the minutes and remainder seconds 
        return (minutes: _lengthSec / 60, seconds: _lengthSec % 60);
    }

    public (int hours, int minutes, int seconds) MinToHour()
    {
        //This method returns a tuple the returns the hours and the minutes that still don't form a hour;
        var time = this.SecToMin();
        return (hours: time.minutes / 60,
                minutes: time.minutes % 60, 
                seconds: time.seconds);
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetAuthor()
    {
        return _author;
    }

    public int GetLengthInSec()
    {
        return _lengthSec;
    }

    public string GetFormattedLength()
    {
        var time = this.MinToHour();
        if (time.hours == 0)
        {
            return $"{time.minutes}:{time.seconds}";
        }

        return $"{time.hours}:{time.minutes}:{time.seconds}";
    }

}