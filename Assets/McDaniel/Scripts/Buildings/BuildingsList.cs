using NUnit.Framework;
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
        public int level;
        public int income;
        public float speed;
        public ResourceTracker resourceTracker;

        public IEnumerator GainIncome()
        {
            while (level >= 1)
            {
                resourceTracker.AddGold(income);
                yield return new WaitForSeconds(speed);
            }
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
                level = 0,
                income = 50,
                speed = 8
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
}
