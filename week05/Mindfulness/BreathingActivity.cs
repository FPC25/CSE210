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

        Utils.RepeatListString(breathIn, timeInSeconds);
    }
    
    public static void BreatheOut(int timeInSeconds)
    {
        List<string> breathIn = new List<string> { "O", "o", ".", " " }; 

        Utils.RepeatListString(breathIn, timeInSeconds);
    }
}