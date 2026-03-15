using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        public EnemySpawner enemySpawner;
        public int cost;

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
    }
    #endregion

    public List<Teammates> teammates;

    // Creating teammate types. Add as much as we need/please
    #region
    void Awake()
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
                cost = 10
            }
        };
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
        Debug.Log($"{name} does not exist");
        return null;
    }
    #endregion
}
