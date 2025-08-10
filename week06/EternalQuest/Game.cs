using System;

/// <summary>
/// The Game class manages the main flow and logic of the Eternal Quest application.
/// It handles user interaction, menu navigation, and player profile creation.
/// </summary>
class Game
{
    private const string
        SIMPLE = "simple",
        CHECKLIST = "checklist",
        ETERNAL = "eternal";

    /// <summary>
    /// Stores the current player's profile.
    /// </summary>
    private Profile _player;
    private QuestManager _questManager;

    /// <summary>
    /// Initializes a new instance of the Game class.
    /// </summary>
    public Game()
    {
    }

    /// <summary>
    /// Guides the user through the tutorial and character creation process,
    /// collecting basic information and ordinances to instantiate a Profile.
    /// </summary>
    /// <returns>A new Profile object with user data.</returns>
    private Profile Tutorial()
    {
        string input, name, genderPrompt;
        DateTime birthday;
        bool male = false;
        List<string> ordinancesList = new List<string>() { "baptism", "confirmation" }, genderOptions;

        Console.WriteLine("Welcome to Eternal Quest!\n\nThis 'game' is way to turn our responsibilities as members of the Church of Jesus Christ of the Latter Days Saint more fun simulating an RPG game using us as characters. Let's begin creating a 'character sheet' for you!\n");

        Console.WriteLine("What is your name?");
        name = Console.ReadLine();

        birthday = Utils.ReadDate($"\nHello {name}! Please enter your birth year (e.g., 1995 or 95): ", "Which month were you born in? (Enter the number, e.g., 1 for January)", "Which day of the month were you born?");

        int age = DateTime.Now.Year - birthday.Year;
        if (DateTime.Now < birthday.AddYears(age))
        {
            age--;
        }

        if (age < 18)
        {
            genderPrompt = "\nAre you a boy or a girl?";
            genderOptions = new List<string>() { "Boy", "Girl" };
        }
        else
        {
            genderPrompt = "\nAre you a man or a woman?";
            genderOptions = new List<string>() { "Man", "Woman" };
        }

        Console.WriteLine(genderPrompt);
        input = Utils.DecisionString(genderOptions);

        male = input.Equals("Man", StringComparison.OrdinalIgnoreCase) || input.Equals("Boy", StringComparison.OrdinalIgnoreCase);

        Dictionary<string, DateTime> ordinances = Utils.GetOrdinance(ordinancesList);

        return new Profile(name, birthday, age, male, ordinances);
    }

    /// <summary>
    /// Starts the main game loop, displaying the initial menu and handling user choices.
    /// </summary>
    public void Run()
    {
        InitialMenu();
    }

    /// <summary>
    /// Displays the initial menu (New Game, Load Game, Quit) and processes user selection.
    /// </summary>
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
                    _player = Tutorial();
                    _questManager = new QuestManager(_player);
                    _questManager.PopulatePlayerQuests();
                    _questManager.ActivateQuest();
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

    /// <summary>
    /// Displays the main game menu (Active Quests, Record Completion, Custom Quest, Save, Profile, Quit)
    /// and processes user selection.
    /// </summary>
    public void GameMenu()
    {
        const string
            PROFILE = "Access Player Info",
            CUSTOM = "Add a Custom Quest",
            ACTIVE = "Show Active Quests",
            RECORD = "Record Quest Completion",
            COMPLETED = "See Completed Quests",
            SAVE = "Save Progress",
            QUIT = "Quit";

        List<string> options = new List<string>()
        {
            PROFILE, CUSTOM, ACTIVE, RECORD, COMPLETED, SAVE, QUIT
        };

        string selectedOption;

        do
        {
            Console.WriteLine("Select one option:");
            selectedOption = Utils.DecisionString(options);
            switch (selectedOption)
            {
                case PROFILE:
                    _player.DisplayPlayerInfo();
                    CheckAutoCompleteQuests(); // Check after profile updates
                    _questManager.ActivateQuest(); // Check for newly available quests
                    break;

                case CUSTOM:
                    Console.WriteLine("Still in development");
                    break;

                case ACTIVE:
                    _questManager.DisplayActiveQuests();
                    break;

                case RECORD:
                    Console.WriteLine("Do you want to record an event from which category? ");
                    string category = Utils.DecisionString(new List<string>() { SIMPLE, CHECKLIST, ETERNAL }).ToLower();
                    Console.WriteLine("Which quest do you want to record a change?");
                    string questName = Utils.DecisionString(_questManager.GetActiveQuestNamesPerCategory(category));
                    _questManager.CheckSpecificQuest(category, questName);
                    _questManager.ActivateQuest();
                    break;

                case COMPLETED:
                    _questManager.DisplayCompletedQuests();
                    break;

                case SAVE:
                    Console.WriteLine("Still in development");
                    break;

                case QUIT:
                    break;
            }
        } while (selectedOption != QUIT);
    }
    
    /// <summary>
    /// Automatically checks and completes all auto-check quests based on current profile status.
    /// </summary>
    private void CheckAutoCompleteQuests()
    {
        foreach (Quest simpleQuest in _player.GetSimpleQuests())
        {
            if (simpleQuest.GetActiveStatus() && !simpleQuest.GetIsCompletedStatus())
            {
                simpleQuest.IsComplete(_player);
            }
        }
    }
}