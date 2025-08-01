using System;

class BreathingActivity : Activity
{
    //constants to run this program 
    const string NAME = "Breathing", MESSAGE = "This activity will help you relax by walking your through breathing routine. Clear your mind and focus on your breathing.";
    const int STEP_TIME = 4; //s
    const int NUM_STEPS = 4; // per cycle

    //An empty constructor
    public BreathingActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        Console.Clear();
        //initiate the program running it, displaying the messages and getting for how long it should run
        int time = DisplayStartMessage();
        //get the next value that creates a complete breathing cycle and set it;
        time = NextFullCycle(time, STEP_TIME, NUM_STEPS);
        SetTimer(time);
        DisplayGetReady();

        //setting the initial moment and the end time to the routine
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(time);

        //running the breathing routine
        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            BreatheIn(STEP_TIME);
            Hold(STEP_TIME);
            BreatheOut(STEP_TIME);
            Hold(STEP_TIME);
        }

        DisplayEndMessage();
    }

    //Since a full routine takes a time that is defined by the num of steps x the time each step takes, and the user may not enter a time that completes the full cycle we must find the next grater amount of seconds that completes a full cycle, in order to finish the breathing activity cycle
    private int NextFullCycle(int ogTime, int stepTime, int numSteps)
    {
        //calculate how many seconds each full cycle takes
        int totalCycleTime = stepTime * numSteps;
        //Calculating the remainder for the entered time, if takes a full cycle or if takes less
        int remainder = ogTime % totalCycleTime;
        //verifying if the value entered by the user already takes a number of full cycles 
        if (remainder == 0)
            return ogTime;
        //if takes more than a full cycle, calculate the next amount of seconds to complete one more cycle 
        return ogTime + (totalCycleTime - remainder);
    }

    //Functions to show the animations and messages to, breath in, breath out and to hold your breathe 
    public void BreatheIn(int timeInSeconds)
    {
        List<string> breathIn = new List<string> { " ", ".", "o", "O" };

        Console.Write($"Breathe in for {timeInSeconds} seconds: ");
        Utils.RepeatListString(breathIn, timeInSeconds);
        Console.WriteLine();
    }

    public void BreatheOut(int timeInSeconds)
    {
        List<string> breathIn = new List<string> { "O", "o", ".", " " };

        Console.Write($"Breathe out for {timeInSeconds} seconds: ");
        Utils.RepeatListString(breathIn, timeInSeconds);
        Console.WriteLine();
    }

    public void Hold(int timeInSeconds)
    {
        Console.Write($"Hold your breath for {timeInSeconds} seconds: ");
        Countdown(timeInSeconds);
        Console.WriteLine();
    }
}