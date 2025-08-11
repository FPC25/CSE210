using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// The QuestManager class manages all quest-related operations for a player's profile in Eternal Quest.
/// It loads quests from JSON files, populates the profile's quest dictionary, activates quests based on requirements,
/// and provides methods to display active and completed quests. It also verifies quest requirements and handles
/// quest activation logic based on both dependencies and profile-specific conditions.
/// </summary>
public class QuestManager
{
    // --- File Paths for Quest Data ---
    private const string
        SIMPLE = "simple",
        CHECKLIST = "checklist",
        ETERNAL = "eternal",
        SIMPLEPATH = "./Quests/SimpleQuests.json",
        CHECKLISTPATH = "./Quests/ChecklistQuests.json",
        DAILYPATH = "./Quests/DailyEternalQuests.json",
        WEEKLYPATH = "./Quests/WeeklyEternalQuests.json",
        MONTHLYPATH = "./Quests/MonthlyEternalQuests.json";

    private const int MAXREP = 12;

    // --- Reference to Player Profile ---
    private Profile _player;

    /// <summary>
    /// Constructs a new QuestManager for the given player profile.
    /// </summary>
    /// <param name="player">The Profile object representing the current player.</param>
    public QuestManager(Profile player)
    {
        _player = player;
    }

    /// <summary>
    /// Loads quests from a JSON file and returns a list of Quest objects.
    /// </summary>
    /// <param name="filepath">Path to the JSON file.</param>
    /// <param name="questType">Type of quest to instantiate ("simple", CHECKLIST, "eternal").</param>
    /// <returns>List of Quest objects loaded from the file.</returns>
    public List<Quest> LoadQuestsFromJson(string filepath, string questType)
    {
        if (!File.Exists(filepath))
        {
            Console.WriteLine($"Quest file not found: {filepath}");
            return new List<Quest>();
        }

        var quests = new List<Quest>();
        string json = File.ReadAllText(filepath);
        using var doc = JsonDocument.Parse(json);
        var questArray = doc.RootElement.GetProperty("quests").EnumerateArray();

        foreach (var q in questArray)
        {
            string description;
            string name = q.GetProperty("name").GetString();
            bool active = q.GetProperty("active").GetBoolean();
            int xpNextLevel = _player.CalculateNextLevelXP();
            List<string> requirements = new List<string>();
            if (q.TryGetProperty("requirement", out var reqProp) && reqProp.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement item in reqProp.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.String)
                        requirements.Add(item.GetString());
                }
            }

            switch (questType)
            {
                case SIMPLE:
                    description = q.GetProperty("description").GetString();
                    bool autoCheck = q.GetProperty("auto_check").GetBoolean();
                    quests.Add(new SimpleQuest(name, description, active, autoCheck, xpNextLevel, requirements));
                    break;

                case CHECKLIST:
                    string initial = q.GetProperty("first_part_description").GetString();
                    string final = q.GetProperty("second_part_description").GetString();
                    int total = q.TryGetProperty("total", out var totalProp) ? CalculateTotalSteps(_player.GetLevel()) : 1;
                    int steps = q.TryGetProperty("steps", out var stepsProp) ? stepsProp.GetInt32() : 0;
                    description = $"{initial} {total} {final}";
                    quests.Add(new ChecklistQuest(name, description, active, xpNextLevel, steps, total, requirements));
                    break;

                case ETERNAL:
                    description = q.GetProperty("description").GetString();
                    string frequency = q.GetProperty("frequency").GetString();
                    quests.Add(new EternalQuest(name, description, frequency, active, xpNextLevel, DateTime.Now, requirements));
                    break;
            }
        }

        return quests;
    }

    /// <summary>
    /// Calculates the total number of steps for a checklist quest based on player level.
    /// The value grows gradually from 3 to a maximum of around 15.
    /// </summary>
    /// <param name="playerLevel">The current player level.</param>
    /// <returns>Number of steps required (3-15 range).</returns>
    private int CalculateTotalSteps(int playerLevel)
    {
        // Fórmula: 3 + (level * 0.6) com cap de 15
        int calculatedSteps = 3 + (int)(playerLevel * 0.6);

        // Garante que não passe de 15
        return Math.Min(calculatedSteps, MAXREP);
    }

    /// <summary>
    /// Gets all daily eternal quests.
    /// </summary>
    /// <returns>List of daily eternal Quest objects.</returns>
    public List<Quest> GetDailyQuests() => LoadQuestsFromJson(DAILYPATH, ETERNAL);

    /// <summary>
    /// Gets all weekly eternal quests.
    /// </summary>
    /// <returns>List of weekly eternal Quest objects.</returns>
    public List<Quest> GetWeeklyQuests() => LoadQuestsFromJson(WEEKLYPATH, ETERNAL);

    /// <summary>
    /// Gets all monthly eternal quests.
    /// </summary>
    /// <returns>List of monthly eternal Quest objects.</returns>
    public List<Quest> GetMonthlyQuests() => LoadQuestsFromJson(MONTHLYPATH, ETERNAL);

    /// <summary>
    /// Gets all simple quests.
    /// </summary>
    /// <returns>List of simple Quest objects.</returns>
    public List<Quest> GetSimpleQuests() => LoadQuestsFromJson(SIMPLEPATH, SIMPLE);

    /// <summary>
    /// Gets all checklist quests.
    /// </summary>
    /// <returns>List of checklist Quest objects.</returns>
    public List<Quest> GetChecklistQuests() => LoadQuestsFromJson(CHECKLISTPATH, CHECKLIST);

    /// <summary>
    /// Populates the player's quest dictionary with all loaded quests by category.
    /// </summary>
    public void PopulatePlayerQuests()
    {
        Dictionary<string, List<Quest>> quests = _player.GetAllQuests();
        List<Quest> eternalQuests = new List<Quest>();

        eternalQuests.AddRange(GetDailyQuests());
        eternalQuests.AddRange(GetWeeklyQuests());
        eternalQuests.AddRange(GetMonthlyQuests());

        quests[SIMPLE] = GetSimpleQuests();
        quests[CHECKLIST] = GetChecklistQuests();
        quests[ETERNAL] = eternalQuests;
    }

    /// <summary>
    /// Displays all active quests for the player, grouped by category with improved formatting.
    /// </summary>
    public void DisplayActiveQuests()
    {
        string mainSeparator = new string('=', 50);
        string subSeparator = new string('-', 30);
        bool hasActiveQuests = false;

        Console.WriteLine(mainSeparator);
        Console.WriteLine("                ACTIVE QUESTS");
        Console.WriteLine(mainSeparator);

        foreach (KeyValuePair<string, List<Quest>> category in _player.GetAllQuests())
        {
            List<Quest> activeQuestsInCategory = category.Value.Where(q => q.GetActiveStatus()).ToList();

            if (activeQuestsInCategory.Count > 0)
            {
                hasActiveQuests = true;
                Console.WriteLine($"\n📋 {Utils.ToTitleCase(category.Key)} Quests:");
                Console.WriteLine(subSeparator);

                // Group eternal quests by frequency for better display
                if (category.Key == ETERNAL)
                {
                    var dailyQuests = activeQuestsInCategory.Where(q => q is EternalQuest eq && eq.GetFrequency() == "daily").ToList();
                    var weeklyQuests = activeQuestsInCategory.Where(q => q is EternalQuest eq && eq.GetFrequency() == "weekly").ToList();
                    var monthlyQuests = activeQuestsInCategory.Where(q => q is EternalQuest eq && eq.GetFrequency() == "monthly").ToList();

                    DisplayEternalQuestGroup(dailyQuests, "🌅 Daily Quests", "⏰");
                    DisplayEternalQuestGroup(weeklyQuests, "📅 Weekly Quests", "🗓️");
                    DisplayEternalQuestGroup(monthlyQuests, "🗓️ Monthly Quests", "📆");
                }
                else
                {
                    // Display simple and checklist quests
                    string icon = category.Key == SIMPLE ? "📝" : "✅";
                    foreach (Quest quest in activeQuestsInCategory)
                    {
                        Console.WriteLine($"  {icon} {quest.GetName()}");
                        Console.WriteLine($"     {quest.GetDescription()}");

                        // Show progress for checklist quests
                        if (quest is ChecklistQuest checklistQuest)
                        {
                            int current = checklistQuest.GetAmountOfTime();
                            int total = checklistQuest.GetTotalSteps();
                            double progress = current / total;
                            string progressBar = Utils.BuildProgressBar(progress, 20);
                            Console.WriteLine($"     Progress: [{progressBar}] {current}/{total}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        if (!hasActiveQuests)
        {
            Console.WriteLine("\n🎉 No active quests! You're all caught up!");
            Console.WriteLine("   Consider creating a custom quest or checking back later.");
        }

        Console.WriteLine(mainSeparator);
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }

    /// <summary>
    /// Helper method to display eternal quest groups with specific formatting.
    /// </summary>
    /// <param name="quests">List of quests in the frequency group.</param>
    /// <param name="groupTitle">Title for the group (e.g., "Daily Quests").</param>
    /// <param name="icon">Icon to display next to each quest.</param>
    private void DisplayEternalQuestGroup(List<Quest> quests, string groupTitle, string icon)
    {
        if (quests.Count > 0)
        {
            Console.WriteLine($"\n  {groupTitle}:");
            foreach (Quest quest in quests)
            {
                Console.WriteLine($"    {icon} {quest.GetName()}");
                Console.WriteLine($"       {quest.GetDescription()}");

                // Show last completion for eternal quests
                if (quest is EternalQuest eternalQuest)
                {
                    DateTime lastCompleted = eternalQuest.GetLastCompletedDate();
                    if (lastCompleted != DateTime.MinValue)
                    {
                        string timeAgo = GetTimeAgoString(lastCompleted);
                        Console.WriteLine($"       Last completed: {timeAgo}");
                    }
                    else
                    {
                        Console.WriteLine($"       Never completed");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// Finds a specific quest by category and name.
    /// </summary>
    /// <param name="category">The quest category (e.g., "simple", "checklist", "eternal").</param>
    /// <param name="questName">The name of the quest to find.</param>
    /// <returns>The Quest object if found, null otherwise.</returns>
    public Quest FindQuestByName(string category, string questName)
    {
        Dictionary<string, List<Quest>> allQuestsByCategory = _player.GetAllQuests();

        if (allQuestsByCategory.ContainsKey(category))
        {
            return allQuestsByCategory[category].Find(q => q.GetName() == questName);
        }

        return null;
    }

    /// <summary>
    /// Calls IsComplete on a specific quest identified by category and name.
    /// </summary>
    /// <param name="category">The quest category.</param>
    /// <param name="questName">The name of the quest.</param>
    /// <returns>True if quest was found and checked, false otherwise.</returns>
    public void CheckSpecificQuest(string category, string questName)
    {
        Quest quest = FindQuestByName(category, questName);

        if (quest != null)
        {
            quest.IsComplete(_player);
        }
        else
        {
            Console.WriteLine($"Quest '{questName}' not found");
        }
    }

    /// <summary>
    /// Gets a list of all active quest names of a given category for the player.
    /// </summary>
    /// <returns>List of active quest names of a certain category.</returns>
    public List<string> GetActiveQuestNamesPerCategory(string category)
    {
        List<string> activeQuestNames = new List<string>();
        Dictionary<string, List<Quest>> allQuestsByCategory = _player.GetAllQuests();

        foreach (Quest quest in allQuestsByCategory[category])
        {
            if (quest.GetActiveStatus() && !quest.GetIsCompletedStatus())
            {
                //if the quest is a simple quest and it is not an auto checkable or is not a simple quest add to the list 
                if ((quest is SimpleQuest simpleQuest && !simpleQuest.GetAutoCheck()) || category != SIMPLE)
                {
                    activeQuestNames.Add(quest.GetName());
                }
            }
        }

        return activeQuestNames;
    }

    /// <summary>
    /// Displays all completed quests for the player with improved formatting.
    /// </summary>
    public void DisplayCompletedQuests()
    {
        List<Quest> completedQuests = GetCompletedQuests();
        string mainSeparator = new string('=', 50);
        string subSeparator = new string('-', 30);

        Console.WriteLine(mainSeparator);
        Console.WriteLine("              COMPLETED QUESTS");
        Console.WriteLine(mainSeparator);

        if (completedQuests.Count == 0)
        {
            Console.WriteLine("\n🎯 No completed quests yet.");
            Console.WriteLine("   Start working on your active quests to see them here!");
        }
        else
        {
            // Group by category
            var questsByCategory = completedQuests.GroupBy(q =>
            {
                if (q is SimpleQuest) return SIMPLE;
                if (q is ChecklistQuest) return CHECKLIST;
                if (q is EternalQuest) return ETERNAL;
                return "unknown";
            });

            foreach (var category in questsByCategory)
            {
                Console.WriteLine($"\n🏆 {Utils.ToTitleCase(category.Key)} Quests Completed:");
                Console.WriteLine(subSeparator);

                if (category.Key == ETERNAL)
                {
                    var dailyCompleted = category.Where(q => q is EternalQuest eq && eq.GetFrequency() == "daily").ToList();
                    var weeklyCompleted = category.Where(q => q is EternalQuest eq && eq.GetFrequency() == "weekly").ToList();
                    var monthlyCompleted = category.Where(q => q is EternalQuest eq && eq.GetFrequency() == "monthly").ToList();

                    DisplayCompletedEternalGroup(dailyCompleted, "🌅 Daily Quests");
                    DisplayCompletedEternalGroup(weeklyCompleted, "📅 Weekly Quests");
                    DisplayCompletedEternalGroup(monthlyCompleted, "🗓️ Monthly Quests");
                }
                else
                {
                    string icon = category.Key == SIMPLE ? "✅" : "📝";
                    foreach (Quest quest in category)
                    {
                        Console.WriteLine($"  {icon} {quest.GetName()}");
                        Console.WriteLine($"     {quest.GetDescription()}");

                        // Show final stats for checklist quests
                        if (quest is ChecklistQuest checklistQuest)
                        {
                            int total = checklistQuest.GetTotalSteps();
                            Console.WriteLine($"     ✨ Completed all {total} steps!");
                        }
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine($"\n🎊 Total Completed: {completedQuests.Count} quests");
        }

        Console.WriteLine(mainSeparator);
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }

    /// <summary>
    /// Helper method to display completed eternal quest groups.
    /// </summary>
    /// <param name="quests">List of completed quests in the frequency group.</param>
    /// <param name="groupTitle">Title for the group.</param>
    private void DisplayCompletedEternalGroup(List<Quest> quests, string groupTitle)
    {
        if (quests.Count > 0)
        {
            Console.WriteLine($"\n  {groupTitle}:");
            foreach (Quest quest in quests)
            {
                Console.WriteLine($"    ✨ {quest.GetName()}");
                Console.WriteLine($"       {quest.GetDescription()}");

                if (quest is EternalQuest eternalQuest)
                {
                    DateTime lastCompleted = eternalQuest.GetLastCompletedDate();
                    if (lastCompleted != DateTime.MinValue)
                    {
                        Console.WriteLine($"       Last completed: {lastCompleted.ToString("MM/dd/yyyy")}");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// Gets a human-readable "time ago" string for a given date.
    /// </summary>
    /// <param name="date">The date to compare.</param>
    /// <returns>A string like "2 days ago" or "Yesterday".</returns>
    private string GetTimeAgoString(DateTime date)
    {
        TimeSpan timeDiff = DateTime.Now - date;
        
        if (timeDiff.Days == 0) return "Today";
        if (timeDiff.Days == 1) return "Yesterday";
        if (timeDiff.Days < 7) return $"{timeDiff.Days} days ago";
        if (timeDiff.Days < 30) return $"{timeDiff.Days / 7} weeks ago";
        
        return date.ToString("MM/dd/yyyy");
    }

    /// <summary>
    /// Gets a list of all completed quests for the player.
    /// </summary>
    /// <returns>List of completed Quest objects.</returns>
    public List<Quest> GetCompletedQuests()
    {
        List<Quest> completedQuests = new List<Quest>();
        Dictionary<string, List<Quest>> allQuestsByCategory = _player.GetAllQuests();

        foreach (KeyValuePair<string, List<Quest>> category in allQuestsByCategory)
        {
            completedQuests.AddRange(category.Value.FindAll(q => q.GetIsCompletedStatus()));
        }

        return completedQuests;
    }

    /// <summary>
    /// Verifies if all requirements for a quest are fulfilled based on completed quests.
    /// </summary>
    /// <param name="requirements">List of required quest names.</param>
    /// <param name="completedQuests">List of completed Quest objects.</param>
    /// <returns>True if all requirements are fulfilled, false otherwise.</returns>
    public bool VerifyQuestRequirements(List<string> requirements, List<Quest> completedQuests)
    {
        if (requirements.Count == 0)
            return true;

        foreach (string reqName in requirements)
        {
            Quest reqQuest = completedQuests.Find(q => q.GetName() == reqName);
            if (reqQuest == null || !reqQuest.GetIsCompletedStatus())
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Helper method to check profile-specific and quest requirements before activating a quest.
    /// </summary>
    /// <param name="conditional">Profile-specific condition for activation.</param>
    /// <param name="requirements">List of quest dependencies.</param>
    /// <param name="completedQuests">List of completed quests.</param>
    /// <param name="quest">The quest to potentially activate.</param>
    private void ActivateQuestCheck(bool conditional, List<string> requirements, List<Quest> completedQuests, Quest quest)
    {
        bool profileRequirements = false;

        if (conditional)
        {
            profileRequirements = true;
        }

        if (profileRequirements && VerifyQuestRequirements(requirements, completedQuests))
        {
            quest.SetActiveStatus(true);
        }
        else
        {
            quest.SetActiveStatus(false);
        }
    }

    /// <summary>
    /// Activates quests whose requirements and profile conditions are all fulfilled.
    /// Should be called after loading quests or when a quest is completed.
    /// </summary>
    public void ActivateQuest()
    {
        // Get all quests from the player's profile
        Dictionary<string, List<Quest>> allQuests = _player.GetAllQuests();
        List<Quest> completedQuests = GetCompletedQuests();

        foreach (KeyValuePair<string, List<Quest>> category in allQuests)
        {
            foreach (Quest quest in category.Value)
            {
                // Only try to activate inactive and incomplete quests
                if (!quest.GetActiveStatus() && !completedQuests.Contains(quest))
                {
                    // Get requirements (even an empty list)
                    List<string> requirements = quest.GetDependencies();
                    bool condition;
                    int age = _player.GetAge();
                    DateTime confirmationDate = _player.GetOrdinances()["confirmation"],
                             today = DateTime.Today,
                             recommendationDueDate = _player.GetRecommendation() ?? DateTime.MinValue;

                    if (category.Key == SIMPLE)
                    {
                        switch (quest.GetName())
                        {
                            case "LDS account":
                                condition = _player.GetAge() > 12;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "FamilySearch account":
                                condition = true;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Add info to FamilySearch (1)":
                                condition = true;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Add info to FamilySearch (2)":
                                condition = true;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Add info to FamilySearch (3)":
                                condition = true;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Add info to FamilySearch (4)":
                                condition = true;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Enter the Temple":
                                condition = today > confirmationDate.AddYears(1) && today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Sealing to Eternity":
                                condition = _player.GetMaritalState() && today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Renovate Temple Recommendation":
                                condition = today > recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Online Tithe":
                                condition = _player.GetWorkStatus();
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Strength of Youth":
                                condition = age > 13 && age < 18;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Activities (YM/YW/Combined)":
                                condition = age > 13 && age < 18;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "YSAs Activities":
                                condition = age > 17 && age < 36 && _player.GetMaritalState() == false;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;
                        }
                    }
                    else if (category.Key == CHECKLIST)
                    {
                        switch (quest.GetName())
                        {
                            case "Attend the Temple":
                                condition = today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Study Patriarchal Blessing":
                                condition = _player.GetPatriarchalBlessingStatus();
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;
                        }
                    }
                    else if (category.Key == ETERNAL)
                    {
                        switch (quest.GetName())
                        {
                            case "Index Family Records":
                                condition = true;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Serve in Your Calling":
                                condition = _player.GetCalling().Count > 0;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Family Tree":
                                condition = true;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Attend Seminary":
                                condition = age > 13 && age < 18;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Attend Institute":
                                condition = age > 17 && age < 36;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Tithe and Offerings":
                                condition = _player.GetWorkStatus();
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Initial Vicarious Ordinances":
                                condition = !_player.GetOrdinances().ContainsKey("initiatory and endowment") && today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Temple and Vicarious Work":
                                condition = today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;
                        }
                    }
                }
                else if (quest.GetActiveStatus() && !completedQuests.Contains(quest))
                {
                    List<string> requirements = quest.GetDependencies();
                    bool condition;
                    int age = _player.GetAge();
                    DateTime confirmationDate = _player.GetOrdinances()["confirmation"],
                             today = DateTime.Today,
                             recommendationDueDate = _player.GetRecommendation() ?? DateTime.MinValue;

                    if (category.Key == SIMPLE)
                    {
                        switch (quest.GetName())
                        {
                            case "Sealing to Eternity":
                                condition = _player.GetMaritalState() && today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Renovate Temple Recommendation":
                                condition = today > recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Strength of Youth":
                                condition = age > 13 && age < 18;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Activities (YM/YW/Combined)":
                                condition = age > 13 && age < 18;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "YSAs Activities":
                                condition = age > 17 && age < 36 && _player.GetMaritalState() == false;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;
                        }
                    }
                    else if (category.Key == ETERNAL)
                    {
                        switch (quest.GetName())
                        {
                            case "Serve in Your Calling":
                                condition = _player.GetCalling().Count > 0;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Attend Seminary":
                                condition = age > 13 && age < 18;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Attend Institute":
                                condition = age > 17 && age < 36;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Tithe and Offerings":
                                condition = _player.GetWorkStatus();
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Initial Vicarious Ordinances":
                                condition = !_player.GetOrdinances().ContainsKey("initiatory and endowment") && today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Temple and Vicarious Work":
                                condition = today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;
                        }
                    }
                }
            }
        }
    }

    public void CreateCustomQuest()
    {
        Console.WriteLine($"Hello {_player.GetName()}! I see that you want to create a new quest for you. That's awesome! The First step is to select the type of the quest, do you want to create a simple, a checklist or a eternal quest?");
        string category = Utils.DecisionString(new List<string>() { SIMPLE, CHECKLIST, ETERNAL });
        (string name, string description) questInfo;
        int nextLevelXP = _player.CalculateNextLevelXP();
        Quest quest = null;
        switch (category)
        {
            case SIMPLE:
                questInfo = EnterBasicInfo();
                quest = new SimpleQuest(questInfo.name, questInfo.description, true, false, nextLevelXP, new List<string>());
                break;

            case CHECKLIST:
                questInfo = EnterBasicInfo();
                int total = Utils.ReadInt("Enter the amount of repetition for this quest: ");
                quest = new ChecklistQuest(questInfo.name, questInfo.description, true, nextLevelXP, 0, total, new List<string>());
                break;

            case ETERNAL:
                questInfo = EnterBasicInfo();
                Console.WriteLine("What is the frequency of this quest: daily, weekly or monthly? ");
                string frequency = Utils.DecisionString(new List<string>() { "daily", "weekly", "monthly" });
                quest = new EternalQuest(questInfo.name, questInfo.description, frequency, true, nextLevelXP, DateTime.Now, new List<string>());
                break;
        }
        _player.AddCustomQuest(quest, category);
    }

    /// <summary>
    /// Prompts the user to enter basic quest information.
    /// </summary>
    /// <returns>A tuple containing the quest name and description.</returns>
    public (string name, string description) EnterBasicInfo()
    {
        string name = Utils.ValidStringInput("Enter the quest short name: ");
        string description = Utils.ValidStringInput("Enter the quest description: ");
        
        return (name, description);
    }
}
