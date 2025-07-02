using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise 4 Project.");

        List<float> numbers = new List<float>();
        float number, sum = 0, average, large = 0;
        bool isNumeric;
        string input;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {
            isNumeric = false;
            do
            {
                Console.Write("Enter a number: ");
                input = Console.ReadLine();
                isNumeric = float.TryParse(input, out number);

                if (!isNumeric)
                {
                    Console.WriteLine($"Sorry, but {input} is not a number.");
                }
            } while (!isNumeric);

            if (number != 0)
            {
                numbers.Add(number);

            }
        } while (number != 0);

        foreach (float num in numbers)
        {
            sum += num;
            if (num > large)
            {
                large = num;
            }
        }
        average = sum / numbers.Count;
        Console.WriteLine($"The sum is: {sum}\nThe average is: {average}\nThe largest number is: {large}");
    }
}