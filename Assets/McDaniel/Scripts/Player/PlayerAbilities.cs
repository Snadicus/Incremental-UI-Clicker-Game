using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilities : MonoBehaviour
{
    // Variables
    #region
    // Script references
    EnemySpawner enemyspawner;
    Player player;
    public enum AttackTypes
    {
        physical,
        magical
    }
    #endregion


    // Defining abilities class
    #region
    public class Ability
    {
        public string name;
        public int level;
        public float mult; // Multiply strenght by x amount
        public float cooldown; // How long each attack takes to use again. Higher equals longer.
        float time; // How long it has been ability since pressed. Does not go above cooldown.
        public AttackTypes attackType;
        public Player player;
        public EnemySpawner enemySpawner;

        public void UseAbility()
        {
            enemySpawner.EnemyAttacked(player.stats[3] * mult * level);
            time = cooldown;
        }

        public IEnumerator DecreaseCooldown()
        {
            while (level > 0)
            {
                if(time >= 0)
                {
                    time -= Time.deltaTime;
                } 
            }
            yield return null;
        }
    }
    #endregion

    // Creating abilities
    #region
    public List<Ability> abilities;

    void Awake()
    {
        abilities = new List<Ability>
        {
            new Ability
            {
                name = "Magic Missle",
                level = 0,
                mult = 2,
                cooldown = 5,
                attackType = AttackTypes.magical,
                player = player,
                enemySpawner = enemyspawner,
            },
            new Ability
            {
                name = "Quick Strike",
                level = 0,
                mult = 1.25f,
                cooldown = 2,
                attackType = AttackTypes.physical,
                player = player,
                enemySpawner = enemyspawner
            }
        };
    }
    #endregion
}
