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
        int index = Decision(options);
        return options[index];
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

    //Counts the number of digits of a number
    public static int CountDigit(int number)
    {
        if (number < 0) number = Math.Abs(number);

        return number.ToString().Length;
    }

    public static int ReadInt(string questionToUser)
    {
        //setting variables
        string input;
        int number;

        //while the input cannot be convert to integer continue to prompt
        do
        {
            Console.WriteLine(questionToUser);
            input = Console.ReadLine();

        } while (!int.TryParse(input, out number));

        return number;
    }

    public static DateTime ReadDate(string yearCall, string monthCall, string dayCall)
    {
        int year = Utils.ReadInt(yearCall);
        if (year < 100)
        {
            int currentYear = DateTime.Now.Year % 100;
            int century = (year > currentYear ? 1900 : 2000);
            year += century;
        }
        int month = Utils.ReadInt(monthCall);
        int day = Utils.ReadInt($"{dayCall} (1-{DateTime.DaysInMonth(year, month)}): ");

        return new DateTime(year, month, day);
    }

    public static Dictionary<string, DateTime> GetOrdinance(List<string> ordinances)
    {
        Dictionary<string, DateTime> ordinancesDict = new Dictionary<string, DateTime>();
        string confirmationDateEqualsBaptism;

        string yearCall, monthCall, dayCall;
        foreach (string ordinance in ordinances)
        {
            yearCall = $"Please enter the year the {ordinance} occurred (e.g., 1995 or 95): ";
            monthCall = $"Please enter the month the {ordinance} occurred? (Enter the number, e.g., 1 for January): ";
            dayCall = $"Please enter the day the {ordinance} occurred?";
            if (ordinance.ToLower() == "confirmation" && ordinances.Contains("baptism"))
            {
                Console.WriteLine("Is your confirmation date the same as your baptism date? (yes/no)");
                confirmationDateEqualsBaptism = DecisionString(new List<string>() { "Yes", "No" });

                if (confirmationDateEqualsBaptism == "Yes")
                {
                    // Use baptism date for confirmation
                    if (ordinancesDict.ContainsKey("baptism"))
                    {
                        ordinancesDict["confirmation"] = ordinancesDict["baptism"];
                    }
                    else
                    {
                        Console.WriteLine("Baptism date not found. Please enter confirmation date manually.");
                        ordinancesDict["confirmation"] = ReadDate(yearCall, monthCall, dayCall);
                    }
                    continue;
                }
            }
            ordinancesDict[ordinance] = ReadDate(yearCall, monthCall, dayCall);
        }

        return ordinancesDict;
    }

    public static string ValidStringInput(string question)
    {
        string input;
        do
        {
            Console.WriteLine(question);
            input = Console.ReadLine() ?? "";
        } while (input != "");

        return input;
    }
}