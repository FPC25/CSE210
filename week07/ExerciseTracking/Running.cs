using System;

class Running : Activity
{

    private double _distance;

    public Running(DateTime date, int length, double distance) : base(date, length)
    {
        _distance = distance;
    }   

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return 60 / GetPace();
    }

    public override double GetPace()
    {
        return GetLengthInMinutes() / _distance;
    }
}