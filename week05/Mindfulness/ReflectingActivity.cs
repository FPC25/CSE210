using System;

class ReflectingActivity : Activity
{
    const string NAME = "Reflection", MESSAGE = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    private List<string>
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
    public ReflectingActivity() : base(NAME, MESSAGE)
    {
    }

    public void Run()
    {
        List<string> usedQuestions = new List<string>();
        int time = DisplayStartMessage();
        DateTime startime = new DateTime();
        startime = DateTime.Now;
        SetTimer(time);
        DisplayPrompt();
        DisplayQuestion(startime);
        DisplayEndMessage();
    }

    private string GetRandomPrompt()
    {
        return "";
    }

    private string GetRandomQuestion(List<string> usedQuestionList)
    {
        return "";
    }

    private void DisplayPrompt()
    {
        string prompt;
        prompt = GetRandomPrompt();

        Console.WriteLine("Considering the following prompt:");
        Console.WriteLine($"--- {prompt} ---\n");

        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();
    }

    private void DisplayQuestion(DateTime time)
    {
        List<string> usedQuestions = new List<string>();
        string question;
        question = GetRandomQuestion(usedQuestions);

        
        Console.WriteLine("Answer the questions: ");
        Console.WriteLine(question);
    }
}