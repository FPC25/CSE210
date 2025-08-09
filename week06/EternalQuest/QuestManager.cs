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
            string name = q.GetProperty("name").GetString() ?? "";
            bool active = q.TryGetProperty("active", out var activeProp) ? activeProp.GetBoolean() : true;
            int xpNextLevel = _player.CalculateNextLevelXP();

            switch (questType)
            {
                case "simple":
                    description = q.GetProperty("description").GetString() ?? "";
                    bool autoCheck = q.TryGetProperty("auto_check", out var autoCheckProp) ? activeProp.GetBoolean() : false;
                    quests.Add(new SimpleQuest(name, description, active, autoCheck, xpNextLevel));
                    break;
                case "checklist":
                    string initial = q.GetProperty("first_part_description").GetString() ?? "";
                    string final = q.GetProperty("second_part_description").GetString() ?? "";
                    int total = q.TryGetProperty("total", out var totalProp) ? totalProp.GetInt32() : 1;    
                    int steps = q.TryGetProperty("steps", out var stepsProp) ? stepsProp.GetInt32() : 0;
                    description = $"{initial} {total} {final}";
                    quests.Add(new ChecklistQuest(name, description, active, xpNextLevel, steps, total));
                    break;
                case "eternal":
                    description = q.GetProperty("description").GetString() ?? "";
                    string frequency = q.TryGetProperty("frequency", out var freqProp) ? freqProp.GetString() ?? "daily" : "daily";
                    quests.Add(new EternalQuest(name, description, frequency, active, xpNextLevel, DateTime.Now));
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
        foreach (var category in _player.GetActiveQuests())
        {
            Console.WriteLine($"Category: {category.Key}");
            foreach (var quest in category.Value)
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
        foreach (var category in _player.GetActiveQuests())
        {
            Console.WriteLine($"Category: {category.Key}");
            foreach (var quest in category.Value)
            {
                if (quest.GetIsCompletedStatus())
                    Console.WriteLine(quest.GetDetailsString());
            }
        }
    }
}