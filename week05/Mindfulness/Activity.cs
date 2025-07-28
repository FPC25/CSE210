using System;

class Activity
{
    private string _activityName, _message;
    private int _activityDurationInSeconds;

    public Activity(string name, string message)
    {
        _activityName = name;
        _message = message;
    }

    public int DisplayStartMessage()
    {
        Console.WriteLine($"Welcome to the {_activityName} Activity.\n");
        Console.WriteLine(_message + "\n");
        int time;
        string input;
        do
        {
            Console.Write("For how long, in seconds, would you like to do this session?");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out time));

        return time;
    }

    protected void setTimer(int time)
    {
        _activityDurationInSeconds = time;
    }

    public void DisplayEndMessage()
    {
        Console.Write($"Great Job!!");
        if (_activityName.ToLower().Contains("breathe"))
        {
            Console.WriteLine($" Breathe normally now!");
        }
        Console.WriteLine("\n");
        Spinner(8);
        Console.Clear();
        Console.WriteLine($"You have completed another {_activityDurationInSeconds} seconds of the {_activityName} Activity.");
        Spinner(12);
    }



    public void Countdown(int timeInSeconds)
    {
        int numDigits;
        for (int i = timeInSeconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            numDigits = Utils.CountDigit(i);
            Console.Write(Utils.BuiltCleanTerminalString(numDigits));
        }
    }

    public void Spinner(int timeInSeconds)
    {
        List<string> spinner = new List<string> { "|", "/", "-", "\\" };

        Utils.RepeatListString(spinner, timeInSeconds);
    }
}