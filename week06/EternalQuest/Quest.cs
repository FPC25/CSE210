using System;

/// <summary>
/// The abstract Quest class defines the structure and common behaviors for all quest types in Eternal Quest.
/// It provides fields for quest details, completion status, and XP management.
/// Derived classes must implement core quest logic and can override detail formatting.
/// </summary>
abstract class Quest
{
    protected const double K = 0.5;
    /// <summary>
    /// The amount of XP awarded for completing this quest.
    /// </summary>
    private int _xpPoints, _playerXPToNextLevel;

    /// <summary>
    /// The short name, description, and type of the quest.
    /// </summary>
    private string _shortName, _description;

    /// <summary>
    /// Indicates whether the quest is completed.
    /// </summary>
    private bool _isCompleted, _active;

    /// <summary>
    /// Constructs a new Quest with the specified details.
    /// </summary>
    /// <param name="name">The short name of the quest.</param>
    /// <param name="description">A description of the quest.</param>
    /// <param name="active">If the quest is active or not.</param>
    /// <param name="XPNextLevel">The XP required for the next level.</param>
    public Quest(string name, string description, bool active, int XPNextLevel)
    {
        _shortName = name;
        _description = description;
        _active = active;
        _playerXPToNextLevel = XPNextLevel;
        _isCompleted = false;
    }

    public string GetName()
    {
        return _shortName;
    }

    public string GetDescription()
    {
        return _description;
    }

    public bool GetActiveStatus()
    {
        return _active;
    }

    public void SetActiveStatus(bool active)
    {
        _active = active;
    }

    public int GetNextLevelXP()
    {
        return _playerXPToNextLevel;
    }

    public bool GetIsCompletedStatus()
    {
        return _isCompleted;
    }

    protected void CompleteQuest()
    {
        _active = false;
        _isCompleted = true;
    }

    /// <summary>
    /// Sets the amount of XP awarded for completing this quest.
    /// </summary>
    /// <param name="xp">The XP value to assign.</param>
    public void SetXpPoints(int xp)
    {
        _xpPoints = xp;
    }

    /// <summary>
    /// Gets the amount of XP awarded for completing this quest.
    /// </summary>
    /// <returns>The XP value for the quest.</returns>
    public int GetXpPoints()
    {
        return _xpPoints;
    }

    /// <summary>
    /// Returns a formatted string with quest details and completion status.
    /// Can be overridden by derived classes for custom formatting.
    /// </summary>
    /// <returns>A formatted string showing quest status and details.</returns>
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

    /// <summary>
    /// Records an event or progress for this quest.
    /// Must be implemented by derived classes.
    /// </summary>
    public abstract void RecordEvent(Profile player, bool conditional);

    /// <summary>
    /// Determines whether the quest is complete.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>True if the quest is complete; otherwise, false.</returns>
    public abstract void IsComplete(Profile player);

    /// <summary>
    /// Returns a Dictionary<string, string> representation of the quest for display or saving.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>A Dictionary<string, string> representing the quest.</returns>
    public abstract Dictionary<string, string> GetStringRepresentation();

    /// <summary>
    /// Calculates the XP awarded for this quest type.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>The XP value for the quest.</returns>
    public abstract int CalculateXpPerQuestType(int level);
}