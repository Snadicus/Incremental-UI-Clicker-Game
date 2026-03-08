using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teammates : MonoBehaviour
{
    public string teammateType;
    public int level;
    public int equipment;
    public float attackPower;
    public float attackSpeed;

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
