using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    // Variables & References
    #region

    // Contains the file path for save data
    string saveFilePath;

    // Script References
    public ResourceTracker resourceTracker;
    public BuildingsList buildingsList;
    public TeammateManager teammateManager;
    public PlayerAbilities playerAbilities;
    public Player player;
    public EnemySpawner enemySpawner;

    #endregion

    // Awake
    #region

    // Called when loaded. Sets the save path.
    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/saveFile.json";
        LoadGame();

        //Debug.Log("File path: " + saveFilePath);
    }

    #endregion

    // Update
    #region

    void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SaveGame();
            Debug.Log("Game Saved!");
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadGame();
            Debug.Log("Game Loaded!");
        }
    }

    #endregion

    // SaveGame
    #region

    // Saves the game when called. Will save any variable listed.
    public void SaveGame()
    {
        SaveData data = new SaveData();

        // Taken from BuildingLists
        foreach (var building in buildingsList.buildings)
        {

            data.buildingData.Add(building);
        }

        // Taken from PlayerAbilities
        foreach (var ability in playerAbilities.abilities)
        {
            data.abilityData.Add(ability);
        }

        // Taken from TeammateManager
        foreach (var teammate in teammateManager.teammates)
        {
            data.teammatesData.Add(teammate);
        }

        // Taken from ResourceTracker
        data.currentArea = enemySpawner.currentArea;
        data.loop = enemySpawner.loop;

        // Taken from EnemySpawner
        data.gold = resourceTracker.gold;
        data.mana = resourceTracker.mana;
        data.gem = resourceTracker.gem;
        data.divineFavor = resourceTracker.divineFavor;
        data.prestigeLevel = resourceTracker.prestigeLevel;

        // Taken from Player
        data.playerStats = player.stats;
        data.playerCosts = player.statsCost;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved to: " + saveFilePath);
    }

    #endregion

    // LoadGame
    #region

    // Loads the game when called
    public void LoadGame() 
    {
        try
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // For Player Stats
            player.stats = data.playerStats;

            // For Resources
            resourceTracker.gold = data.gold;
            resourceTracker.mana = data.mana;
            resourceTracker.gem = data.gem;
            resourceTracker.divineFavor = data.divineFavor;
            resourceTracker.prestigeLevel = data.prestigeLevel;

            // For Area
            enemySpawner.currentArea = data.currentArea;
            enemySpawner.loop = data.loop;

            // Clear data the gets created at awake
            buildingsList.buildings.Clear();
            playerAbilities.abilities.Clear();
            teammateManager.teammates.Clear();

            for (int i=0; i < data.playerStats.Length; i++)
            {
                player.stats[i] = data.playerStats[i];
            }
            for (int i = 0; i < data.playerStats.Length; i++)
            {
                player.statsCost[i] = data.playerCosts[i];
            }

            // For Buildings
            foreach (var savedBuilding in data.buildingData)
            {
                savedBuilding.resourceTracker = resourceTracker;
                buildingsList.buildings.Add(savedBuilding);
                if (savedBuilding.level > 0)
                {
                    StartCoroutine(savedBuilding.GainIncome());
                }
            }

            // For Abilities
            foreach (var savedAbility in data.abilityData)
            {
                playerAbilities.abilities.Add(savedAbility);
            }

            // For Teammates
            foreach (var savedTeammate in data.teammatesData)
            {
                savedTeammate.enemySpawner = enemySpawner;
                teammateManager.teammates.Add(savedTeammate);
                if (savedTeammate.level > 0)
                {
                    StartCoroutine(savedTeammate.Attack());
                }
            }
        }
        catch (FileNotFoundException)
        {
            
        }
    }

    #endregion

    // OnApplicationQuit
    #region
    void OnApplicationQuit()
    {
        SaveGame();
    }
    #endregion
}
