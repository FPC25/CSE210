using System;

class Swimming : Activity
{
    private const string NAME = "Swimming";
    
    private const int POOL_LENGTH = 50;

    private int _numberOfLaps;

    public Swimming(DateTime date, int length, int numberOfLaps) : base(date, length)
    {
        _numberOfLaps = numberOfLaps;
        SetName(NAME);
    }

    public override double GetDistance()
    {
        return _numberOfLaps * POOL_LENGTH / 1000;
    }

    public override double GetPace()
    {
        return GetLengthInMinutes() / GetDistance();
    }

    public override double GetSpeed()
    {
        return  60 / GetPace();
    }
}