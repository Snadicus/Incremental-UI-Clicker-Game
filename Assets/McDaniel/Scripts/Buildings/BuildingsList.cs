using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsList : MonoBehaviour
{
    public ResourceTracker resourceTracker;

    // Building Data
    #region
    [System.Serializable]
    public class BuildingData
    {
        public string name;
        public ResourceTracker.resources buyType;
        public ResourceTracker.resources produceType;
        public Permanent permanent;
        public int level;
        public int cost;
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
    }
    #endregion



    public List<BuildingData> buildings;
    // List of buildings
    #region
    void Awake()
    {
        buildings = new List<BuildingData>()
        {
            new BuildingData
            {
                name = "Bar",
                buyType = ResourceTracker.resources.gem,
                produceType = ResourceTracker.resources.gold,
                permanent = Permanent.temporary,
                level = 0,
                cost = 100,
                income = 15,
                speed = 8,
                resourceTracker = resourceTracker
            },

            new BuildingData
            {
                name = "GemMine",
                buyType = ResourceTracker.resources.gold,
                produceType = ResourceTracker.resources.gem,
                permanent = Permanent.permanent,
                level = 0,
                cost = 1000,
                income = 1,
                speed = 20,
                resourceTracker = resourceTracker
            }
        };
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
}
