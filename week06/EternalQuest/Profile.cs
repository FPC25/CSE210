using System;

class Profile
{
    private int _level, _currentXP, _age;
    private bool _sex, _married, _patriarcalBlessing, _working, _activeRecommendation;
    private DateTime _sacramentalTime, _recommendationDueDate;
    private Dictionary<string, DateTime> _ordinances;
    private Dictionary<string, List<Quest>> _quests;
    private string _name, __familysearchLink, _ldsAccount;
    private ?string _dominicalEducation, _priesthood;
    private List<string> _calling;


    public Profile(string name, int age, bool male, Dictionary<string, DateTime> ordinances)
    {
        _name = name;
        _age = age;
        _male = male;
        _calling = new List<string>();
        _ordinances = ordinances;
        SetDominicalEducation();

    }

    public void SetDominicalEducation()
    {
        if (_age > 13  && _age < 18)
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

    public ?string GetDominicalEducation()
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

    public void SetMelchizedekPriesthood()
    {
        DateTime now = new DateTime;
        now = DateTime.Now; 
        if (_male && _age )
    }

    public ?string GetPriesthood()
    {
        return _priesthood;
    }
}