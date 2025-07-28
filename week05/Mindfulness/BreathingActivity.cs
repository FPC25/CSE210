using System;

class BreathingActivity : Activity
{
    const string NAME = "Breathing";
    const string MESSAGE = "This activity will help you relax by walking your through breathing routine. Clear your mind and focus on your breathing.";
    const int STEP_TIME = 4; //s
    const int NUM_STEPS = 4; // per cycle
    public BreathingActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        //initiate the program running it, displaying the messages and getting for how long it should run
        int time = DisplayStartMessage();
        //get the next value that creates a complete breathing cycle and set it;
        time = NextFullCycle(time, STEP_TIME, NUM_STEPS);
        SetTimer(time);


        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(time);

        while (DateTime.Now < endTime)
        {
            BreatheIn(STEP_TIME);
            Hold(STEP_TIME);
            BreatheOut(STEP_TIME);
            Hold(STEP_TIME);
        }
    }

    private static int NextFullCycle(int ogTime, int stepTime, int numSteps)
    {
        int totalCycleTime = stepTime * numSteps;
        int remainder = ogTime % totalCycleTime;
        if (remainder == 0)
            return ogTime;
        return ogTime + (totalCycleTime - remainder);
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
        Console.Write($"Hold your breath for {timeInSeconds} seconds:");
        Countdown(timeInSeconds);
    }
}