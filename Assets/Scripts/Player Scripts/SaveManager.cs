using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Variables & References
    #region

    // Contains the file path for save data
    string saveFilePath;

    // Script References
    public ResourceTracker resources;
    public BuildingsList buildings;
    public TeammateManager teammates;
    public PlayerAbilities abilites;
    public Player stats;
    public EnemySpawner areas;

    #endregion

    // Awake
    #region

    // Called when loaded. Sets the save path.
    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/saveFile.json";
    }

    #endregion

    // Start
    #region

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveData data = LoadGame();

        if (data != null)
        {
            
        }
    }

    #endregion

    // SaveGame
    #region

    // Saves the game when called. Will save any variable listed.
    public void SaveGame()
    {
        SaveData data = new SaveData();

        // Taken from ResourceTracker


        // Taken from BuildingLists


        // Taken from EnemySpawner

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved to: " + saveFilePath);
    }

    #endregion

    // LoadGame
    #region

    // Loads the game when called
    public SaveData LoadGame() 
    { 
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data;
        }
        else
        {
            Debug.Log("No save found!");
            return null;
        }
    }

    #endregion
}
