using System;

abstract class Activity
{
    private string _name;

    private DateTime _date;

    private int _lengthInMinutes;

    public Activity(DateTime date, int length)
    {
        _date = date;
        _lengthInMinutes = length;
    }

    public int GetLengthInMinutes() => _lengthInMinutes;

    public void SetName(string name) => _name = name;

    public abstract double GetDistance();

    public abstract double GetSpeed();

    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{_date} - {_name} ({_lengthInMinutes} min): Distance: {GetDistance()} km, Speed: {GetSpeed()} km/h, Pace: {GetPace()} min per km";
    }
}