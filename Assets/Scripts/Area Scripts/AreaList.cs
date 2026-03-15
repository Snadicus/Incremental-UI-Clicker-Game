using System.Collections.Generic;
using UnityEngine;

public class AreaList : MonoBehaviour
{
    // Enemy Data
    #region

    // Enemy data
    [System.Serializable]
    public class enemyData
    {
        public string name;
        public float baseHealth;
        public int baseGold;
    }

    #endregion

    // Boss Data
    #region

    // Boss data
    [System.Serializable]
    public class bossData
    {
        public string name;
        public float baseHealth;
        public int baseGold;
    }
    #endregion

    // Area Data
    #region

    // Area Data
    [System.Serializable]
    public class areaData
    {
        public string areaName;
        public float healthMultiplier;
        public float goldMultiplier;
        public int enemyMultiplier;

        public List<enemyData> enemies;

        public List<bossData> boss;

        // Gets a random enemy from the area
        public enemyData getRandomEnemy()
        {
            int index = Random.Range(0, enemies.Count);
            return enemies[index];
        }

        // Gets the boss when enemyCounter is filled.=
        public bossData getBoss()
        {
            int index = 0;
            return boss[index];
        }
    }

    #endregion

    // List of Areas and imbedded Enemy Lists
    #region

    // List of areas
    public List<areaData> areas;

    void Awake()
    {
        areas = new List<areaData>()
        {
            // Testing area. Do not use in final game.
            new areaData
            {
                areaName = "Test",
                healthMultiplier = Random.Range(0.9f, 1.1f+0.1f),
                goldMultiplier = Random.Range(0.9f, 1.1f+0.1f),
                enemyMultiplier = Random.Range(1, 3+1),

                enemies = new List<enemyData>()
                {
                    // Basic test enemy
                    new enemyData
                    {
                        name = "Test Enemies",
                        baseHealth = 1f,
                        baseGold = 1
                    },
                    
                    // Test enemy with lots of health
                    new enemyData
                    {
                        name = "Tanky Test Enemies",
                        baseHealth = 10,
                        baseGold = 2
                    }
                },

                boss = new List<bossData>()
                {
                    // Test boss enemy
                    new bossData
                    {
                        name = "Test Boss",
                        baseHealth = 20f,
                        baseGold = 50
                    }
                }
            }
        };
    }

    #endregion

     // Give away area data
    #region

    // Give current area data
    public areaData getArea(int index)
    {
        return areas[index];
    }

    #endregion
}
