using UnityEngine;

public class AttackButton : MonoBehaviour
{
    // Script Reference
    public EnemySpawner enemySpawner;
    public Player player;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Call this whenever the attack button is pressed to hurt the enemy.
    public void OnAttackButtonPressed()
    {
        enemySpawner.EnemyAttacked(player.stats[0]);
        Debug.Log("Player Attacked!");
    }
}
