using System;

class Cycling : Activity
{

    public Cycling(DateTime date, int length) : base(date, length)
    {

    }

    public override double GetDistance()
    {
        return 0.1f;
    }

    public override double GetSpeed()
    {
        return 0.1f;
    }

    public override double GetPace()
    {
        return 0.1f;
    }
}