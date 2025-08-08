using System;

abstract class Quest
{
    private int _xpPoints, _playerXPToNextLevel;
    private string _shortName, _description, _type;
    private bool _isCompleted;

    public Quest(string name, string description, string type, int XPNextLevel)
    {
        _shortName = name;
        _description = description;
        _type = type;
        _playerXPToNextLevel = XPNextLevel;
        _isCompleted = false;
    }

    public abstract void RecordEvent();

    public abstract bool IsComplete();

    public abstract string GetStringRepresentation();

    public abstract int CalculateXpPerQuestType();

    public virtual string GetDetailsString()
    {
        string complete, message;
        if (_isCompleted)
        {
            complete = "X";
        }
        else
        {
            complete = " ";
        }
        message = $"[{complete}] {_shortName} - {_description}";
        return message;
    }
    
}