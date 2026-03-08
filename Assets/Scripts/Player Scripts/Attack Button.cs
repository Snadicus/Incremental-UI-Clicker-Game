using UnityEngine;

public class AttackButton : MonoBehaviour
{
    // Script Reference
    public EnemySpawner enemySpawner;

    // Call this whenever the attack button is pressed to hurt the enemy.
    public void OnAttackButtonPressed()
    {
        enemySpawner.EnemyAttacked(1f);
        Debug.Log("Player Attacked!");
    }
}
