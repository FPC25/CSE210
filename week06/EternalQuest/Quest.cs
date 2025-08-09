using System;

/// <summary>
/// The abstract Quest class defines the structure and common behaviors for all quest types in Eternal Quest.
/// It manages quest details, completion status, activity status, and XP management.
/// Derived classes must implement specific quest logic, including how events are recorded, completion is determined, and XP is calculated.
/// </summary>
abstract class Quest
{
    /// <summary>
    /// Constant used for XP calculation.
    /// </summary>
    protected const double K = 0.5;

    /// <summary>
    /// The amount of XP awarded for completing this quest.
    /// </summary>
    private int _xpPoints, _playerXPToNextLevel;

    /// <summary>
    /// The short name and description of the quest.
    /// </summary>
    private string _shortName, _description;

    /// <summary>
    /// Indicates whether the quest is completed and active.
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

    /// <summary>
    /// Gets the short name of the quest.
    /// </summary>
    public string GetName() => _shortName;

    /// <summary>
    /// Gets the description of the quest.
    /// </summary>
    public string GetDescription() => _description;

    /// <summary>
    /// Gets the active status of the quest.
    /// </summary>
    public bool GetActiveStatus() => _active;

    /// <summary>
    /// Sets the active status of the quest.
    /// </summary>
    public void SetActiveStatus(bool active) => _active = active;

    /// <summary>
    /// Gets the XP required for the next level.
    /// </summary>
    public int GetNextLevelXP() => _playerXPToNextLevel;

    /// <summary>
    /// Gets the completion status of the quest.
    /// </summary>
    public bool GetIsCompletedStatus() => _isCompleted;

    /// <summary>
    /// Marks the quest as completed and sets it as inactive.
    /// </summary>
    protected void CompleteQuest()
    {
        _active = false;
        _isCompleted = true;
    }

    /// <summary>
    /// Sets the amount of XP awarded for completing this quest.
    /// </summary>
    /// <param name="xp">The XP value to assign.</param>
    public void SetXpPoints(int xp) => _xpPoints = xp;

    /// <summary>
    /// Gets the amount of XP awarded for completing this quest.
    /// </summary>
    public int GetXpPoints() => _xpPoints;

    /// <summary>
    /// Returns a formatted string with quest details and completion status.
    /// Can be overridden by derived classes for custom formatting.
    /// </summary>
    /// <returns>A formatted string showing quest status and details.</returns>
    public virtual string GetDetailsString()
    {
        string complete = _isCompleted ? "X" : " ";
        return $"[{complete}] {_shortName} - {_description}";
    }

    /// <summary>
    /// Records an event or progress for this quest.
    /// Must be implemented by derived classes.
    /// Should mark the quest as complete and handle XP logic.
    /// </summary>
    /// <param name="player">The player's profile, used for updating XP and checking conditions.</param>
    /// <param name="conditional">Condition for auto-check quests.</param>
    public abstract void RecordEvent(Profile player, bool conditional);

    /// <summary>
    /// Determines whether the quest is complete.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <param name="player">The player's profile, used for auto-check quests.</param>
    public abstract void IsComplete(Profile player);

    /// <summary>
    /// Returns a Dictionary representation of the quest for display or saving.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>A Dictionary<string, string> representing the quest.</returns>
    public abstract Dictionary<string, string> GetStringRepresentation();

    /// <summary>
    /// Calculates the XP awarded for this quest type.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <param name="level">The player's current level, used for XP calculation.</param>
    /// <returns>The XP value for the quest.</returns>
    public abstract int CalculateXpPerQuestType(int level);
}