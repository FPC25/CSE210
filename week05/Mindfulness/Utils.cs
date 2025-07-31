using System;

public static class Utils
{
    //A function to Capitaliza a string
    public static string ToTitleCase(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    // Functions to create a menu from each the user can interact given a list of options

    //returns the option index
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

    //returns the text of the option
    public static string DecisionString(List<string> options)
    {
        return options[Decision(options)];
    }

    // A function to print all the elements in a list in a python style
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

    //Functions to help to create an animation

    //Counts the number of digits of a number
    public static int CountDigit(int number)
    {
        if (number < 0) number = Math.Abs(number);

        return number.ToString().Length;
    }

    //depending the number of characters presented it changes the amount of backspace and space on it
    public static string BuiltCleanTerminalString(int numElements)
    {
        string cleanLine = new String('\b', numElements); 
        string spaces = new String(' ', numElements); 
        return $"{cleanLine}{spaces}{cleanLine}";
    }

    //Make the animation works for a given amount of time
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
            Console.Write(BuiltCleanTerminalString(s.Length));

            i++;

            if (i >= animationList.Count) i = 0;
        }
    }
}