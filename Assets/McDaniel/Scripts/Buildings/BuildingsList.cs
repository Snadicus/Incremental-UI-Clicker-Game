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
        public int baseCost;
        public float baseSpeed;
        public int baseIncome;
        public ResourceTracker resourceTracker;

        public void StartProduction(MonoBehaviour runner)
        {
            runner.StartCoroutine(GainIncome());
        }

        public IEnumerator GainIncome()
        {
            while (level >= 1)
            {
                if (resourceTracker != null)
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
            cost = Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(upgradeCostMultiplier, level - 1));
            return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(upgradeCostMultiplier, level - 1));
        }
    }
    #endregion



    public List<BuildingData> buildings;
    // List of buildings
    #region
    void Awake()
    {
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
                    baseCost = 100,
                    baseSpeed = 8,
                    baseIncome = 15,
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
                    baseCost = 1000,
                    baseSpeed = 20,
                    baseIncome = 1,
                    resourceTracker = resourceTracker
                },

                new BuildingData
                {
                    name = "ManaPool",
                    buyType = ResourceTracker.resources.gem,
                    produceType = ResourceTracker.resources.mana,
                    upgradeType = ResourceTracker.resources.gold,
                    permanent = Permanent.permanent,
                    unlocked = false,
                    level = 0,
                    cost = 1500,
                    baseUpgradeCost = 2000,
                    upgradeCostMultiplier = 1.1f,
                    income = 1,
                    speed = 2,
                    baseCost = 1500,
                    baseSpeed = 2,
                    baseIncome = 1,
                    resourceTracker = resourceTracker
                },

                new BuildingData
                {
                    name = "GuildHall",
                    buyType = ResourceTracker.resources.gem,
                    produceType = ResourceTracker.resources.gold,
                    upgradeType = ResourceTracker.resources.gold,
                    permanent = Permanent.permanent,
                    unlocked = false,
                    level = 0,
                    cost = 2000,
                    baseUpgradeCost = 2250,
                    upgradeCostMultiplier = 1.15f,
                    income = 45,
                    speed = 30,
                    baseCost = 2000,
                    baseSpeed = 30,
                    baseIncome = 45,
                    resourceTracker = resourceTracker
                }
            };
        }

        foreach (var b in buildings)
        {
            b.resourceTracker = this.resourceTracker;
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
                if (enemySpawner.currentArea >= 2 || enemySpawner.loop >= 2)
                {
                    building.unlocked = true;
                }
            }
        }
    }

    #endregion

    // PrestigeProgress
    #region

    // Resets some progress for prestige
    public void PrestigeProgress()
    {
        foreach (var building in buildings)
        {
            if (building.permanent == Permanent.permanent && building.upgradeType == ResourceTracker.resources.gem)
                continue;
            else if(building.permanent == Permanent.permanent)
            {

            }

            building.level = 0;
            building.cost = building.baseCost;
            building.income = building.baseIncome;
            building.speed = building.baseSpeed;
        }
    }

    #endregion
}