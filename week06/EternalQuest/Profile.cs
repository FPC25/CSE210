#nullable enable

using System;
// using Quests; // Removed because the namespace 'Quests' could not be found

class Profile
{
    private int _level, _currentXP, _age;
    private bool _male, _married, _patriarcalBlessing, _working;
    private bool? _activeRecommendation;
    private DateTime _birthday, _sacramentalTime, _recommendationDueDate;
    private Dictionary<string, DateTime> _ordinances;
    private Dictionary<string, List<Quest>> _quests;
    private string _name, __familysearchLink, _ldsAccount;
    private string? _dominicalEducation, _priesthood;
    private List<string> _calling;


    public Profile(string name, DateTime birthday, int age, bool male, Dictionary<string, DateTime> ordinances)
    {
        _name = name;
        _birthday = birthday;
        _age = age;
        _male = male;
        _level = 1;
        _currentXP = 0;
        _calling = new List<string>();
        _ordinances = ordinances;
        SetDominicalEducation();
        SetAaronicPriesthood();

    }

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

    public string? GetDominicalEducation()
    {
        return _dominicalEducation;
    }

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

    public void SetMelchizedekPriesthood(string level)
    {
        //I know there is great levels of priesthood, but greater than this I doubt they would still 'play' this game
        DateTime today = new DateTime();
        today = DateTime.Now;

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

    public string? GetPriesthood()
    {
        return _priesthood;
    }

    private void ProfileMenu()
    {
        const string
            REGISTER = "Register Ordinance",
            SACRAMENT = "Change Sacramental Time",
            PRIESTHOOD = "Change Priesthood Office",
            MARRIAGE = "Change Marital Status",
            CALLING = "Add or Remove a Calling",
            WORKING = "Change Work Status",
            RECOMMENDATION = "Renovate Temple Recommendation",
            COMPLETED = "See Completed Quests",
            QUIT = "Quit";

        //Some of the basic options the menu can have, register a new ordinance, 
        List<string> options = new List<string>()
        {
            SACRAMENT, CALLING
        };

        if (_activeRecommendation != null)
        {
            options.Add(RECOMMENDATION);
        }

        if (_age > 18)
        {
            options.Add(REGISTER);
            options.Add(MARRIAGE);
            options.Add(WORKING);
            if (_male)
            {
                options.Add(PRIESTHOOD);
            }
        }
        
        options.Add(COMPLETED);
        options.Add(QUIT);

        string selectedOption;
        do
        {
            Console.WriteLine("Player Menu:");
            selectedOption = Utils.DecisionString(options);
            switch (selectedOption)
            {
                case REGISTER:
                    Console.WriteLine("Still in development");
                    break;

                case SACRAMENT:
                    Console.WriteLine("Still in development");
                    break;

                case PRIESTHOOD:
                    // We can ignore this warning since to access this option the game have already checked if you are a man over 18 years old so the value can't be null or lower than priest;
                    #pragma warning disable CS8602 // Dereference of a possibly null reference.
                    string priesthood = GetPriesthood().ToLower();
                    #pragma warning restore CS8602 // Dereference of a possibly null reference.
                    if (priesthood == "priest")
                    {
                        SetMelchizedekPriesthood("elder");
                    }
                    else if (priesthood == "elder")
                    {
                        SetMelchizedekPriesthood("high priest");
                    }
                    else if (priesthood == "high priest")
                    {
                        Console.WriteLine($"You already hold the {priesthood} office. Consider if you should continue to play this game game.");
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong! You should not be here!");
                    }

                    break;

                case COMPLETED:
                    Console.WriteLine("Still in development");
                    break;

                case QUIT:
                    break;
            }
        } while (selectedOption != QUIT);
    }
}