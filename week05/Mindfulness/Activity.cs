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

    //a method to display a start message, based on the name of the activity and request the time it should take
    public int DisplayStartMessage()
    {
        //Display basic messages of welcoming and description of the activity
        Console.WriteLine($"Welcome to the {_activityName} Activity.\n");
        Console.WriteLine(_message + "\n");

        //setting some variables of time and input
        int time;
        string input;

        //requesting the user to enter the how long each activity will take, but added some safe guards in order to guarantee that the value entered can be converted to int, otherwise request keep requesting
        do
        {
            Console.Write("For how long, in seconds, would you like to do this session?");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out time));

        return time;
    }

    //setter to the _activityDurationInSeconds variable
    protected void SetTimer(int time)
    {
        _activityDurationInSeconds = time;
    }

    //a method to display the activity end message
    public void DisplayEndMessage()
    {
        //printing the basic message
        Console.Write($"Great Job!!");
        //if it is a breathing activity, make sure the user to breathe normally again.
        if (_activityName.ToLower().Contains("breath"))
        {
            Console.Write($" Breathe normally now!");
        }
        Console.WriteLine("\n");
        //show the spinner animation and clear terminal 
        Spinner(8);
        Console.Clear();
        //show a new message informing the last activity and how long it took
        Console.WriteLine($"You have completed another {_activityDurationInSeconds} seconds of the {_activityName} Activity.");
        //another spinner animation
        Spinner(12);
    }

    //countdown animation
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

    //spinner animation
    public void Spinner(int timeInSeconds)
    {
        List<string> spinner = new List<string> { "|", "/", "-", "\\" };

        Utils.RepeatListString(spinner, timeInSeconds);
    }

    //ellipsis (... symbol) animation
    public void Ellipsis(int timeInSeconds)
    {
        var ellipsis = new List<string> { ".", ".", "." };

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(timeInSeconds);

        int i = 0;

        while (DateTime.Now < endTime)
        {
            string s = ellipsis[i];

            Console.Write(s);
            Thread.Sleep(1000);

            i++;

            if (i >= ellipsis.Count)
            {
                i = 0;
                Console.Write(Utils.BuiltCleanTerminalString(ellipsis.Count));
            }
        }
    }
}