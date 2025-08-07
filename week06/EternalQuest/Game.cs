using System;

class Game
{
    public Game()
    {
    }

    public Profile Tutorial()
    {
        Profile player;
        string input, name;
        int yearBorn, dayBorn, monthBorn;
        DateTime birthday;
        bool male = false;
        List<string> ordinances = new List<string>() { "baptism", "confirmation" };


        Console.WriteLine("Welcome to Eternal Quest!\n\nThis 'game' is way to turn our responsibilities as members of the Church of Jesus Christ of the Latter Days Saint more fun simulating an RPG game using us as characters. Let's begin creating a 'character sheet' for you!");

        Console.WriteLine("What is your name?");
        name = Console.ReadLine();

        yearBorn = Utils.ReadInt($"Hello {name}! Please enter your birth year (e.g., 1995 or 95): ");
        // Normalize yearBorn to four digits (handles 2-digit input intelligently)
        if (yearBorn < 100)
        {
            int currentYear = DateTime.Now.Year % 100;
            int century = (yearBorn > currentYear ? 1900 : 2000);
            yearBorn += century;
        }

        monthBorn = Utils.ReadInt("Which month were you born in? (Enter the number, e.g., 1 for January) ");
        dayBorn = Utils.ReadInt($"Which day of the month were you born? (1-{DateTime.DaysInMonth(yearBorn, monthBorn)}): ");
        birthday = new DateTime(yearBorn, monthBorn, dayBorn);

        int age = DateTime.Now.Year - birthday.Year;
        if (DateTime.Now < birthday.AddYears(age))
        {
            age--;
        }

        List<string> gender;
        if (age < 18)
        {
            Console.WriteLine("Are you a boy or a girl?");
            gender = new List<string>() { "boy", "girl" };

        }
        else
        {
            Console.WriteLine("Are you a man or a woman?");
            gender = new List<string>() { "man", "woman" };
        }
        input = Utils.DecisionString(gender);
        if (input == "man" || input == "boy")
        {
            male = true;
        }



        return player;
    }

    private Dictionary<string, DateTime> GetOrdinance(List<string> ordinances)
    {
        Dictionary<string, DateTime> ordinancesDict = new Dictionary<string, DateTime>();
        DateTime ordinanceDate;
        int year, month, day;
        string input, confirmationDateEqualsBaptism;

        foreach (string ordinance in ordinances)
        {
            if (ordinance.ToLower() == "confirmation" && ordinances[0] != "confirmation")
            {
                Console.WriteLine("Is your confirmation date the same as your baptism date? (yes/no)");
                confirmationDateEqualsBaptism = Utils.DecisionString(new List<string>() { "Yes", "No" });

                if (confirmationDateEqualsBaptism == "Yes")
                {
                    ordinancesDict[ordinance] = ordinanceDate;
                    break;
                }
            }

            year = Utils.ReadInt($"Please enter the year the {ordinance} occurred (e.g., 1995 or 95): ");
            // Normalize yearBorn to four digits (handles 2-digit input intelligently)
            if (year < 100)
            {
                int currentYear = DateTime.Now.Year % 100;
                int century = (year > currentYear ? 1900 : 2000);
                year += century;
            }
            month = Utils.ReadInt("Which month were you born in? (Enter the number, e.g., 1 for January) ");
            day = Utils.ReadInt($"Which day of the month were you born? (1-{DateTime.DaysInMonth(year, month)}): ");



        }
        return ordinancesDict;
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