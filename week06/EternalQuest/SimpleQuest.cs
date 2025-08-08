using System;

class SimpleQuest : Quest
{
    private const double XPMAX = 0.1f;

    public SimpleQuest(string name, string description, string type, int XPNextLevel) : base(name, description, type, XPNextLevel)
    {
    }

    /// <summary>
    /// Records an event or progress for this quest.
    /// Must be implemented by derived classes.
    /// </summary>
    public override void RecordEvent();

    /// <summary>
    /// Determines whether the quest is complete.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>True if the quest is complete; otherwise, false.</returns>
    public override bool IsComplete();

    /// <summary>
    /// Returns a string representation of the quest for display or saving.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <returns>A string representing the quest.</returns>
    public override string GetStringRepresentation();

    /// <summary>
    /// Calculates the XP awarded for this quest type.
    /// Must be implemented by derived classes.
    /// </summary> 
    /// <returns>The XP value for the quest.</returns>
    public override int CalculateXpPerQuestType(int level)
    {
        return (int)(GetNextLevelXP() * (XPMAX / (1 + K*(level - 1))));
    }
}