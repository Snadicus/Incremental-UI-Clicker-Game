using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingsList : MonoBehaviour
{
    // Script References
    public ResourceTracker resourceTracker;
    public EnemySpawner enemySpawner;

    // Building Data
    #region
    [System.Serializable]
    public class BuildingData
    {
        public string name;
        public ResourceTracker.resources buyType;
        public ResourceTracker.resources produceType;
        public ResourceTracker.resources upgradeType;
        public Permanent permanent;
        public bool unlocked;
        public int level;
        public int cost;
        public int baseUpgradeCost;
        public float upgradeCostMultiplier;
        public int income;
        public float speed;
        public ResourceTracker resourceTracker;

        public IEnumerator GainIncome()
        {
            while (level >= 1)
            {
                resourceTracker.AddResource(produceType, income);
                yield return new WaitForSeconds(speed);
            }
        }

        public void IncreaseCost()
        {
            cost += Convert.ToInt32(cost * 0.5f);
        }

        public int GetUpgradeCost()
        {
            return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(upgradeCostMultiplier, level - 1));
        }
    }
    #endregion



    public List<BuildingData> buildings;
    // List of buildings
    #region
    void Awake()
    {
        Debug.Log(buildings.Count);
        if (buildings.Count == 0)
        {
            buildings = new List<BuildingData>()
            {
                new BuildingData
                {
                    name = "Bar",
                    buyType = ResourceTracker.resources.gold,
                    produceType = ResourceTracker.resources.gold,
                    upgradeType = ResourceTracker.resources.gold,
                    permanent = Permanent.temporary,
                    unlocked = true,
                    level = 0,
                    cost = 100,
                    baseUpgradeCost = 150,
                    upgradeCostMultiplier = 1.2f,
                    income = 15,
                    speed = 8,
                    resourceTracker = resourceTracker
                },

                new BuildingData
                {
                    name = "GemMine",
                    buyType = ResourceTracker.resources.gold,
                    produceType = ResourceTracker.resources.gem,
                    upgradeType = ResourceTracker.resources.gem,
                    permanent = Permanent.permanent,
                    unlocked = false,
                    level = 0,
                    cost = 1000,
                    baseUpgradeCost = 20,
                    upgradeCostMultiplier = 1.2f,
                    income = 1,
                    speed = 20,
                    resourceTracker = resourceTracker
                }
            };
        }
    }
    #endregion

    // Give related building for upgrades
    #region
    public BuildingData? GetBuildingData(string name)
    {
        int index = 0;
        foreach (BuildingData building in buildings)
        {
            if (building.name == name)
            {
                return buildings[index];
            }
            index++;
        }
        return null;
    }
    #endregion
    
    // Enum for whether building is permanent or not
    public enum Permanent
    {
        temporary,
        permanent
    }

    // UnlockBuildings
    #region 

    // Called whenever an area is finished to check if a building is unlocked
    public void UnlockBuildings()
    {
        foreach (BuildingData building in buildings)
        {
            if (building.name == "GemMine" && !building.unlocked)
            {
                if (enemySpawner.currentArea >= 2)
                {
                    building.unlocked = true;
                }
            }
        }
    }

    #endregion
}
