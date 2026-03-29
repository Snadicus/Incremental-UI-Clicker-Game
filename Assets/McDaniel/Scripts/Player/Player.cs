using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    // Variables
    // Player Stats
    public int[] stats = {1,1,1,1}; // Strength(attack damage), Agility(cooldown for special abilities), intelligece(spell damage), Wisdom(spell cooldown)
    public int[] statsCost = { 2, 2, 2, 2 };

    public int? GetStat(string type)
    {
        switch (type)
        {
            case "Strength":
                return 0;
            case "Agility":
                return 1;
            case "Intelligence":
                return 2;
            case "Wisdom":
                return 3;
            default:
                return 0;
        }
    }

    public void IncreaseCost(int stat)
    {
        statsCost[stat] += Convert.ToInt32(statsCost[stat] * 0.5f);
    }

    public IEnumerator IncreaseMana()
    {

        yield return new WaitForSeconds(90);
    }
}
