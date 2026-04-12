using System.IO;
using UnityEngine;

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
    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/saveFile.json";
    }

    #endregion

    // Update
    #region

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
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
            BuildingSaveData b = new BuildingSaveData();

            b.name = building.name;
            b.level = building.level;
            b.unlocked = building.unlocked;

            data.buildings.Add(b);
        }

        // Taken from PlayerAbilities
        foreach (var ability in playerAbilities.abilities)
        {
            AbilitySaveData a = new AbilitySaveData();

            a.name = ability.name;
            a.level = ability.level;

            data.abilities.Add(a);
        }

        // Taken from TeammateManager
        foreach (var teammate in teammateManager.teammates)
        {
            TeammateSaveData t = new TeammateSaveData();

            t.name = teammate.teammateType;
            t.level = teammate.level;

            data.teammates.Add(t);
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
        if (!File.Exists(saveFilePath)) return;

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

        // For Buildings
        foreach (var savedBuilding in data.buildings)
        {
            var building = buildingsList.GetBuildingData(savedBuilding.name);

            if (building != null)
            {
                building.level = savedBuilding.level;
                building.unlocked = savedBuilding.unlocked;
            }
        }

        // For Abilities
        foreach (var savedAbility in data.abilities)
        {
            var ability = playerAbilities.GetAbility(savedAbility.name);

            if (ability != null)
            {
                ability.level = savedAbility.level;
            }
        }

        // For Teammates
        foreach (var savedTeammate in data.teammates)
        {
            var teammate = teammateManager.GetTeammate(savedTeammate.name);

            if (teammate != null)
            {
                teammate.level = savedTeammate.level;
            }
        }
    }

    #endregion
}
