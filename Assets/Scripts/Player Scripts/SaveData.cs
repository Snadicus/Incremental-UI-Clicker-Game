using System;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    // Saving teammate List
    public List<TeammateManager.Teammates> teammatesData = new List<TeammateManager.Teammates>();

    // Saving buildingData
    public List<BuildingsList.BuildingData> buildingData = new List<BuildingsList.BuildingData>();

    // Saving ability data
    public List<PlayerAbilities.AbilityData> abilityData = new List<PlayerAbilities.AbilityData>();

    // From ResourceTracker
    public int gold;
    public float mana;
    public int gem;
    public int divineFavor;
    public int prestigeLevel;

    // From Player
    public int[] playerStats;
    public int[] playerCosts; 

    // From EnemySpawner
    public int currentArea;
    public int loop;
}
