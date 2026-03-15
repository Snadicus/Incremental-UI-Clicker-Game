using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsList : MonoBehaviour
{
    // Building Data
    #region
    [System.Serializable]
    public class BuildingData
    {
        public string name;
        public int level;
        public float income;
        public float speed;
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
    public BuildingData? GetBuildingData(string name)
    {
        int index = 0;
        foreach (BuildingData building in buildings)
        {
            if (building.name = name)
            {
                return buildings[index];
            }
            index++;
        }
        return null
    }
}
