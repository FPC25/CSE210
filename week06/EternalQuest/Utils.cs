using System;

/// <summary>
/// The Utils class provides static utility methods for user input, string formatting,
/// menu interaction, and data conversion for the Eternal Quest application.
/// These methods help standardize user prompts, input validation, and data formatting throughout the project.
/// </summary>
public static class Utils
{
    /// <summary>
    /// Converts the first character of a string to uppercase.
    /// </summary>
    /// <param name="s">The string to capitalize.</param>
    /// <returns>The string with the first character in uppercase.</returns>
    public static string ToTitleCase(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    /// <summary>
    /// Displays a numbered menu and prompts the user to select an option by number.
    /// </summary>
    /// <param name="options">List of options to display.</param>
    /// <returns>The zero-based index of the selected option.</returns>
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

    /// <summary>
    /// Displays a menu and returns the text of the selected option.
    /// </summary>
    /// <param name="options">List of options to display.</param>
    /// <returns>The text of the selected option.</returns>
    public static string DecisionString(List<string> options)
    {
        int index = Decision(options);
        return options[index];
    }

    /// <summary>
    /// Prints all elements of a string list in Python-style formatting.
    /// </summary>
    /// <param name="list">The list of strings to print.</param>
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

    /// <summary>
    /// Counts the number of digits in an integer.
    /// </summary>
    /// <param name="number">The integer to count digits for.</param>
    /// <returns>The number of digits.</returns>
    public static int CountDigit(int number)
    {
        if (number < 0) number = Math.Abs(number);

        return number.ToString().Length;
    }

    /// <summary>
    /// Prompts the user for an integer input and validates it.
    /// </summary>
    /// <param name="questionToUser">The prompt to display to the user.</param>
    /// <returns>The integer entered by the user.</returns>
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

    /// <summary>
    /// Prompts the user for year, month, and day, and returns a DateTime object.
    /// </summary>
    /// <param name="yearCall">Prompt for year.</param>
    /// <param name="monthCall">Prompt for month.</param>
    /// <param name="dayCall">Prompt for day.</param>
    /// <returns>A DateTime object representing the entered date.</returns>
    public static DateTime ReadDate(string yearCall, string monthCall, string dayCall)
    {
        int year = Utils.ReadInt(yearCall);
        if (year < 100)
        {
            int currentYear = DateTime.Now.Year % 100;
            int century = (year > currentYear ? 1900 : 2000);
            year += century;
        }
        int day;
        int month;
        do
        {
            month = Utils.ReadInt(monthCall);
        } while (month < 1 || month > 12);
        do
        {
            day = Utils.ReadInt($"{dayCall} (1-{DateTime.DaysInMonth(year, month)}): ");
        } while (day < 1 || day > DateTime.DaysInMonth(year, month));
        
        return new DateTime(year, month, day);
    }

    /// <summary>
    /// Prompts the user to enter dates for a list of ordinances, handling confirmation/baptism logic.
    /// </summary>
    /// <param name="ordinances">List of ordinance names.</param>
    /// <returns>A dictionary mapping ordinance names to their dates.</returns>
    public static Dictionary<string, DateTime> GetOrdinance(List<string> ordinances)
    {
        Dictionary<string, DateTime> ordinancesDict = new Dictionary<string, DateTime>();
        string confirmationDateEqualsBaptism;

        string yearCall, monthCall, dayCall;
        foreach (string ordinance in ordinances)
        {
            yearCall = $"\nPlease enter the year the {ordinance} occurred (e.g., 1995 or 95): ";
            monthCall = $"\nPlease enter the month the {ordinance} occurred? (Enter the number, e.g., 1 for January): ";
            dayCall = $"\nPlease enter the day the {ordinance} occurred?";
            if (ordinance.ToLower() == "confirmation" && ordinances.Contains("baptism"))
            {
                Console.WriteLine("\nIs your confirmation date the same as your baptism date? (yes/no)");
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

    /// <summary>
    /// Prompts the user for a non-empty string input.
    /// </summary>
    /// <param name="question">The prompt to display to the user.</param>
    /// <returns>A non-empty string entered by the user.</returns>
    public static string ValidStringInput(string question)
    {
        string input;
        do
        {
            Console.WriteLine(question);
            input = Console.ReadLine() ?? "";
        } while (input == "");

        return input;
    }
}