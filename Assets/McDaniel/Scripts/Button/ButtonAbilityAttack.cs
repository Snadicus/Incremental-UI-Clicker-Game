using System;
using UnityEngine;

public class ButtonAbilityAttack : MonoBehaviour
{
    public ResourceTracker resourceTracker;
    public PlayerAbilities playerAbilities;
    PlayerAbilities.AbilityData ability;

    void Start()
    {
        foreach (PlayerAbilities.AbilityData ability in playerAbilities.abilities)
        {
            if (ability.name == gameObject.name)
            {
                this.ability = ability;
            }
        } 
    }

    public void Attack()
    {
        ability.UseAbility();
        int manaCost = Convert.ToInt32(ability.manaCost);
        resourceTracker.SpendResource(ResourceTracker.resources.mana, manaCost);
    }
}
