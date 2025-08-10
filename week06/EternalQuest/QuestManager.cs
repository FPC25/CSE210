using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class QuestManager
{
    private const string
        SIMPLEPATH = "./Quests/SimpleQuests.json",
        CHECKLISTPATH = "./Quests/ChecklistQuests.json",
        DAILYPATH = "./Quests/DailyEternalQuests.json",
        WEEKLYPATH = "./Quests/WeeklyEternalQuests.json",
        MONTHLYPATH = "./Quests/MonthlyEternalQuests.json";

    private Profile _player;

    public QuestManager(Profile player)
    {
        _player = player;
    }

    /// <summary>
    /// Loads quests from a JSON file and returns a list of Quest objects.
    /// </summary>
    /// <param name="filepath">Path to the JSON file.</param>
    /// <param name="questType">Type of quest to instantiate ("simple", "checklist", "eternal").</param>
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
                foreach (string item in reqProp.EnumerateArray())
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
                    quests.Add(new ChecklistQuest(name, description, active, xpNextLevel, steps, total,  requirements));
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
    public List<Quest> GetDailyQuests()
    {
        return LoadQuestsFromJson(DAILYPATH, "eternal");
    }

    /// <summary>
    /// Gets all weekly eternal quests.
    /// </summary>
    public List<Quest> GetWeeklyQuests()
    {
        return LoadQuestsFromJson(WEEKLYPATH, "eternal");
    }

    /// <summary>
    /// Gets all monthly eternal quests.
    /// </summary>
    public List<Quest> GetMonthlyQuests()
    {
        return LoadQuestsFromJson(MONTHLYPATH, "eternal");
    }

    /// <summary>
    /// Gets all simple quests.
    /// </summary>
    public List<Quest> GetSimpleQuests()
    {
        return LoadQuestsFromJson(SIMPLEPATH, "simple");
    }

    /// <summary>
    /// Gets all checklist quests.
    /// </summary>
    public List<Quest> GetChecklistQuests()
    {
        return LoadQuestsFromJson(CHECKLISTPATH, "checklist");
    }

    /// <summary>
    /// Displays all active quests for the player.
    /// </summary>
    public void DisplayActiveQuests()
    {
        foreach (string category in _player.GetAllQuests())
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

    public List<Quest> GetCompletedQuests()
    {
        List<Quest> completedQuests = new List<Quest>();
        var allQuestsByCategory = _player.GetAllQuests();

        foreach (var category in allQuestsByCategory)
        {
            completedQuests.AddRange(category.Value.FindAll(q => q.GetIsCompletedStatus()));
        }

        return completedQuests;
    }

    public bool VerifyQuestRequirements(List<string> requirements)
    {
        if (requirements.Count == 0)
            return true;

        List<Quest> completedQuests = GetCompletedQuests();
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
    /// Activates quests whose requirements are all completed.
    /// Should be called after loading quests or when a quest is completed.
    /// </summary>
    public void ActivateQuest()
    {
        // Get all quests from the player's profile
        List<Quest> allQuests = _player.GetAllQuests();

        foreach (string category in allQuests)
        {
            foreach (Quest quest in category.Value)
            {
                // Only try to activate inactive and incomplete quests
                if (!quest.GetActiveStatus() && !quest.GetIsCompletedStatus())
                {
                    // Get requirements (even a empty list)
                    List<string> requirements = quest.GetDependencies();

                    if (category == "simple")
                    {
                        switch(quest.GetName())
                        {
                            case "FamilySearch account":
                                break;
                            
                            case "Enter the Temple":
                                break;

                            case "Sealing to Eternity":
                                break;

                            case "Renovate Temple Recommendation":
                                break;

                            case "Online Tithe":
                                break;

                            case "Add info to FamilySearch (1)":
                                break;

                            case "Add info to FamilySearch (2)":
                                break;

                            case "Add info to FamilySearch (3)":
                                break;

                            case "Add info to FamilySearch (4)":
                                break;

                            case "Strength of Youth":
                                break;

                            case "Activities (YM/YW/Combined)":
                                break;
                            
                            case "YSAs Activities":
                                break;
                        }
                    }
                    else if (category == "checklist")
                    {
                        switch(quest.GetName())
                        {
                            case "Attend the Temple":
                                break;
                            
                            case "Study Patriarchal Blessing":
                                break;
                        }
                    }
                    else if (category == "eternal")
                    {
                        switch(quest.GetName())
                        {
                            case "Index Family Records":
                                break;
                            
                            case "Serve in Your Calling":
                                break;

                            case "Family Tree":
                                break;

                            case "Attend Seminary":
                                break;

                            case "Attend Institute":
                                break;

                            case "Tithe and Offerings":
                                break;

                            case "Temple and Vicarious Work":
                                break;
                        }
                    }

                    
                }
            }
        }
    }
}
