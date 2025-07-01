using System;
using System.Diagnostics.Tracing;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise 3 Project.");

        Random randomGenerator = new Random();

        string response;
        string input;
        do
        {
            bool isNumeric = false;
            int level = -1;

            do
            {
                Console.Write("Choose a number from 1 to 3 to select the difficulty level (default is 2. Medium): \n1. Easy (max value = 50)\n2. Medium (max value = 100)\n3. Hard (max value = 1000)\nLevel: ");
                input = Console.ReadLine();
                isNumeric = int.TryParse(input, out level);
                Console.WriteLine("");
            } while (!isNumeric || level < 1 || level > 3);

            int maxNumber;
            if (level == 1)
            {
                maxNumber = 50;
            }
            else if (level == 3)
            {
                maxNumber = 1000;
            }
            else
            {
                maxNumber = 100;
            }

            int number = randomGenerator.Next(1, maxNumber + 1);
            int guess = -1;
            int numGuesses = 0;
            while (guess != number)
            {
                isNumeric = false;
                do
                {
                    Console.Write("What is your guess? ");
                    input = Console.ReadLine();
                    isNumeric = int.TryParse(input, out guess);
                } while (!isNumeric);

                numGuesses++;
                if (guess > number)
                {
                    Console.WriteLine("Lower");
                }
                else if (guess < number)
                {
                    Console.WriteLine("Higher");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {numGuesses} guesses!\n");
                }
            }

            do
            {
                Console.WriteLine("Do you want to continue (y/n)?");
                input = Console.ReadLine();
                Console.WriteLine("");
            } while (input == "");

            response = input.ToLower()[0].ToString();
            if (response != "y" && response != "n")
            {
                response = "n";
            }

        } while (response == "y");
    }
}