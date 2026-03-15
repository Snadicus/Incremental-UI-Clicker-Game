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

    // Area Data
    #region

    // Area Data
    [System.Serializable]
    public class areaData
    {
        public string areaName;

        public List<enemyData> enemies;

        // Gets a random enemy from the area
        public enemyData getRandomEnemy()
        {
            int index = Random.Range(0, enemies.Count-1);
            return enemies[index];
        }

        // Gets the boss when enemyCounter is filled
        public enemyData getBoss()
        {
            int index = 10;
            return enemies[index];
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
                    },

                    // Test boss enemy
                    new enemyData
                    {
                        name = "Test Boss",
                        baseHealth = 20f,
                        baseGold = 50
                    }
                }
            },

            // Area One: Forests of the Grenwald
            new areaData
            {
                areaName= "Forests of the Grenwald",

                enemies = new List<enemyData>()
                {
                    new enemyData
                    {
                        name = "Dire Rats",
                        baseHealth = 10f,
                        baseGold = 2
                    },

                    new enemyData
                    {
                        name = "Giant Bats",
                        baseHealth = 11f,
                        baseGold = 2
                    },

                    new enemyData
                    {
                        name = "Giant Centipedes",
                        baseHealth = 13f,
                        baseGold = 2
                    },

                    new enemyData
                    {
                        name = "Green Slimes",
                        baseHealth = 14f,
                        baseGold = 2
                    },

                    new enemyData
                    {
                        name = "Wolves",
                        baseHealth = 16f,
                        baseGold = 2
                    },

                    new enemyData
                    {
                        name = "Mossy Skeletons",
                        baseHealth = 17f,
                        baseGold = 2
                    },

                    new enemyData
                    {
                        name = "Goblin Hunters",
                        baseHealth = 19f,
                        baseGold = 3
                    },

                    new enemyData
                    {
                        name = "Goblin Warriors",
                        baseHealth = 21f,
                        baseGold = 3
                    },

                    new enemyData
                    {
                        name = "Bandits",
                        baseHealth = 22f,
                        baseGold = 3
                    },

                    new enemyData
                    {
                        name = "Black Bears",
                        baseHealth = 24f,
                        baseGold = 3
                    },

                    // Boss
                    new enemyData
                    {
                        name = "Goblin War Chief",
                        baseHealth = 238f,
                        baseGold = 30
                    }
                }
            },

            
            // Area Two: The Forgotten Fortress of Festung
            new areaData
            {
                areaName= "The Forgotten Fortress of Festung",

                enemies = new List<enemyData>()
                {
                    new enemyData
                    {
                        name = "Rat Swarms",
                        baseHealth = 26f,
                        baseGold = 3
                    },

                    new enemyData
                    {
                        name = "Skeletal Guard Dogs",
                        baseHealth = 27f,
                        baseGold = 3
                    },

                    new enemyData
                    {
                        name = "Shrieker Fungi",
                        baseHealth = 29f,
                        baseGold = 4
                    },

                    new enemyData
                    {
                        name = "Giant Spiders",
                        baseHealth = 31f,
                        baseGold = 4
                    },

                    new enemyData
                    {
                        name = "Giant Scorpions",
                        baseHealth = 32f,
                        baseGold = 4
                    },

                    new enemyData
                    {
                        name = "Zombie Goblins",
                        baseHealth = 34f,
                        baseGold = 4
                    },

                    new enemyData
                    {
                        name = "Skeleton Guards",
                        baseHealth = 36f,
                        baseGold = 4
                    },

                    new enemyData
                    {
                        name = "Zombie Bandits",
                        baseHealth = 38f,
                        baseGold = 4
                    },

                    new enemyData
                    {
                        name = "Ghouls",
                        baseHealth = 40f,
                        baseGold = 4
                    },

                    new enemyData
                    {
                        name = "Spectres",
                        baseHealth = 42f,
                        baseGold = 5
                    },

                    // Boss
                    new enemyData
                    {
                        name = "Ancient Wraith",
                        baseHealth = 420f,
                        baseGold = 46
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
