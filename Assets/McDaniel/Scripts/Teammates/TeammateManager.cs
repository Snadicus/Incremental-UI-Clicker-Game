using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
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
        public int baseCost;
        public float baseAttackPower;
        public float baseAttackSpeed;
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
        if (teammates == null || teammates.Count == 0)
        {
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
                    cost = 5,
                    baseCost = 5,
                    baseAttackPower = 1,
                    baseAttackSpeed = 2
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
                    cost = 10,
                    baseCost = 10,
                    baseAttackPower = 5,
                    baseAttackSpeed = 5
                }
            };
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

    // PrestigeProgress
    #region

    // Resets some progress for prestige
    public void PrestigeProgress()
    {
        foreach (var teammate in teammates)
        {
            teammate.level = 0;
            teammate.equipment = 0;
            teammate.cost = teammate.baseCost;
            teammate.attackPower = teammate.baseAttackPower;
            teammate.attackSpeed = teammate.baseAttackSpeed;
        }
    }

    #endregion
}