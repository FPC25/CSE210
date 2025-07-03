using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise 4 Project.");

        // Declaring the variables used on the project
        bool isNumeric;
        string input;
        float number, sum = 0, average, large = float.NegativeInfinity, minPos = float.PositiveInfinity;

        List<float> numbers = new List<float>();
        List<float> sorted = new List<float>();

       // Initializing the project and set expectations
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {   
            // Requesting the number to the user and verifying if is numeric or not
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

            //Making sure only values != 0 enter the list
            if (number != 0)
            {
                numbers.Add(number);

            }
        } while (number != 0);

        //Processing the values on the list, summing, selecting largest, smallest positive and sorting
        foreach (float num in numbers)
        {
            sum += num;

            if (num > 0 && num < minPos)
            {
                minPos = num;
            }

            if (num > large)
            {
                large = num;
            }
        }
        // Calculating the average
        average = sum / numbers.Count;

        //Presenting the results
        Console.WriteLine($"The sum is: {sum}\nThe average is: {average}\nThe largest number is: {large}\nThe smallest positive number is: {minPos}");
    }
}