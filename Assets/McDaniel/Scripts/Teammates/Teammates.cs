using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teammates
{
    // Script reference
    EnemySpawner enemySpawner;

    // Stats of teammates.
    public string teammateType;
    public int level;
    public int equipment;
    // Damange delt
    public float attackPower;
    // Time between next attack. Lower is faster
    public float attackSpeed;

    // Instantiating teammate allowing different starting stats for type, attack power, and attack speed, 
    public Teammates(string teammateType, float attackPower, float attackSpeed, EnemySpawner enemySpawner)
    {
        this.teammateType = teammateType;
        this.attackPower = attackPower;
        this.attackSpeed = attackSpeed;
        this.level = 1;
        this.equipment = 1;
        this.enemySpawner = enemySpawner;
    }

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
