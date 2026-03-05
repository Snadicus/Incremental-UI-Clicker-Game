using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManger : MonoBehaviour
{
    public Dictionary<string, int> resources;
    public List<string> teammates;

    private void Start()
    {
        resources.Add("Gold", 10);
        resources.Add("Mana", 5);
        resources.Add("Gem", 0);
        resources.Add("Divine Favor", 0);
        resources.Add("Prestige Level", 0);
    }

    public void AddGold(int amountGained)
    {
        resources["Gold"] += amountGained;
    }

    public void AddTeammate(string teammateType)
    {
        teammates.Add(teammateType);
    }
}
