using System;

class ReflectingActivity : Activity
{
    const string NAME = "Reflection", MESSAGE = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    private readonly List<string>
        _prompts = new List<string>() {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    },
        _questions = new List<string>() {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?",
        // Additional mindfulness-focused questions
        "What emotions did you notice during this experience?",
        "How did your body feel at the time?",
        "What thoughts were present as you acted?",
        "Did you notice any changes in your breathing or heart rate?",
        "How did you respond to challenges or setbacks in the moment?",
        "What strengths did you discover in yourself?",
        "How did this experience affect your perspective on similar situations?",
        "What would you do differently if faced with a similar situation again?",
        "How can you use what you learned to help others?",
        "How can you practice gratitude for this experience?"
    };
    private Random _random = new Random();

    public ReflectingActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        Console.Clear();
        int time = DisplayStartMessage();
        SetTimer(time);
        DisplayGetReady();
        DisplayPrompt();
        DisplayQuestion(time);
        DisplayEndMessage();
    }

    private string GetRandomPrompt()
    {
        // Select a random prompt from the list
        return _prompts[_random.Next(_prompts.Count)];
    }

    private string GetRandomQuestion(List<string> usedQuestionList)
    {
        string question;

        //While the question selected randomly was already used, try again
        do
        {
            question = _questions[_random.Next(_questions.Count)];
        } while (usedQuestionList.Contains(question));
        
        // add the new question to the used list to future reference and return the question
        usedQuestionList.Add(question);
        return question;
    }

    private void DisplayPrompt()
    {
        Console.Clear();
        //Get a random prompt
        string prompt = GetRandomPrompt();

        //Present the prompt formatted as shown
        Console.WriteLine("Considering the following prompt:");
        Console.WriteLine($"--- {prompt} ---\n");

        //create this wait time until the user thing on something and then they press the enter to continue
        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();
    }

    private void DisplayQuestion(int timeInSeconds)
    {
        //Setting the list of to track the used questions and the start and end times 
        List<string> usedQuestions = new List<string>();
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();

        //Beginning the questions

        //Advise the user the questions will begin in n seconds 
        Console.Write("You may begin in: ");
        Countdown(5);
        Console.Clear();

        //Set the start and end times for the activity. 
        startTime = DateTime.Now;
        endTime = startTime.AddSeconds(timeInSeconds);

        //While the time is not finished keep asking questions 
        while (DateTime.Now <= endTime)
        {
            Console.Write($"> {GetRandomQuestion(usedQuestions)}");
            Spinner(10);
            Console.WriteLine();
        }        
    }
}