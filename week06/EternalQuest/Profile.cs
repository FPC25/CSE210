#nullable enable

using System;

/// <summary>
/// The Profile class represents a player's personal and spiritual information in Eternal Quest.
/// It manages attributes such as name, age, ordinances, callings, education, priesthood, and progress.
/// The class provides methods for updating profile data, managing quests, handling XP and level progression,
/// and displaying player information. It also supports logic for auto-completing quests based on profile status.
/// </summary>
class Profile
{
    private const string SIMPLE = "simple";

    // --- Basic Info ---
    /// <summary>
    /// The player's name.
    /// </summary>
    private string _name;

    /// <summary>
    /// The player's birthday.
    /// </summary>
    private DateTime _birthday;

    /// <summary>
    /// The player's age.
    /// </summary>
    private int _age;

    /// <summary>
    /// The player's gender (true for male, false for female).
    /// </summary>
    private bool _male;

    // --- Progression ---
    /// <summary>
    /// The player's current level.
    /// </summary>
    private int _level;

    /// <summary>
    /// The player's current XP.
    /// </summary>
    private int _currentXP;

    // --- Personal Status ---
    /// <summary>
    /// Indicates whether the player is married.
    /// </summary>
    private bool _married;

    /// <summary>
    /// Indicates whether the player is currently working.
    /// </summary>
    private bool _working;

    // --- Church Status ---
    /// <summary>
    /// Indicates whether the player has received a patriarchal blessing.
    /// </summary>
    private bool _patriarchalBlessing;

    /// <summary>
    /// Indicates whether the player has an active temple recommendation.
    /// </summary>
    private bool _activeRecommendation;

    /// <summary>
    /// The expiration date of the temple recommendation (nullable).
    /// </summary>
    private DateTime? _recommendationDueDate;

    /// <summary>
    /// The time of the sacramental meeting (nullable).
    /// </summary>
    private TimeSpan? _sacramentalTime;

    /// <summary>
    /// The player's dominical education level (e.g., Seminar, Institute).
    /// </summary>
    private string? _dominicalEducation;

    /// <summary>
    /// The player's priesthood office (e.g., Deacon, Teacher, Priest, Elder, High Priest).
    /// </summary>
    private string? _priesthood;

    // --- Accounts ---
    /// <summary>
    /// The player's FamilySearch account link.
    /// </summary>
    private string _familysearchLink;

    /// <summary>
    /// The player's LDS account username.
    /// </summary>
    private string _ldsAccount;

    // --- Ordinances & Callings ---
    /// <summary>
    /// Dictionary mapping ordinance names to their dates.
    /// </summary>
    private Dictionary<string, DateTime> _ordinances;

    /// <summary>
    /// List of callings (church responsibilities).
    /// </summary>
    private List<string> _callings;

    // --- Quests ---
    /// <summary>
    /// Dictionary mapping quest categories to lists of quests.
    /// </summary>
    private Dictionary<string, List<Quest>> _quests;

    /// <summary>
    /// Constructs a new Profile with basic information and ordinances.
    /// Initializes all fields and sets up education and priesthood status.
    /// </summary>
    public Profile(string name, DateTime birthday, int age, bool male, Dictionary<string, DateTime> ordinances)
    {
        _name = name;
        _birthday = birthday;
        _age = age;
        _male = male;
        _level = 1;
        _currentXP = 0;
        _callings = new List<string>();
        _ordinances = ordinances;
        _familysearchLink = "";
        _ldsAccount = "";
        _married = false;
        _patriarchalBlessing = false;
        _working = false;
        _activeRecommendation = false;
        _sacramentalTime = null;
        _recommendationDueDate = null;
        _quests = new Dictionary<string, List<Quest>>();
        SetDominicalEducation();
        SetAaronicPriesthood();
    }

    // --- Getters for Profile Info ---
    /// <summary>
    /// Gets the player's marital status.
    /// </summary>
    public bool GetMaritalState() => _married;

    /// <summary>
    /// Gets the patriarchal blessing status.
    /// </summary>
    public bool GetPatriarchalBlessingStatus() => _patriarchalBlessing;

    /// <summary>
    /// Gets the work status.
    /// </summary>
    public bool GetWorkStatus() => _working;

    /// <summary>
    /// Gets the expiration date of the temple recommendation.
    /// </summary>
    public DateTime? GetRecommendation() => _recommendationDueDate;

    /// <summary>
    /// Gets the LDS account username.
    /// </summary>
    public string GetLdsAccount() => _ldsAccount;

    /// <summary>
    /// Gets the FamilySearch account link.
    /// </summary>
    public string GetFamilysearchLink() => _familysearchLink;

    /// <summary>
    /// Gets the player's name.
    /// </summary>
    public string GetName() => _name;

    /// <summary>
    /// Gets the player's current XP.
    /// </summary>
    public int GetXP() => _currentXP;

    /// <summary>
    /// Adds XP and checks for level up.
    /// </summary>
    public void AddXP(int xp)
    {
        _currentXP += xp;
        CheckLevelUp();
    }

    /// <summary>
    /// Gets the player's current level.
    /// </summary>
    public int GetLevel() => _level;

    /// <summary>
    /// Checks if the player should level up and updates level/Xp accordingly.
    /// </summary>
    public void CheckLevelUp()
    {
        int nextLevelXP = CalculateNextLevelXP();
        if (_currentXP >= nextLevelXP)
        {
            _level += 1;
            _currentXP -= nextLevelXP;
        }
    }

    /// <summary>
    /// Calculates the XP required for the next level.
    /// </summary>
    public int CalculateNextLevelXP()
    {
        double power = 0.5 * Math.Log(2 * _level + 1);
        return (int)(2650 * Math.Pow(1.5, power));
    }

    /// <summary>
    /// Gets the player's age in years.
    /// </summary>
    public int GetAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - _birthday.Year;
        if (today.Month < _birthday.Month || (today.Month == _birthday.Month && today.Day < _birthday.Day))
        {
            age--;
        }
        return age;
    }

    /// <summary>
    /// Sets the dominical education level based on age.
    /// </summary>
    public void SetDominicalEducation()
    {
        if (_age > 13 && _age < 18)
        {
            _dominicalEducation = "Seminar";
        }
        else if (_age >= 18 && _age < 36)
        {
            _dominicalEducation = "Institute";
        }
        else
        {
            _dominicalEducation = null;
        }
    }

    /// <summary>
    /// Gets the dominical education level.
    /// </summary>
    public string? GetDominicalEducation() => _dominicalEducation;

    /// <summary>
    /// Sets the Aaronic priesthood office based on age and gender.
    /// </summary>
    public void SetAaronicPriesthood()
    {
        if (_male)
        {
            if (_age >= 11 && _age < 14)
            {
                _priesthood = "Deacon";
            }
            else if (_age >= 14 && _age < 16)
            {
                _priesthood = "Teacher";
            }
            else if (_age >= 16)
            {
                _priesthood = "Priest";
            }
        }
        else
        {
            _priesthood = null;
        }
    }

    /// <summary>
    /// Sets the Melchizedek priesthood office if eligible.
    /// </summary>
    public void SetMelchizedekPriesthood(string level)
    {
        DateTime today = DateTime.Now;
        if (_male && _age >= 18 && today >= _ordinances["confirmation"].AddYears(1))
        {
            if (level.ToLower() == "elder")
            {
                _priesthood = "Elder";
            }
            else
            {
                _priesthood = "High Priest";
            }
        }
    }

    /// <summary>
    /// Gets the priesthood office.
    /// </summary>
    public string? GetPriesthood() => _priesthood;

    /// <summary>
    /// Prompts the user to set the sacramental meeting time.
    /// </summary>
    public void SetSacramentalTime()
    {
        string input;
        TimeSpan newTime;
        do
        {
            input = Utils.ValidStringInput("Enter new sacramental time (format: HH:mm):");
            if (!TimeSpan.TryParse(input, out newTime))
            {
                Console.WriteLine("Invalid time format. Try again");
            }
        } while (!TimeSpan.TryParse(input, out newTime));
        _sacramentalTime = newTime;
    }

    /// <summary>
    /// Gets the sacramental meeting time.
    /// </summary>
    public TimeSpan? GetSacramentalTime() => _sacramentalTime;

    /// <summary>
    /// Gets the dictionary of ordinances.
    /// </summary>
    public Dictionary<string, DateTime> GetOrdinances() => _ordinances;

    /// <summary>
    /// Adds new ordinances to the profile.
    /// </summary>
    public void AddOrdinance()
    {
        List<string> ordinances = new List<string>();
        string highOrdinance = "initiatory and endowment";
        if (!_ordinances.ContainsKey(highOrdinance))
        {   
            Console.WriteLine($"Do you want to add the date you've made your {highOrdinance}?");
            if (Utils.DecisionString(new List<string>() { "yes", "no" }) == "yes")
            {
                ordinances.Add(highOrdinance);
            }
        }
        if (_married)
        {
            Console.WriteLine("Do you want to add the sealing date?");
            if (Utils.DecisionString(new List<string>() { "yes", "no" }) == "yes")
            {
                string spouse = Utils.NameToTitleCase(Utils.ValidStringInput("What is the name of your spouse?"));
                ordinances.Add($"sealing with {spouse}");
            }
        }
        if (ordinances.Count != 0)
        {
            var newOrdinance = Utils.GetOrdinance(ordinances);
            foreach (string ordinance in newOrdinance.Keys)
            {
                _ordinances[ordinance] = newOrdinance[ordinance];
            }
        }
    }

    /// <summary>
    /// Gets the list of callings.
    /// </summary>
    public List<string> GetCalling() => _callings;

    /// <summary>
    /// Adds a new calling to the profile.
    /// </summary>
    public void AddCalling()
    {
        if (_callings.Count > 0)
        {
            Console.WriteLine("Do you want to remove a previous calling?");
            if (Utils.DecisionString(new List<string>() { "yes", "no" }) == "yes")
            {
                RemoveCalling();
            }
        }

        string calling = Utils.ValidStringInput("Please enter the new calling you have received: ");
        if (!_callings.Contains(calling.ToLower()))
        {
            _callings.Add(calling);
        }
        else
        {
            Console.WriteLine($"The calling \"{calling}\" is already in your list of callings.");
        }
    }

    /// <summary>
    /// Removes a calling from the profile.
    /// </summary>
    public void RemoveCalling()
    {
        Console.WriteLine("Please select the calling you want to remove: ");
        string callingToRemove = Utils.DecisionString(_callings);
        _callings.Remove(callingToRemove);
    }

    /// <summary>
    /// Inverts a boolean status based on user input.
    /// </summary>
    public bool InvertBoolStatus(string yesNoQuestion, bool status)
    {
        Console.WriteLine(yesNoQuestion);
        if (Utils.DecisionString(new List<string>() { "yes", "no" }) == "yes")
        {
            return !status;
        }
        return status;
    }

    /// <summary>
    /// Prompts the user to set or correct an account (LDS or FamilySearch).
    /// </summary>
    private string SetAccount(string variable, string varName)
    {
        string prompt;
        bool change = false;

        if (_ldsAccount == "")
        {
            prompt = $"Do you want to add a {varName} account? ";
        }
        else
        {
            prompt = $"Do you want to correct your {varName} account? ";
        }
        change = InvertBoolStatus(prompt, change);
        if (change)
        {
            variable = Utils.ValidStringInput("Please enter your account: ");
        }
        return variable;
    }

    /// <summary>
    /// Displays the profile menu and handles user choices for updating profile data.
    /// </summary>
    public void ProfileMenu()
    {
        const string
            REGISTER = "Register Ordinance",
            SACRAMENT = "Change Sacramental Time",
            PRIESTHOOD = "Change Priesthood Office",
            MARRIAGE = "Change Marital Status",
            ACCOUNT = "Change LDS Account",
            FAMILY = "Change FamilySearch Link",
            CALLING = "Add or Remove a Calling",
            WORKING = "Change Work Status",
            RECOMMENDATION = "Renovate Temple Recommendation",
            PATRIARCHAL = "Change Patriarchal Blessing",
            QUIT = "Quit";

        //Some of the basic options the menu can have
        List<string> options = new List<string>()
        {
            SACRAMENT, CALLING, RECOMMENDATION
        };

        //Some conditional options the menu can have,
        if (!_patriarchalBlessing)
        {
            options.Add(PATRIARCHAL);
        }

        if (_age > 12)
        {
            options.Add(ACCOUNT);
        }

        if (_age >= 18)
            {
                options.Add(MARRIAGE);
                options.Add(WORKING);
                options.Add(FAMILY);

                //Since man can seal more than once and women don't we need to separate those situations 
                if (_male)
                {
                    options.Add(PRIESTHOOD);
                    options.Add(REGISTER); //the register is there because the the ordinances the member can have before 18 years old are already set, so only after this age they can do more
                }
                else if (!_ordinances.Keys.Any(key => key.Contains("sealing")))
                {
                    options.Add(REGISTER);
                }
            }

        //The one that must go at the end of the list
        options.Add(QUIT);

        string selectedOption;
        do
        {
            Console.Clear();
            Console.WriteLine("Player Menu:");
            selectedOption = Utils.DecisionString(options);
            switch (selectedOption)
            {
                case REGISTER:
                    Console.Clear();
                    AddOrdinance();
                    break;

                case SACRAMENT:
                    Console.Clear();
                    TimeSpan? sacramentalTime = GetSacramentalTime();
                    string formattedTime = sacramentalTime?.ToString(@"hh\:mm") ?? "Not set";
                    if (formattedTime == "Not set")
                    {
                        Console.WriteLine("You have not set a sacramental time yet.");
                    }
                    else
                    {
                        Console.WriteLine($"Current sacramental time: {formattedTime}");
                    }

                    SetSacramentalTime();

                    break;

                case PRIESTHOOD:
                    Console.Clear();
                    string? priesthood = GetPriesthood();
                    if (priesthood == null)
                    {
                        Console.WriteLine("Priesthood office is not set.");
                        break;
                    }
                    priesthood = priesthood.ToLower();

                    bool changePriesthood = false;
                    string newPriesthood = "";

                    if (priesthood == "priest")
                    {
                        newPriesthood = "elder";
                    }
                    else if (priesthood == "elder")
                    {
                        newPriesthood = "high priest";
                    }
                    else
                    {
                        Console.WriteLine("You reached the highest Priesthood Office available in this game! Press Enter to Continue!");
                        Console.ReadLine();
                        break;
                    }

                    changePriesthood = InvertBoolStatus($"Do you want to change your priesthood office from {priesthood} to {newPriesthood}?", changePriesthood);
                    if (changePriesthood)
                    {
                        SetMelchizedekPriesthood(newPriesthood);
                    }
                    break;

                case MARRIAGE:
                    Console.Clear();
                    _married = InvertBoolStatus("Do you want to change the your marital status?", _married);
                    break;

                case CALLING:
                    Console.Clear();
                    List<string> operations = new List<string> { "Add Calling" };
                    string prompt = "Would you like to add";
                    if (_callings.Count > 0)
                    {
                        operations.Add("Remove Calling");
                        prompt += " or remove";
                    }
                    prompt += " a calling?";
                    operations.Add("Back");

                    Console.WriteLine(prompt);
                    string decision = Utils.DecisionString(operations);

                    if (decision == "Add Calling")
                    {
                        AddCalling();
                    }
                    else if (decision == "Remove Calling")
                    {
                        RemoveCalling();
                    }
                    break;

                case WORKING:
                    Console.Clear();
                    _working = InvertBoolStatus("Are you working at the moment?", _working);
                    break;

                case PATRIARCHAL:
                    Console.Clear();
                    _patriarchalBlessing = InvertBoolStatus("Did you received your patriarchal blessing?", _patriarchalBlessing);
                    break;

                case RECOMMENDATION:
                    Console.Clear();
                    string recommendationPrompt;
                    bool newRecommendation = false;
                    if (!_activeRecommendation && _recommendationDueDate == null)
                    {
                        recommendationPrompt = "Did you received your recommendation?";
                    }
                    else
                    {
                        recommendationPrompt = "Did you renovated your recommendation?";
                    }
                    newRecommendation = InvertBoolStatus(recommendationPrompt, newRecommendation);
                    if (newRecommendation)
                    {
                        Console.WriteLine("Congratulations! Please informe the new due Date!");
                        _recommendationDueDate = Utils.ReadDate($"\nIn what year your new recommendation will expire? (e.g., {DateTime.Now.Year + 2})", "\nIn what month your new recommendation will expire? (Enter the number, e.g., 1 for January)", "In what month your new recommendation will expire?");
                    }
                    break;

                case ACCOUNT:
                    Console.Clear();
                    _ldsAccount = SetAccount(_ldsAccount, "LDS");
                    break;

                case FAMILY:
                    Console.Clear();
                    _familysearchLink = SetAccount(_familysearchLink, "FamilySearch");
                    break;

                case QUIT:
                    Console.Clear();
                    break;
            }
        } while (selectedOption != QUIT);
    }

    /// <summary>
    /// Displays a variable's value in a formatted way.
    /// </summary>
    private void DisplaySettableVar(object? variable, string varName)
    {
        string message = $"{varName}: ";
        if (variable == null ||
           (variable is string str && string.IsNullOrEmpty(str)))
        {
            message += "Not set yet";
        }
        else if (variable is bool b && b == false)
        {
            message += "No";
        }
        else if (variable is string strValue && !string.IsNullOrEmpty(strValue))
        {
            message += strValue;
        }
        else if (variable is TimeSpan time)
        {
            message += time.ToString(@"hh\:mm");
        }
        else if (variable is DateTime date)
        {
            message += date.ToString(@"MM/dd/yyyy");
        }
        else
        {
            message += "Yes";
        }
        Console.WriteLine(message);
    }

    /// <summary>
    /// Displays the player's level progress as a bar.
    /// </summary>
    private void DisplayLevelProgress()
    {
        int nextLevelXP = CalculateNextLevelXP();
        double progress = (double)_currentXP / nextLevelXP;
        int barLength = 20;

        string bar = Utils.BuildProgressBar(progress, barLength);

        // Padding: 2 spaces between level and bar, 2 spaces between bar and next level
        Console.WriteLine($"{_level}  [{bar}]  {_level + 1}");
        Console.WriteLine($"XP: {_currentXP} / {nextLevelXP} to next level");
    }

    /// <summary>
    /// Displays all ordinances and their dates.
    /// </summary>
    private void DisplayOrdinances()
    {
        string padding = new String(' ', 3);
        Console.WriteLine("Ordinances: ");
        foreach (string ordinance in _ordinances.Keys)
        {
            Console.WriteLine($"{padding}* {ordinance}: {_ordinances[ordinance].ToString(@"MM/dd/yyyy")}");
        }

    }

    /// <summary>
    /// Displays all callings.
    /// </summary>
    private void DisplayCallings()
    {
        string padding = new String(' ', 3);
        Console.WriteLine("Callings: ");
        foreach (string calling in _callings)
        {
            Console.WriteLine($"{padding}* {calling}");
        }

    }

    /// <summary>
    /// Displays all player information in a formatted way.
    /// </summary>
    public void DisplayPlayerInfo()
    {
        Console.Clear();

        string mainSeparator = new string('=', 50);
        string subSeparator = new string('-', 30);

        Console.WriteLine(mainSeparator);
        Console.WriteLine("                PLAYER PROFILE");
        Console.WriteLine(mainSeparator);

        Console.WriteLine("Player Info: \n");
        string gender = "Gender: ";
        if (_male) gender += "Masc.";
        else gender += "Fem.";
        Console.Write($"Name: {_name}  |  Age: {GetAge()}  |  {gender}\n");
        Console.WriteLine("Level: \n");
        DisplayLevelProgress();
        Console.WriteLine();
        if (_dominicalEducation != null) DisplaySettableVar(_dominicalEducation, "Dominical Education Level");
        if (_male) DisplaySettableVar(_priesthood, "Priesthood Office");
        DisplaySettableVar(_sacramentalTime, "Sacramental Time");
        DisplaySettableVar(_recommendationDueDate, "Recommendation due Date");
        DisplaySettableVar(_married, "Married");
        DisplaySettableVar(_working, "Working");
        DisplaySettableVar(_patriarchalBlessing, "Patriarchal Blessing Received");
        DisplaySettableVar(_ldsAccount, "LDS Account");
        DisplaySettableVar(_familysearchLink, "FamilySearch Account");
        DisplayCallings();
        DisplayOrdinances();

        Console.WriteLine("\nPress enter to return to the menu!");
        Console.ReadLine();
    }

    /// <summary>
    /// Gets all quest categories and their associated quests.
    /// </summary>
    /// <returns>A dictionary of quest categories and their lists of quests.</returns>
    public Dictionary<string, List<Quest>> GetAllQuests() => _quests;

    /// <summary>
    /// Gets all quest in simple quests category
    /// </summary>
    /// <returns>A list of quest of simple quests.</returns>
    public List<Quest> GetSimpleQuests() => _quests[SIMPLE];

    public void AddCustomQuest(Quest quest, string category) => _quests[category].Add(quest);

    /// <summary>
    /// Returns a dictionary representation of the profile for saving or display.
    /// </summary>
    public Dictionary<string, object?> DataToSaveDict()
    {
        return new Dictionary<string, object?>
            {
                { "Name", _name },
                { "Age", GetAge() },
                { "Gender", _male },
                { "Level", _level },
                { "XP", _currentXP },
                { "DominicalEducation", _dominicalEducation },
                { "Priesthood", _priesthood },
                { "SacramentalTime", _sacramentalTime },
                { "RecommendationDueDate", _recommendationDueDate },
                { "Married", _married },
                { "Working", _working },
                { "PatriarchalBlessing", _patriarchalBlessing },
                { "LDSAccount", _ldsAccount },
                { "FamilySearchAccount", _familysearchLink },
                { "Callings", _callings },
                { "Ordinances", _ordinances },
                { "Quests", _quests}
            };
    }
}