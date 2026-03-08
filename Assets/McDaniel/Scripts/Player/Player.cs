using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    EnemyLists enemyList;

    // Player stats Strength, Agility, Intelligence, Wisdom
    public int[] stats = { 1, 1, 1, 1 };

    public void Awake()
    {
        enemyList = GameObject.Find("GameManager").GetComponent<EnemyLists>();
    }

    public void Attack()
    {
        float damage = stats[0];
        //enemyList.currentEnemies[0].gameObject.GetComponent<BaseEnemy>().EnemyDamage(damage);
    }
}
