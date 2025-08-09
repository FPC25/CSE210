using System;

/// <summary>
/// The EternalQuest class represents a recurring quest that can be daily, weekly, or monthly.
/// It inherits from the abstract Quest class and uses a frequency tag to determine how often the quest repeats.
/// EternalQuest tracks its initial date, completion status, and activity status.
/// It awards XP based on its frequency and the player's level.
/// The class provides methods for recording completion, checking status, displaying quest details,
/// and serializing quest status for saving or display.
/// </summary>
class EternalQuest : Quest
{
    /// <summary>
    /// XP multipliers for daily, weekly, and monthly eternal quests.
    /// </summary>
    private const double XPMAXDAILY = 0.03f, XPMAXWEEKLY = 0.06f, XPMAXMONTHLY = 0.09f;

    /// <summary>
    /// The current number of completed steps and the total required steps (if applicable).
    /// </summary>
    private int _steps, _total, _playerXPToNextLevel;

    /// <summary>
    /// The short name, description, and frequency of the quest.
    /// </summary>
    private string _shortName, _description, _frequency;

    /// <summary>
    /// Indicates whether the quest is completed and active.
    /// </summary>
    private bool _isCompleted, _active;

    /// <summary>
    /// The initial date when the quest was created or started.
    /// </summary>
    private DateTime _initialDate;

    /// <summary>
    /// Constructs a new EternalQuest with the specified details and frequency.
    /// </summary>
    /// <param name="name">The short name of the quest.</param>
    /// <param name="description">A description of the quest.</param>
    /// <param name="frequency">The frequency of the quest (daily, weekly, monthly).</param>
    /// <param name="active">If the quest is active or not.</param>
    /// <param name="XPNextLevel">The XP required for the next level.</param>
    /// <param name="initialDate">The initial date of the quest.</param>
    public EternalQuest(string name, string description, string frequency, bool active, int XPNextLevel, DateTime initialDate, List<string> requirements) : base(name, description, active, XPNextLevel, requirements)
    {
        _frequency = frequency;
        _initialDate = initialDate;
    }

    /// <summary>
    /// Records completion for this quest and awards XP if the condition is met.
    /// </summary>
    /// <param name="player">The player's profile, used for updating XP.</param>
    /// <param name="conditional">Whether the quest was completed.</param>
    public override void RecordEvent(Profile player, bool conditional)
    {
        if (conditional)
        {
            CompleteQuest();
            player.AddXP(CalculateXpPerQuestType(player.GetLevel()));
        }
    }

    /// <summary>
    /// Prompts the user to record completion of the quest.
    /// </summary>
    /// <param name="player">The player's profile.</param>
    public override void IsComplete(Profile player)
    {
        RecordEvent(player, player.InvertBoolStatus($"Did you completed the quest: '{GetName()}'?", false));
    }

    /// <summary>
    /// Returns a dictionary representation of the quest for saving or display.
    /// </summary>
    /// <returns>A Dictionary<string, string> representing the quest.</returns>
    public override Dictionary<string, string> GetDictRepresentation()
    {
        Dictionary<string, string> QuestStatus = new Dictionary<string, string>();
        QuestStatus["type"] = "eternal";
        QuestStatus["frequency"] = _frequency;
        QuestStatus["initialDate"] = _initialDate.ToString();
        QuestStatus["name"] = GetName();
        QuestStatus["description"] = GetDescription();
        QuestStatus["active"] = GetActiveStatus().ToString();
        QuestStatus["completed"] = GetIsCompletedStatus().ToString();

        return QuestStatus;
    }

    /// <summary>
    /// Calculates the XP awarded for this quest type based on the player's level and quest frequency.
    /// </summary>
    /// <param name="level">The player's current level.</param>
    /// <returns>The XP value for the quest.</returns>
    public override int CalculateXpPerQuestType(int level)
    {
        int nextLevelXp = GetNextLevelXP();
        double levelFactor = 1 + (K * (level - 1));
        switch (_frequency)
        {
            case "daily":
                return (int)(nextLevelXp * (XPMAXDAILY / levelFactor));

            case "weekly":
                return (int)(nextLevelXp * (XPMAXWEEKLY / levelFactor));

            case "monthly":
                return (int)(nextLevelXp * (XPMAXMONTHLY / levelFactor));

            default:
                return 0;
        }
    }
}