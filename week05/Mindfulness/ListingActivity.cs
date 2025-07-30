using System;

class ListeningActivity : Activity
{
    const string NAME = "Listing", MESSAGE = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";

    private List<string> _prompts = new List<string>() {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?",
        // New questions for mindfulness
        "What are three things you are grateful for today?",
        "What are some achievements you are proud of?",
        "What activities help you feel relaxed?",
        "What are some places where you feel at peace?",
        "What are some acts of kindness you have witnessed or performed recently?",
        "What are some goals you are working towards?",
        "What are some lessons you have learned this year?",
        "Who has made a positive impact on your life?",
        "What are some things you enjoy about your daily routine?",
        "What are some challenges you have overcome?"
    };
    private int _count;

    public ListeningActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        SetTimer(DisplayStartMessage());
        GetRandomPrompt();
        Countdown(10);
        GetListFromUser();
        DisplayEndMessage();
    }

    private void GetRandomPrompt()
    {

    }

    private List<string> GetListFromUser()
    {
        return new List<string>();
    }

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