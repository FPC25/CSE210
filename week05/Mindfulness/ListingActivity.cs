using System;

class ListingActivity : Activity
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
    private Random _random = new Random();

    public ListingActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        Console.Clear();
        int time = DisplayStartMessage();
        SetTimer(time);
        DisplayGetReady();
        DisplayPrompt();
        GetListFromUser(time);
        DisplayEndMessage();
    }

    private string GetRandomPrompt()
    {
        // Select a random prompt from the list
        return _prompts[_random.Next(_prompts.Count)];
    }

    private void DisplayPrompt()
    {
        Console.Clear();
        
        //Present the prompt formatted as shown
        Console.WriteLine("List as many responses you can to the following prompt:");
        Console.WriteLine($"--- {GetRandomPrompt()} ---\n");
    }

    private void SetCount(int count)
    {
        _count = count;
    }

    private void GetListFromUser(int timeInSeconds)
    {
        //Setting the list of to track the used questions and the start and end times 
        List<string> userInput = new List<string>();
        string input;
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();

        //Beginning the questions

        //Advise the user the questions will begin in n seconds 
        Console.Write("You may begin in: ");
        Countdown(5);
        Console.WriteLine();

        //Set the start and end times for the activity. 
        startTime = DateTime.Now;
        endTime = startTime.AddSeconds(timeInSeconds);

        //While the time is not finished keep asking questions 
        while (DateTime.Now <= endTime)
        {
            Console.Write("> ");
            input = Console.ReadLine();
            userInput.Add(input);
        }
        SetCount(userInput.Count);
        Console.WriteLine($"You listed {_count} itens!");
    }
}