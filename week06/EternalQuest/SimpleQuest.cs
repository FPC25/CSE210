using System;

/// <summary>
/// The SimpleQuest class represents a quest that is completed by a single action or profile update.
/// It inherits from the abstract Quest class and implements logic for auto-check and manual completion.
/// SimpleQuest can be automatically completed based on profile status or manually marked as complete by the user.
/// </summary>
class SimpleQuest : Quest
{
    /// <summary>
    /// The maximum XP multiplier for a simple quest.
    /// </summary>
    private const double XPMAX = 0.1f;

    /// <summary>
    /// Indicates whether this quest should be auto-checked based on profile status.
    /// </summary>
    private bool _autoCheck;

    /// <summary>
    /// Initializes a new instance of the SimpleQuest class with the specified details.
    /// </summary>
    /// <param name="name">The name of the quest.</param>
    /// <param name="description">A description of the quest.</param>
    /// <param name="active">Whether the quest is currently active.</param>
    /// <param name="autoCheck">Whether the quest should be auto-checked.</param>
    /// <param name="XPNextLevel">The XP required for the next level.</param>
    public SimpleQuest(string name, string description, bool active, bool autoCheck, int XPNextLevel, List<string> requirements) : base(name, description, active, XPNextLevel, requirements)
    {
        _autoCheck = autoCheck;
    }

    public bool GetAutoCheck() => _autoCheck;

    /// <summary>
    /// Records an event or progress for this quest.
    /// If the condition is met, marks the quest as complete and awards XP to the player.
    /// </summary>
    /// <param name="player">The player's profile.</param>
    /// <param name="conditional">Whether the quest's completion condition is met.</param>
    public override void RecordEvent(Profile player, bool conditional)
    {
        if (conditional)
        {
            CompleteQuest();
            player.AddXP(CalculateXpPerQuestType(player.GetLevel()));
        }
    } 

    /// <summary>
    /// Determines whether the quest is complete.
    /// For auto-check quests, checks relevant profile status.
    /// For manual quests, prompts the user for completion.
    /// </summary>
    /// <param name="player">The player's profile.</param>
    public override void IsComplete(Profile player)
    {
        if (_autoCheck)
        {
            switch (GetName())
            {
                case "Sacramental Time":
                    RecordEvent(player, player.GetSacramentalTime() != null);
                    break;
                case "LDS account":
                    RecordEvent(player, !string.IsNullOrEmpty(player.GetLdsAccount()));
                    break;
                case "FamilySearch account":
                    RecordEvent(player, !string.IsNullOrEmpty(player.GetFamilysearchLink()));
                    break;
                case "Temple Recommendation":
                    RecordEvent(player, player.GetRecommendation != null);
                    break;
                case "Patriarchal Blessing":
                    RecordEvent(player, player.GetPatriarchalBlessingStatus());
                    break;
                case "Receive a Calling":
                    RecordEvent(player, player.GetCalling().Count > 0);
                    break;
                case "Enter the Temple":
                    RecordEvent(player, player.GetOrdinances().Keys.Contains("initiatory and endowment"));
                    break;
                case "Sealing to Eternity":
                    RecordEvent(player, (player.GetMaritalState() && player.GetOrdinances().Keys.Any(key => key.Contains("sealing"))));
                    break;
            }
        }
        else
        {
            RecordEvent(player, player.InvertBoolStatus($"Did you completed the quest '{GetName()}'?", false));
        }
    }

    /// <summary>
    /// Returns a dictionary representation of the quest for display or saving.
    /// </summary>
    /// <returns>A dictionary containing quest details and status.</returns>
    public override Dictionary<string, string> GetDictRepresentation()
    {
        Dictionary<string, string> QuestStatus = new Dictionary<string, string>();
        QuestStatus["type"] = "simple";
        QuestStatus["name"] = GetName();
        QuestStatus["description"] = GetDescription();
        QuestStatus["active"] = GetActiveStatus().ToString();
        QuestStatus["completed"] = GetIsCompletedStatus().ToString();

        return QuestStatus;
    }

    /// <summary>
    /// Calculates the XP awarded for this quest type based on the player's level.
    /// </summary>
    /// <param name="level">The player's current level.</param>
    /// <returns>The XP value for the quest.</returns>
    public override int CalculateXpPerQuestType(int level)
    {
        int nextLevelXp = GetNextLevelXP();
        double levelFactor = 1 + (K * (level - 1));
        return (int)(nextLevelXp * (XPMAX / levelFactor));
    }
}