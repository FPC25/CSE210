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
class QuestManager
{
    // --- File Paths for Quest Data ---
    private const string
        SIMPLEPATH = "./Quests/SimpleQuests.json",
        CHECKLISTPATH = "./Quests/ChecklistQuests.json",
        DAILYPATH = "./Quests/DailyEternalQuests.json",
        WEEKLYPATH = "./Quests/WeeklyEternalQuests.json",
        MONTHLYPATH = "./Quests/MonthlyEternalQuests.json";

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
    /// <param name="questType">Type of quest to instantiate ("simple", "checklist", "eternal").</param>
    /// <returns>List of Quest objects loaded from the file.</returns>
    public List<Quest> LoadQuestsFromJson(string filepath, string questType)
    {
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
                case "simple":
                    description = q.GetProperty("description").GetString();
                    bool autoCheck = q.GetProperty("auto_check").GetBoolean();
                    quests.Add(new SimpleQuest(name, description, active, autoCheck, xpNextLevel, requirements));
                    break;

                case "checklist":
                    string initial = q.GetProperty("first_part_description").GetString();
                    string final = q.GetProperty("second_part_description").GetString();
                    int total = q.TryGetProperty("total", out var totalProp) ? totalProp.GetInt32() : 1;
                    int steps = q.TryGetProperty("steps", out var stepsProp) ? stepsProp.GetInt32() : 0;
                    description = $"{initial} {total} {final}";
                    quests.Add(new ChecklistQuest(name, description, active, xpNextLevel, steps, total, requirements));
                    break;

                case "eternal":
                    description = q.GetProperty("description").GetString();
                    string frequency = q.GetProperty("frequency").GetString();
                    quests.Add(new EternalQuest(name, description, frequency, active, xpNextLevel, DateTime.Now, requirements));
                    break;
            }
        }

        return quests;
    }

    /// <summary>
    /// Gets all daily eternal quests.
    /// </summary>
    /// <returns>List of daily eternal Quest objects.</returns>
    public List<Quest> GetDailyQuests() => LoadQuestsFromJson(DAILYPATH, "eternal");

    /// <summary>
    /// Gets all weekly eternal quests.
    /// </summary>
    /// <returns>List of weekly eternal Quest objects.</returns>
    public List<Quest> GetWeeklyQuests() => LoadQuestsFromJson(WEEKLYPATH, "eternal");

    /// <summary>
    /// Gets all monthly eternal quests.
    /// </summary>
    /// <returns>List of monthly eternal Quest objects.</returns>
    public List<Quest> GetMonthlyQuests() => LoadQuestsFromJson(MONTHLYPATH, "eternal");

    /// <summary>
    /// Gets all simple quests.
    /// </summary>
    /// <returns>List of simple Quest objects.</returns>
    public List<Quest> GetSimpleQuests() => LoadQuestsFromJson(SIMPLEPATH, "simple");

    /// <summary>
    /// Gets all checklist quests.
    /// </summary>
    /// <returns>List of checklist Quest objects.</returns>
    public List<Quest> GetChecklistQuests() => LoadQuestsFromJson(CHECKLISTPATH, "checklist");

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

        quests["simple"] = GetSimpleQuests();
        quests["checklist"] = GetChecklistQuests();
        quests["eternal"] = eternalQuests;
    }

    /// <summary>
    /// Displays all active quests for the player, grouped by category.
    /// </summary>
    public void DisplayActiveQuests()
    {
        foreach (KeyValuePair<string, List<Quest>> category in _player.GetAllQuests())
        {
            Console.WriteLine($"Category: {category.Key}");
            foreach (Quest quest in category.Value)
            {
                if (quest.GetActiveStatus())
                    Console.WriteLine(quest.GetDetailsString());
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

        Console.WriteLine($"Quest '{questName}' not found");
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
                //if the quest is a simple quest and it is not an autocheckable or is not a simple quest add to the list 
                if ((quest is SimpleQuest simpleQuest && !simpleQuest.GetAutoCheck()) || category != "simple")
                {
                    activeQuestNames.Add(quest.GetName());
                }
            }
        }

        return activeQuestNames;
    }

    /// <summary>
    /// Displays all completed quests for the player.
    /// </summary>
    public void DisplayCompletedQuests()
    {
        List<Quest> completedQuests = GetCompletedQuests();
        if (completedQuests.Count == 0)
        {
            Console.WriteLine("No completed quests yet.");
        }

        foreach (Quest quest in completedQuests)
        {
            Console.WriteLine(quest.GetDetailsString());
        }
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

                    if (category.Key == "simple")
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
                                condition = age > 17 && age < 36;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;
                        }
                    }
                    else if (category.Key == "checklist")
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
                    else if (category.Key == "eternal")
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

                            case "Vicarious Baptism":
                                condition = (_player.GetPriesthood().ToLower() != "elder" || _player.GetPriesthood().ToLower() != "high priest") && today <= recommendationDueDate;
                                ActivateQuestCheck(condition, requirements, completedQuests, quest);
                                break;

                            case "Initial Vicarious Ordinances":
                                condition = _player.GetPriesthood().ToLower() == "elder" && !_player.GetOrdinances().ContainsKey("initiatory and endowment") && today <= recommendationDueDate;
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
}
