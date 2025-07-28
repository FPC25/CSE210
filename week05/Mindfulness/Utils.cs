using System;

public static class Utils
{
    public static string ToTitleCase(this string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static int Decision(List<string> options)
    {
        // Display numbered choices
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        int choice;
        string input;
        // Prompt until user enters a valid number
        do
        {
            Console.Write("Please select an option by number: ");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out choice) || choice < 1 || choice > options.Count);

        // Return zero-based index
        return choice - 1;
    }

    public static string DecisionString(List<string> options)
    {
        return options[Decision(options)];
    }

    public static void PrintList(List<string> list)
    {
        Console.Write("[");
        for (int i = 0; i < list.Count; i++)
        {
            Console.Write($"\"{list[i]}\"");
            if (i < list.Count - 1)
                Console.Write(", ");
        }
        Console.WriteLine("]");
    }

    public static int CountDigit(int number)
    {
        if (number < 0) number = Math.Abs(number);

        return number.ToString().Length;
    }

    public static void RepeatListString(List<string> animationList, int timeInSeconds)
    {
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(timeInSeconds);

        int i = 0;

        while (DateTime.Now < endTime)
        {
            string s = animationList[i];

            Console.Write(s);
            Thread.Sleep(1000);
            String backspace = new String('\b', s.Length);
            String space = new String(' ', s.Length);
            Console.Write($"{backspace}{space}{backspace}");

            i++;

            if (i >= animationList.Count) i = 0;
        }
    }
}