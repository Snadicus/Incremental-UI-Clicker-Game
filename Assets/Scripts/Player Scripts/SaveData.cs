[System.Serializable]
public class BuildingSaveData
{
    // From BuildingLists
    public string name;
    public int level;
    public bool unlocked;
}

[System.Serializable]
public class AbilitySaveData
{
    // From PlayerAbilites
    public string name;
    public int level;
}

[System.Serializable]
public class TeammateSaveData
{
    // From TeammateManager
    public string name;
    public int level;
}

[System.Serializable]
public class SaveData
{
    // For saving Buildings
    public List<BuildingSaveData> buildings = new List<BuildingSaveData>();

    // For saving Player Abilities
    public List<AbilitySaveData> buildings = new List<AbilitySaveData>();

    // For saving Teammates
    public List<TeammateSaveData> buildings = new List<TeammateSaveData>();

    // From ResourceTracker
    public int gold;
    public float mana;
    public int gem;
    public int divineFavor;
    public int prestigeLevel;

    // From Player


    // From EnemySpawner
    public int currentArea;
    public int loop;
}
