using System;

/// <summary>
/// The abstract Quest class defines the structure and common behaviors for all quest types in Eternal Quest.
/// It provides fields for quest details, completion status, and XP management.
/// Derived classes must implement core quest logic and can override detail formatting.
/// </summary>
abstract class Quest
{
    /// <summary>
    /// The amount of XP awarded for completing this quest.
    /// </summary>
    private int _xpPoints, _playerXPToNextLevel;

    /// <summary>
    /// The short name, description, and type of the quest.
    /// </summary>
    private string _shortName, _description, _type;

    /// <summary>
    /// Indicates whether the quest is completed.
    /// </summary>
    private bool _isCompleted;

    /// <summary>
    /// Constructs a new Quest with the specified details.
    /// </summary>
    /// <param name="name">The short name of the quest.</param>
    /// <param name="description">A description of the quest.</param>
    /// <param name="type">The type/category of the quest.</param>
    /// <param name="XPNextLevel">The XP required for the next level.</param>
    public Quest(string name, string description, string type, int XPNextLevel)
    {
        _shortName = name;
        _description = description;
        _type = type;
        _playerXPToNextLevel = XPNextLevel;
        _isCompleted = false;
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
    /// Records an event or progress for this quest.
    /// Must be implemented by derived classes.
    /// </summary>
    public abstract void RecordEvent();

    /// <summary>
    /// Determines whether the quest is complete.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>True if the quest is complete; otherwise, false.</returns>
    public abstract bool IsComplete();

    /// <summary>
    /// Returns a string representation of the quest for display or saving.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>A string representing the quest.</returns>
    public abstract string GetStringRepresentation();

    /// <summary>
    /// Calculates the XP awarded for this quest type.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>The XP value for the quest.</returns>
    public abstract int CalculateXpPerQuestType();

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
}