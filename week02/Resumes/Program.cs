using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Resumes Project.");

        List<Job> jobs = new List<Job>();

        jobs.Add(new Job("Microsoft", "Software Engineer", 2019, 2022));

        jobs.Add(new Job("Apple", "Manager", 2022, 2023));

        Resume employeeResume = new Resume("Allison Rose", jobs);

        employeeResume.Display();
    }
}