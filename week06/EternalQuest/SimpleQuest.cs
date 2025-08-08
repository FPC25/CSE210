using System;

class SimpleQuest : Quest
{
    private const double XPMAX = 0.1f;

    private bool _autoCheck;

    public SimpleQuest(string name, string description, bool active, bool autoCheck, int XPNextLevel) : base(name, description, active, XPNextLevel)
    {
        _autoCheck = autoCheck;
    }

    /// <summary>
    /// Records an event or progress for this quest.
    /// Must be implemented by derived classes.
    /// </summary>
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
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>True if the quest is complete; otherwise, false.</returns>
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
    /// Returns a string representation of the quest for display or saving.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>A string representing the quest.</returns>
    public override Dictionary<string, string> GetStringRepresentation()
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
    /// Calculates the XP awarded for this quest type.
    /// Must be implemented by derived classes.
    /// </summary> 
    /// <returns>The XP value for the quest.</returns>
    public override int CalculateXpPerQuestType(int level)
    {
        return (int)(GetNextLevelXP() * (XPMAX / (1 + K * (level - 1))));
    }
}