using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// The SaveLoadService class handles saving and loading player profiles and quest data.
/// It manages file operations for the Eternal Quest save system, storing data in JSON format.
/// </summary>
public static class SaveLoadService
{
    private const string SAVES_DIRECTORY = "./Saves";
    private const string SAVE_EXTENSION = ".json";

    /// <summary>
    /// Ensures the Saves directory exists.
    /// </summary>
    private static void EnsureSavesDirectoryExists()
    {
        if (!Directory.Exists(SAVES_DIRECTORY))
        {
            Directory.CreateDirectory(SAVES_DIRECTORY);
        }
    }

    /// <summary>
    /// Gets all available save files.
    /// </summary>
    /// <returns>List of save file names without extension.</returns>
    public static List<string> GetAvailableSaves()
    {
        EnsureSavesDirectoryExists();
        List<string> saves = new List<string>();
        
        string[] files = Directory.GetFiles(SAVES_DIRECTORY, "*" + SAVE_EXTENSION);
        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            saves.Add(fileName);
        }
        
        return saves;
    }

    /// <summary>
    /// Saves a player profile and quest manager to a file.
    /// </summary>
    /// <param name="player">The player profile to save.</param>
    /// <param name="questManager">The quest manager to save.</param>
    /// <param name="saveName">The name for the save file.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    public static bool SaveGame(Profile player, QuestManager questManager, string saveName)
    {
        try
        {
            EnsureSavesDirectoryExists();
            
            // Usar diretamente o método da classe Profile
            var profileData = player.DataToSaveDict();
            
            // Create save data structure
            var saveData = new Dictionary<string, object>
            {
                ["playerData"] = profileData,
                ["saveDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ["gameVersion"] = "1.0"
            };

            // Convert to JSON
            string jsonString = JsonSerializer.Serialize(saveData, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });

            // Write to file
            string filePath = Path.Combine(SAVES_DIRECTORY, saveName + SAVE_EXTENSION);
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine($"Game saved successfully as '{saveName}'!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving game: {ex.Message}");
            return false;
        }
    } 

    /// <summary>
    /// Loads a player profile and creates a quest manager from a save file.
    /// </summary>
    /// <param name="saveName">The name of the save file to load.</param>
    /// <returns>A tuple containing the loaded Profile and QuestManager, or null if loading failed.</returns>
    public static (Profile player, QuestManager questManager)? LoadGame(string saveName)
    {
        try
        {
            string filePath = Path.Combine(SAVES_DIRECTORY, saveName + SAVE_EXTENSION);
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Save file '{saveName}' not found!");
                return null;
            }

            // Read and parse JSON
            string jsonString = File.ReadAllText(filePath);
            var saveData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString);

            // Load player data
            Profile player = LoadPlayerFromJson(saveData["playerData"]);
            
            // Load quest data
            LoadQuestDataFromJson(player, saveData["questData"]);
            
            // Create quest manager
            QuestManager questManager = new QuestManager(player);

            Console.WriteLine($"Game '{saveName}' loaded successfully!");
            return (player, questManager);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading game: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Loads a Profile object from JSON data.
    /// </summary>
    /// <param name="playerJson">JSON element containing player data.</param>
    /// <returns>Reconstructed Profile object.</returns>
    private static Profile LoadPlayerFromJson(JsonElement playerJson)
    {
        string name = playerJson.GetProperty("name").GetString();
        int age = playerJson.GetProperty("age").GetInt32();
        bool male = playerJson.GetProperty("male").GetBoolean();
        
        // Calculate birthday from age (approximate)
        DateTime birthday = DateTime.Now.AddYears(-age);
        
        // Load ordinances
        var ordinancesJson = playerJson.GetProperty("ordinances");
        var ordinances = new Dictionary<string, DateTime>();
        foreach (var prop in ordinancesJson.EnumerateObject())
        {
            ordinances[prop.Name] = DateTime.Parse(prop.Value.GetString());
        }

        // Create profile
        Profile player = new Profile(name, birthday, age, male, ordinances);
        
        // Set other properties using reflection-like approach
        // Note: You'll need to add public setters or methods to set these values
        SetPlayerProperties(player, playerJson);
        
        return player;
    }

    /// <summary>
    /// Sets player properties from JSON data.
    /// </summary>
    /// <param name="player">The player profile to update.</param>
    /// <param name="playerJson">JSON data containing player properties.</param>
    private static void SetPlayerProperties(Profile player, JsonElement playerJson)
    {
        // Set XP and level
        int level = playerJson.GetProperty("level").GetInt32();
        int currentXP = playerJson.GetProperty("currentXP").GetInt32();
        player.SetLevel(level);
        player.SetXP(currentXP);
        
        // Set other boolean properties
        if (playerJson.GetProperty("married").GetBoolean())
            player.SetMarried(true);
        
        if (playerJson.GetProperty("working").GetBoolean())
            player.SetWorking(true);
            
        if (playerJson.GetProperty("patriarchalBlessing").GetBoolean())
            player.SetPatriarchalBlessing(true);

        // Set string properties
        string sacramentalTimeStr = playerJson.GetProperty("sacramentalTime").GetString();
        if (!string.IsNullOrEmpty(sacramentalTimeStr))
        {
            player.SetSacramentalTime(TimeSpan.Parse(sacramentalTimeStr));
        }

        string recDateStr = playerJson.GetProperty("recommendationDueDate").GetString();
        if (!string.IsNullOrEmpty(recDateStr))
        {
            player.SetRecommendationDueDate(DateTime.Parse(recDateStr));
        }

        string priesthood = playerJson.GetProperty("priesthood").GetString();
        if (!string.IsNullOrEmpty(priesthood))
        {
            player.SetPriesthood(priesthood);
        }

        // Set accounts
        player.SetLdsAccount(playerJson.GetProperty("ldsAccount").GetString());
        player.SetFamilysearchLink(playerJson.GetProperty("familysearchLink").GetString());

        // Set callings
        var callingsJson = playerJson.GetProperty("callings");
        foreach (var calling in callingsJson.EnumerateArray())
        {
            player.AddCallingDirect(calling.GetString());
        }
    }

    /// <summary>
    /// Loads quest data from JSON into the player profile.
    /// </summary>
    /// <param name="player">The player profile to update.</param>
    /// <param name="questJson">JSON data containing quest information.</param>
    private static void LoadQuestDataFromJson(Profile player, JsonElement questJson)
    {
        var questDict = player.GetAllQuests();
        questDict.Clear(); // Clear existing quests

        foreach (var category in questJson.EnumerateObject())
        {
            var questList = new List<Quest>();
            
            foreach (var questElement in category.Value.EnumerateArray())
            {
                Quest quest = CreateQuestFromJson(questElement, player.CalculateNextLevelXP());
                if (quest != null)
                {
                    questList.Add(quest);
                }
            }
            
            questDict[category.Name] = questList;
        }
    }

    /// <summary>
    /// Creates a Quest object from JSON data.
    /// </summary>
    /// <param name="questJson">JSON element containing quest data.</param>
    /// <param name="nextLevelXP">Player's next level XP for quest creation.</param>
    /// <returns>Reconstructed Quest object.</returns>
    private static Quest CreateQuestFromJson(JsonElement questJson, int nextLevelXP)
    {
        string type = questJson.GetProperty("type").GetString();
        string name = questJson.GetProperty("name").GetString();
        string description = questJson.GetProperty("description").GetString();
        bool active = bool.Parse(questJson.GetProperty("active").GetString());
        bool completed = bool.Parse(questJson.GetProperty("completed").GetString());

        Quest quest = null;

        switch (type)
        {
            case "simple":
                quest = new SimpleQuest(name, description, active, false, nextLevelXP, new List<string>());
                break;

            case "checklist":
                int steps = int.Parse(questJson.GetProperty("steps").GetString());
                int total = int.Parse(questJson.GetProperty("total").GetString());
                quest = new ChecklistQuest(name, description, active, nextLevelXP, steps, total, new List<string>());
                break;

            case "eternal":
                string frequency = questJson.GetProperty("frequency").GetString();
                DateTime initialDate = DateTime.Parse(questJson.GetProperty("initialDate").GetString());
                DateTime lastCompleted = DateTime.Parse(questJson.GetProperty("lastCompletedDate").GetString());
                quest = new EternalQuest(name, description, frequency, active, nextLevelXP, initialDate, new List<string>());
                
                // Set last completed date
                if (lastCompleted != DateTime.MinValue)
                {
                    ((EternalQuest)quest).SetLastCompletedDate(lastCompleted);
                }
                break;
        }

        // Set completion status
        if (quest != null && completed)
        {
            quest.ForceComplete();
        }

        return quest;
    }

    /// <summary>
    /// Deletes a save file.
    /// </summary>
    /// <param name="saveName">Name of the save file to delete.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    public static bool DeleteSave(string saveName)
    {
        try
        {
            string filePath = Path.Combine(SAVES_DIRECTORY, saveName + SAVE_EXTENSION);
            
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Save '{saveName}' deleted successfully!");
                return true;
            }
            else
            {
                Console.WriteLine($"Save '{saveName}' not found!");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting save: {ex.Message}");
            return false;
        }
    }
}