using System;

/// <summary>
/// The abstract Quest class defines the structure and common behaviors for all quest types in Eternal Quest.
/// It manages quest details, completion status, activity status, and XP management.
/// Derived classes must implement specific quest logic, including how events are recorded, completion is determined, and XP is calculated.
/// </summary>
class ChecklistQuest : Quest
{
    /// <summary>
    /// Constant used for XP calculation.
    /// </summary>
    private const double XPMAXSTEP = 0.075f, XPMAXBONUS = 0.15f;

    /// <summary>
    /// The amount of XP awarded for completing this quest.
    /// </summary>
    private int _steps, _total, _xpPoints, _playerXPToNextLevel;

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
    public ChecklistQuest(string name, string description, bool active, int XPNextLevel, int total) : base(name, description, active, XPNextLevel)
    {
        _steps = 0;
        _total = total;
    }

    public ChecklistQuest(string name, string description, bool active, int XPNextLevel, int steps, int total) : base(name, description, active, XPNextLevel)
    {
        _steps = steps;
        _total = total;
    }

    /// <summary>
    /// Returns a formatted string with quest details and completion status.
    /// Can be overridden by derived classes for custom formatting.
    /// </summary>
    /// <returns>A formatted string showing quest status and details.</returns>
    public override string GetDetailsString()
    {
        string complete = _isCompleted ? "X" : " ";
        return $"[{complete}] {_shortName} - ({_steps}/{_total}): {_description}";
    }

    /// <summary>
    /// Records an event or progress for this quest.
    /// Must be implemented by derived classes.
    /// Should mark the quest as complete and handle XP logic.
    /// </summary>
    /// <param name="player">The player's profile, used for updating XP and checking conditions.</param>
    /// <param name="conditional">Condition for auto-check quests.</param>
    public override void RecordEvent(Profile player, bool conditional = false)
    {
        if (conditional)
        {
            _steps++;
            if (_steps < _total)
            {
                player.AddXP(CalculateXpPerQuestType(player.GetLevel()));
            }
            else if (_steps == _total)
            {
                CompleteQuest();
                player.AddXP(CalculateXpPerQuestType(player.GetLevel()));
            }
        }
    }

    /// <summary>
    /// Determines whether the quest is complete.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <param name="player">The player's profile, used for auto-check quests.</param>
    public override void IsComplete(Profile player)
    {
        RecordEvent(player, player.InvertBoolStatus($"Did you completed a step of the quest: '{GetName()}'?", false));
    }

    /// <summary>
    /// Returns a Dictionary representation of the quest for display or saving.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>A Dictionary<string, string> representing the quest.</returns>
    public override Dictionary<string, string> GetDictRepresentation()
    {
        Dictionary<string, string> QuestStatus = new Dictionary<string, string>();
        QuestStatus["type"] = "checklist";
        QuestStatus["name"] = GetName();
        QuestStatus["description"] = GetDescription();
        QuestStatus["steps"] = _steps.ToString();
        QuestStatus["total"] = _total.ToString();
        QuestStatus["active"] = GetActiveStatus().ToString();
        QuestStatus["completed"] = GetIsCompletedStatus().ToString();

        return QuestStatus;
    }

    /// <summary>
    /// Calculates the XP awarded for this quest type.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <param name="level">The player's current level, used for XP calculation.</param>
    /// <returns>The XP value for the quest.</returns>
    public override int CalculateXpPerQuestType(int level)
    {
        int nextLevelXp = GetNextLevelXP();
        double levelFactor = 1 + (K * (level - 1));
        if (_steps < _total)
        {
            return (int)(nextLevelXp * (XPMAXSTEP / levelFactor));
        }
        else if (_steps == _total)
        {
            return (int)(nextLevelXp * ((XPMAXBONUS + XPMAXSTEP) / levelFactor));
        }
        else
        {
            return 0;
        }
    }
}