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
    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/saveFile/";
        LoadGame();
        Debug.Log("File path: " + saveFilePath);
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

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    // SaveGame
    #region

    // Saves the game when called. Will save any variable listed.
    public void SaveGame()
    {
        SaveData data = new SaveData();

        string dataFilePath = saveFilePath + "buildings.json"; 

        // Taken from BuildingLists
        foreach (var building in buildingsList.buildings)
        {
            BuildingSaveData b = new BuildingSaveData();
            b.building = building;

            data.buildings.Add(b);
        }

        string json = JsonUtility.ToJson(data.buildings, true);

        File.WriteAllText(dataFilePath, json);

        dataFilePath = saveFilePath + "abilities.json";

        // Taken from PlayerAbilities
        foreach (var ability in playerAbilities.abilities)
        {
            AbilitySaveData a = new AbilitySaveData();

            a.ability = ability;

            data.abilities.Add(a);
        }

        json = JsonUtility.ToJson(data.abilities, true);

        File.WriteAllText (dataFilePath, json);

        dataFilePath = saveFilePath + "teammates.json";

        // Taken from TeammateManager
        foreach (var teammate in teammateManager.teammates)
        {
            TeammateSaveData t = new TeammateSaveData();

            t.teammate = teammate;

            data.teammates.Add(t);
        }

        json = JsonUtility.ToJson(data.teammates, true);

        File.WriteAllText(dataFilePath, json);

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

        json = JsonUtility.ToJson(data, true);

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

        }
        catch (DirectoryNotFoundException)
        {

        }
        catch (FileNotFoundException) 
        {
            
        }
    }
    #endregion
}
