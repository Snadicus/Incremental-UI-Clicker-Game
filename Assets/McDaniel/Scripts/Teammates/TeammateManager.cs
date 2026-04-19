using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class TeammateManager : MonoBehaviour
{
    // Variables
    #region

    // Script reference
    public EnemySpawner enemySpawner;
    #endregion

    // Teammates Systems and setup
    #region
    [System.Serializable]
    public class Teammates
    {
        // Stats of teammates.
        public string teammateType;
        public int level;
        public int equipment;
        // Damage delt
        public float attackPower;
        // Time between next attack. Lower is faster
        public float attackSpeed;
        public int cost;
        [System.Xml.Serialization.XmlIgnore] public EnemySpawner enemySpawner;
        public ResourceTracker.resources buyType;

        // Attacking first enemy in enemy list, repeating for x amount of seconds which is based on attack speed
        public IEnumerator Attack()
        {
            // Attacks enemies
            while (level >= 1)
            {
                enemySpawner.EnemyAttacked(attackPower);
                yield return new WaitForSeconds(attackSpeed);
            }
        }

        public void IncreaseCost()
        {
            cost += Convert.ToInt32(cost * 0.5f);
        }
    }
    #endregion

    public List<Teammates> teammates;

    // Creating teammate types. Add as much as we need/please
    #region
    void Awake()
    {
        string filePath = Application.persistentDataPath + "/Teammate_Data/";
        string dataPath = filePath + "Teammates.xml";
        try
        {
            Debug.Log("Doing This");
            var xmlSerializer = new XmlSerializer(typeof(List<Teammates>));

            using (FileStream stream = File.OpenRead(dataPath))
            {
                var teammateList = (List<Teammates>)xmlSerializer.Deserialize(stream);
                foreach (var teammate in teammateList)
                {
                    teammates.Add(teammate);
                }
            }
        }
        catch (DirectoryNotFoundException)
        {
            Debug.Log("Doing That");
            Directory.CreateDirectory(filePath);
            teammates = new List<Teammates>()
            {
                new Teammates
                {
                    teammateType = "Archer",
                    level = 0,
                    equipment = 0,
                    attackPower = 1,
                    attackSpeed = 2,
                    enemySpawner = enemySpawner,
                    buyType = ResourceTracker.resources.gold,
                    cost = 5
                },
                new Teammates
                {
                    teammateType = "Wizard",
                    level = 0,
                    equipment = 0,
                    attackPower = 5,
                    attackSpeed = 5,
                    enemySpawner = enemySpawner,
                    buyType = ResourceTracker.resources.gold,
                    cost = 10
                }
            };
            var xmlSerializer = new XmlSerializer(typeof(List<Teammates>));

            using (FileStream stream = File.Create(dataPath))
            {
                xmlSerializer.Serialize(stream, teammates);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("Doing The Third");
            teammates = new List<Teammates>()
            {
                new Teammates
                {
                    teammateType = "Archer",
                    level = 0,
                    equipment = 0,
                    attackPower = 1,
                    attackSpeed = 2,
                    enemySpawner = enemySpawner,
                    buyType = ResourceTracker.resources.gold,
                    cost = 5
                },
                new Teammates
                {
                    teammateType = "Wizard",
                    level = 0,
                    equipment = 0,
                    attackPower = 5,
                    attackSpeed = 5,
                    enemySpawner = enemySpawner,
                    buyType = ResourceTracker.resources.gold,
                    cost = 10
                }
            };
            var xmlSerializer = new XmlSerializer(typeof(List<Teammates>));
            using (FileStream stream = File.Create(dataPath))
            {
                xmlSerializer.Serialize(stream, teammates);
            }
        }
    }
    #endregion

    // Get teammate based on name
    #region
    public Teammates? GetTeammate(string name)
    {
        int index = 0;
        foreach (Teammates teammate in teammates)
        {
            if (teammate.teammateType == name)
            {
                return teammates[index];
            }
            index++;
        }
        return null;
    }
    #endregion

    // Save Teammate stats
    private void OnApplicationQuit()
    {
        string filePath = Application.persistentDataPath + "/Teammate_Data/";
        string dataPath = filePath + "Teammates.xml";
        var xmlSerializer = new XmlSerializer(typeof(List<Teammates>));

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        using (FileStream stream = File.Create(dataPath))
        {
            xmlSerializer.Serialize(stream, teammates);
        }

    }
}
