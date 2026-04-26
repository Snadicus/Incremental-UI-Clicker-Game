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
    float maxMana = 20;

    public ResourceTracker resourceTracker;

    private void Start()
    {
        StartCoroutine(IncreaseMana());
    }

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
        float stand = 0.0015f;
        while (1 == 1)
        {
            if (resourceTracker.mana <= maxMana)
            {
                resourceTracker.mana += stand * (stats[3] * 0.5f);
            }
            yield return null;
        }
    }

    // PrestigeProgress
    #region

    // Resets some progress for prestige
    public void PrestigeProgress()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = 1;
        }

        for (int i = 0; i < statsCost.Length; i++)
        {
            statsCost[i] = 2;
        }

        maxMana = 20;
    }

    #endregion
}
