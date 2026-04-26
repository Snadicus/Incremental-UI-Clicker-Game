using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAbilities : MonoBehaviour
{
    // Variables
    #region
    // Script references
    public EnemySpawner enemyspawner;
    public Player player;
    public enum AttackTypes
    {
        physical,
        magical
    }
    #endregion

    [System.Serializable]
    // Defining abilities class
    #region
    public class AbilityData
    {
        public string name;
        public int level;
        public float mult; // Multiply strength by x amount
        public float cooldown; // How long each attack takes to use again. Higher equals longer.
        public float time; // How long it has been ability since pressed. Does not go above cooldown.
        public AttackTypes attackType;
        public int cost;
        public float manaCost;
        public float baseCooldown;
        public int baseCost;
        public Player player;
        public EnemySpawner enemySpawner;

        public void UseAbility()
        {
            if (attackType == AttackTypes.physical)
            {
                enemySpawner.EnemyAttacked((player.stats[0] + level) * mult);
            }
            else if (attackType == AttackTypes.magical)
            {
                enemySpawner.EnemyAttacked((player.stats[2] + level) * mult);
            }
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
                yield return null;
            }
        }
        public void IncreaseCost()
        {
            cost += Convert.ToInt32(cost * 0.5f);
        }
    }
    #endregion

    // Creating abilities
    #region
    public List<AbilityData> abilities;

    void Awake()
    {
        if (abilities == null || abilities.Count == 0)
        {
            abilities = new List<AbilityData>
            {
                new AbilityData
                {
                    name = "Magic Missile",
                    level = 0,
                    mult = 2,
                    cooldown = 5,
                    attackType = AttackTypes.magical,
                    manaCost = 5,
                    cost = 300,
                    baseCooldown = 5,
                    baseCost = 300,
                    player = player,
                    enemySpawner = enemyspawner,
                },
                new AbilityData
                {
                    name = "Quick Strike",
                    level = 0,
                    mult = 1.25f,
                    cooldown = 2,
                    attackType = AttackTypes.physical,
                    manaCost = 3,
                    cost = 250,
                    baseCooldown = 3,
                    baseCost = 250,
                    player = player,
                    enemySpawner = enemyspawner
                }
            };
        }
    }
    #endregion

    // Get which ability is being used
    #region
    public AbilityData? GetAbility(string name)
    {
        int index = 0;
        foreach (AbilityData ability in abilities)
        {
            if (ability.name == name)
            {
                return abilities[index];
            }
            index++;
        }
        return null;
    }
    #endregion

    // PrestigeProgress
    #region

    // Resets some progress for prestige
    public void PrestigeProgress()
    {
        foreach (var ability in abilities)
        {
            ability.level = 0;
            ability.cooldown = ability.baseCooldown;
            ability.cost = ability.baseCost;
        }
    }

    #endregion
}
