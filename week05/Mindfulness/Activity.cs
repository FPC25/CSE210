using System;

class Activity
{
    private int varName;
    public Activity()
    {
    }

    public void StartMessage()
    {

    }

    public void EndMessage()
    {

    }

    public void Countdown(int timeInSeconds)
    {
        string cleanLine;
        string spaces;
        for (int i = timeInSeconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            cleanLine = new String('\b', Utils.CountDigit(i));
            spaces = new String(' ', Utils.CountDigit(i));
            Console.Write($"{cleanLine}{spaces}{cleanLine}");
        }
    }

    public void Spinner(int timeInSeconds)
    {
        List<string> spinner = new List<string> { "|", "/", "-", "\\" };

        Utils.RepeatListString(spinner, timeInSeconds);
    }
}