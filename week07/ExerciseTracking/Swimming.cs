using System;

class Swimming : Activity
{
    private const int POOLLENGTH = 50;

    private int _numberOfLaps;
    public Swimming(DateTime date, int length, int numberOfLaps) : base(date, length)
    {
        _numberOfLaps = numberOfLaps;
    }

    public override double GetDistance()
    {
        return _numberOfLaps * POOLLENGTH / 1000;
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