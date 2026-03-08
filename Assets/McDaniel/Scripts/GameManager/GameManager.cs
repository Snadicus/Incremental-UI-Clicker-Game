using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, int> resources;
    public List<string> teammates;

    private void Start()
    {
        // Adding resources needed for the game
        resources.Add("Gold", 10);
        resources.Add("Mana", 5);
        resources.Add("Gem", 0);
        resources.Add("Divine Favor", 0);
        resources.Add("Prestige Level", 0);
    }

    // Function called by other functions that adds amount of gold held
    public void AddGold(int amountGained)
    {
        resources["Gold"] += amountGained;
    }

    // Function called by other functions that adds teammate
    public void AddTeammate(string teammateType)
    {
        teammates.Add(teammateType);
    }
}
