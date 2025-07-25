using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Homework Project.");

        Assignment hw1 = new Assignment("Samuel Bennett", "Multiplication");
        Console.WriteLine(hw1.GetSummary());
        Console.WriteLine();

        MathAssignment hw2 = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        Console.WriteLine(hw2.GetHomeworkList());

        WritingAssignment hw3 = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(hw3.GetWritingInfo());
    }
}