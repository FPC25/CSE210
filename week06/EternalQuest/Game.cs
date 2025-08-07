using System;

class Game
{
    public Game()
    {
    }

    private Profile Tutorial()
    {
        string input, name, genderPrompt;
        DateTime birthday;
        bool male = false;
        List<string> ordinancesList = new List<string>() { "baptism", "confirmation" }, genderOptions;

        Console.WriteLine("Welcome to Eternal Quest!\n\nThis 'game' is way to turn our responsibilities as members of the Church of Jesus Christ of the Latter Days Saint more fun simulating an RPG game using us as characters. Let's begin creating a 'character sheet' for you!");

        Console.WriteLine("What is your name?");
        name = Console.ReadLine();

        birthday = ReadDate($"Hello {name}! Please enter your birth year (e.g., 1995 or 95): ", "Which month were you born in? (Enter the number, e.g., 1 for January)", "Which day of the month were you born?");

        int age = DateTime.Now.Year - birthday.Year;
        if (DateTime.Now < birthday.AddYears(age))
        {
            age--;
        }

        if (age < 18)
        {
            genderPrompt = "Are you a boy or a girl?";
            genderOptions = new List<string>() { "Boy", "Girl" };
        }
        else
        {
            genderPrompt = "Are you a man or a woman?";
            genderOptions = new List<string>() { "Man", "Woman" };
        }

        Console.WriteLine(genderPrompt);
        input = Utils.DecisionString(genderOptions);

        male = input.Equals("Man", StringComparison.OrdinalIgnoreCase) || input.Equals("Boy", StringComparison.OrdinalIgnoreCase);
        
        Dictionary<string, DateTime> ordinances = GetOrdinance(ordinancesList);

        return new Profile(name, birthday, age, male, ordinances);
    }

    private Dictionary<string, DateTime> GetOrdinance(List<string> ordinances)
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
                confirmationDateEqualsBaptism = Utils.DecisionString(new List<string>() { "Yes", "No" });

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
    
    private DateTime ReadDate(string yearCall, string monthCall, string dayCall)
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
    
    public void InitialMenu()
    {
        const string
            NEW = "New Game",
            LOAD = "Load Game",
            QUIT = "Quit";

        List<string> options = new List<string>()
            {
                NEW, LOAD, QUIT
            };

        string selectedOption;

        do
        {
            Console.WriteLine("Welcome to Eternal Quest:");
            selectedOption = Utils.DecisionString(options);
            switch (selectedOption)
            {
                case NEW:
                    Profile player = Tutorial();
                    GameMenu();
                    break;

                case LOAD:
                    Console.WriteLine("Still in development");
                    break;

                case QUIT:
                    break;
            }
        } while (selectedOption != QUIT);

    }

    public void GameMenu()
    {
        const string
            ACTIVE = "Show Active Quests",
            RECORD = "Record Quest Completion",
            CUSTOM = "Add a Custom Quest",
            SAVE = "Save Progress",
            PROFILE = "Access Player Info",
            QUIT = "Quit";

        List<string> options = new List<string>()
        {
            ACTIVE, RECORD, CUSTOM, SAVE, PROFILE, QUIT
        };

        string selectedOption;

        do
        {
            Console.WriteLine("Select one option:");
            selectedOption = Utils.DecisionString(options);
            switch (selectedOption)
            {
                case ACTIVE:
                    Console.WriteLine("Still in development");
                    break;
                
                case RECORD:
                    Console.WriteLine("Still in development");
                    break;
                
                case CUSTOM:
                    Console.WriteLine("Still in development");
                    break;

                case SAVE:
                    Console.WriteLine("Still in development");
                    break;

                case PROFILE:
                    Console.WriteLine("Still in development");
                    break;

                case QUIT:
                    break;
            }
        } while (selectedOption != QUIT);
    }
}