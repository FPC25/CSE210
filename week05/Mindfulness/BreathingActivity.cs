using System;

class BreathingActivity : Activity
{
    private int varName;
    public BreathingActivity()
    {
    }

    public void Run()
    {

    }

    public static void BreatheIn(int timeInSeconds)
    {
        List<string> breathIn = new List<string> { " ", ".", "o", "O" };

        Console.Write($"Breathe in for {timeInSeconds} seconds: ");
        Utils.RepeatListString(breathIn, timeInSeconds);
    }

    public static void BreatheOut(int timeInSeconds)
    {
        List<string> breathIn = new List<string> { "O", "o", ".", " " };

        Console.Write($"Breathe out for {timeInSeconds} seconds: ");
        Utils.RepeatListString(breathIn, timeInSeconds);
    }

    public void Hold(int timeInSeconds)
    {
        Console.Write($"Hold for {timeInSeconds} seconds:");
        Countdown(timeInSeconds);
    }
}