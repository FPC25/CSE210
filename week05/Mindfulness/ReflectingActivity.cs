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
    }, _questions = new List<string>() {
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

    }
    

}