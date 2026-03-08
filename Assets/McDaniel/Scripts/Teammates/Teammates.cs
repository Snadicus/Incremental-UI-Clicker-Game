using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teammates : MonoBehaviour
{
    // Stats of teammates.
    public string teammateType;
    public int level;
    public int equipment;
    // Damange delt
    public float attackPower;
    // Time between next attack. Lower is faster
    public float attackSpeed;

    // Instantiating teammate allowing different starting stats for type, attack power, and attack speed, 
    public Teammates(string teammateType, float attackPower, float attackSpeed)
    {
        this.teammateType = teammateType;
        this.attackPower = attackPower;
        this.attackSpeed = attackSpeed;
        this.level = 1;
        this.equipment = 1;
    }

    void Awake()
    {
        StartCoroutine(Attack());
    }

    // Attacking first enemy in enemy list, repeating for x amount of seconds which is based on attack speed
    IEnumerator Attack()
    {
        // Attacks enemies
        while (level > 1)
        {
            // Attack enemy
            /*
             * GameObject enemy = enemyManager.activeEnemies[0]
             * enemy.GetComponent<BaseEnemy>().EnemyTakeDamage(attackPower)
            */
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
