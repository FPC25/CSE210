#nullable enable

using System;
using Microsoft.VisualBasic;
// using Quests; // Removed because the namespace 'Quests' could not be found

class Profile
{
    private int _level, _currentXP, _age;
    private bool _male, _married, _patriarcalBlessing, _working, _activeRecommendation;
    private TimeSpan? _sacramentalTime;
    private DateTime _birthday;
    private DateTime? _recommendationDueDate;
    private Dictionary<string, DateTime> _ordinances;
    private Dictionary<string, List<Quest>> _quests;
    private string _name, _familysearchLink, _ldsAccount;
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
        _familysearchLink = "";
        _ldsAccount = "";
        _married = false;
        _patriarcalBlessing = false;
        _working = false;
        _activeRecommendation = false;
        _sacramentalTime = null;
        _recommendationDueDate = null;
        _quests = new Dictionary<string, List<Quest>>();
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

    public TimeSpan? GetSacramentalTime()
    {
        return _sacramentalTime;
    }

    public Dictionary<string, DateTime> GetOrdinances()
    {
        return _ordinances;
    }

    public void AddOrdinance()
    {
        List<string> ordinances = new List<string>();
        string highOrdinance = "initiatory and endowment";
        if (!_ordinances.ContainsKey(highOrdinance))
        {
            ordinances.Add(highOrdinance);
        }
        if (_married)
        {
            Console.WriteLine("Do you want to add the sealing date?");
            if (Utils.DecisionString(new List<string>() { "yes", "no" }) == "yes")
            {
                string spouse = Utils.ValidStringInput("What is the name of your spouse?");
                ordinances.Add($"sealing with {spouse}");
            }
        }
        if (ordinances.Count != 0) Utils.GetOrdinance(ordinances);
    }

    public List<string> GetCalling()
    {
        return _calling;
    }

    public void AddCalling()
    {
        if (_calling.Count > 0)
        {
            Console.WriteLine("Do you want to remove a previous calling?");
            if (Utils.DecisionString(new List<string>() { "yes", "no" }) == "yes")
            {
                RemoveCalling();
            }
        }
               
        string calling = Utils.ValidStringInput("Please enter the new calling you have received: ");
        if (!_calling.Contains(calling.ToLower()))
        {
            _calling.Add(calling);
        }
        else
        {
            Console.WriteLine($"The calling \"{calling}\" is already in your list of callings.");
        }
    }

    public void RemoveCalling()
    {
        Console.WriteLine("Please select the calling you want to remove: ");
        string callingToRemove = Utils.DecisionString(_calling);
        _calling.Remove(callingToRemove);
    }

    private bool invertBoolStatus(string yesNoQuestion, bool status)
    {
        Console.WriteLine(yesNoQuestion);
        if (Utils.DecisionString(new List<string>() { "yes", "no" }) == "yes")
        {
            return !status;
        }
        return status;
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

        //Some of the basic options the menu can have
        List<string> options = new List<string>()
        {
            SACRAMENT, CALLING, RECOMMENDATION
        };

        //Some conditional options the menu can have, the register is there because the the ordinances the member can have before 18 years old are already set, so only after this age they can do more
        if (_age >= 18)
        {
            options.Add(MARRIAGE);
            options.Add(WORKING);

            //Since man can seal more than once and women don't we need to separate those situations 
            if (_male)
            {
                options.Add(PRIESTHOOD);
                options.Add(REGISTER);
            }
            else if (!_ordinances.Keys.Any(key => key.Contains("sealing")))
            {
                options.Add(REGISTER);
            }
        }

        //The one that must go at the end of the list
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
                    AddOrdinance();
                    break;

                case SACRAMENT:
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

                case MARRIAGE:
                    _married = invertBoolStatus("Do you want to change the your marital status?", _married);
                    break;

                case CALLING:
                    // Simplified logic for adding/removing callings
                    List<string> operations = new List<string> { "Add Calling" };
                    string prompt = "Would you like to add";
                    if (_calling.Count > 0)
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
                    // "Back" just breaks out of the case
                    break;

                case WORKING:
                    _working = invertBoolStatus("Are you working at the moment?", _working);
                    break;

                case RECOMMENDATION:
                    Console.WriteLine("Work in progress.");
                    break;

                case COMPLETED:
                    Console.WriteLine("Work in progress.");
                    break;

                case QUIT:
                    break;
            }
        } while (selectedOption != QUIT);
    }
}