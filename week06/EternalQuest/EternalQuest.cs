using System;

/// <summary>
/// The ChecklistQuest class represents a quest that requires completing multiple steps or tasks.
/// It inherits from the abstract Quest class and tracks progress through a step counter.
/// ChecklistQuest awards XP for each completed step and a bonus when all steps are finished.
/// The class provides methods for recording progress, checking completion, displaying quest details,
/// and serializing quest status for saving or display.
/// </summary>
class EternalQuest : Quest
{
    /// <summary>
    /// XP multiplier for each step and for the bonus when the quest is completed.
    /// </summary>
    private const double XPMAXDAILY = 0.03f, XPMAXWEEKLY = 0.06f, XPMAXMONTHLY = 0.09f;

    /// <summary>
    /// The current number of completed steps and the total required steps.
    /// </summary>
    private int _steps, _total, _playerXPToNextLevel;

    /// <summary>
    /// The short name and description of the quest.
    /// </summary>
    private string _shortName, _description, _frequency;

    /// <summary>
    /// Indicates whether the quest is completed and active.
    /// </summary>
    private bool _isCompleted, _active;

    /// <summary>
    /// Constructs a new ChecklistQuest with the specified details and total steps.
    /// </summary>
    /// <param name="name">The short name of the quest.</param>
    /// <param name="description">A description of the quest.</param>
    /// <param name="active">If the quest is active or not.</param>
    /// <param name="XPNextLevel">The XP required for the next level.</param>
    /// <param name="total">The total number of steps required to complete the quest.</param>
    public EternalQuest(string name, string description, string frequency, bool active, int XPNextLevel) : base(name, description, active, XPNextLevel)
    {
        _frequency = frequency;
    }

    /// <summary>
    /// Records progress for this quest by incrementing the step counter.
    /// Awards XP for each step and a bonus when the quest is completed.
    /// </summary>
    /// <param name="player">The player's profile, used for updating XP.</param>
    /// <param name="conditional">Whether a step was completed.</param>
    public override void RecordEvent(Profile player, bool conditional)
    {
        if (conditional)
        {
            CompleteQuest();
            player.AddXP(CalculateXpPerQuestType(player.GetLevel()));
        }
    }

    /// <summary>
    /// Checks if the quest is complete by comparing steps to total.
    /// Prompts the user to record progress if not complete.
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
        QuestStatus["name"] = GetName();
        QuestStatus["description"] = GetDescription();
        QuestStatus["frequency"] = _frequency;
        QuestStatus["active"] = GetActiveStatus().ToString();
        QuestStatus["completed"] = GetIsCompletedStatus().ToString();

        return QuestStatus;
    }

    /// <summary>
    /// Calculates the XP awarded for this quest type based on the player's level and progress.
    /// Awards a bonus when the quest is fully completed.
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