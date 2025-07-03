using System;

class Program
{
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    static string PromptUserName()
    {
        Console.Write("What is your name? ");
        return Console.ReadLine();
    }

    static int PromptUserNumber()
    {
        string input;
        bool isNumeric;
        int number;
        do
        {
            Console.Write("What is your Favorite number? ");
            input = Console.ReadLine();
            isNumeric = int.TryParse(input, out number);

            if (!isNumeric)
            {
                Console.WriteLine($"Sorry, but {input} is not a integer number.");
            }
        } while (!isNumeric);

        return number;
    }

    static int SquareNumber(int number)
    {
        return number * number;
    }

    static void DisplayResult(string name, int squareNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squareNumber}");
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise5 Project.");

        DisplayWelcome();
        DisplayResult(PromptUserName(), SquareNumber(PromptUserNumber()));
        
    }
}