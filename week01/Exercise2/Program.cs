using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise2 Project.");

        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();

        float grade = int.Parse(input);
        string letter;
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        string sign = "";
        if (letter != "A" || letter != "F")
        {
            if (grade % 10 > 6)
            {
                sign = "+";
            }
            else if (grade % 10 < 3)
            {
                sign = "-";
            }
        }

        if (grade >= 70)
        {
            Console.WriteLine($"Congratulations! You passed with a grade of {letter}{sign}.");
        }
        else
        {
            Console.WriteLine($"Unfortunately, you failed with a grade of {letter}{sign}.");
        }

        Console.WriteLine("Thank you for using the grade calculator!");
        Console.WriteLine("Goodbye!");
    }
}