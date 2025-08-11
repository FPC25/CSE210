using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Hello World! This is the ExerciseTracking Project.\n");

        DateTime date = new DateTime(2022, 11, 3);
        List<Activity> activities = new List<Activity>()
        {
            new Running(date, 30, 4.8f),
            new Cycling(date.AddDays(1), 30, 9.7f),
            new Swimming(date.AddDays(2), 60, 50),
            new Running(date.AddDays(3), 45, 7.2f),
            new Cycling(date.AddDays(4), 60, 20.5f),
            new Swimming(date.AddDays(5), 30, 25),
            new Running(date.AddDays(6), 20, 3.0f),
            new Cycling(date.AddDays(7), 90, 35.0f),
            new Swimming(date.AddDays(8), 45, 35)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}