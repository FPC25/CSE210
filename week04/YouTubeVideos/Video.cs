using System;

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
        _comments = comments ?? new List<Comment>(); //if the comments were given then simply pass the value, if not given it is has the default null value, so create a new empty list (?? -> this operator does this)
    }

    // Getters for the variables
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

    public List<Comment> GetComments()
    {
        return _comments;
    }

    //Other methods
    public int CountComment()
    {
        //COunt the amount of comments in the list
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

    public string GetFormattedLength()
    {
        //Format the time to simply be printed
        var time = this.MinToHour(); //get the time from the method MinToHour calling this class in itself with 'this' keyword
        if (time.hours == 0) //if the hour is zero then simply display the min:sec as in YouTube
        {
            return $"{time.minutes}:{time.seconds}";
        }
        //otherwise print the whole thing
        return $"{time.hours}:{time.minutes}:{time.seconds}";
    }
}