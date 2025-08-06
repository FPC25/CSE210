using System;

class Profile
{
    private int _level, _currentXP, _age;
    private bool _sex, _married, _patriarcalBlessing, _working, _activeRecommendation;
    private DateTime _sacramentalTime, _recommendationDueDate;
    private Dictionary<string, DateTime> _ordinances;
    private Dictionary<string, List<Quest>> _quests;
    private string _name, __familysearchLink, _ldsAccount;
    private ?string _dominicalEducation, priesthood;
    private List<string> _calling;


    public Profile(string name, int age, bool male)
    {
        _name = name;
        _age = age;
        _male = male;
        _calling = new List<string>();
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

    public SetPriesthood()
    {

    }

    public ?string GetPriesthood()
    {
        return _priesthood;
    }
}