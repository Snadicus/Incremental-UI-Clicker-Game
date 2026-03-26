using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    // Player game components
    Player player;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Upgrading different stats
    public void PlayerUpgrade(string type)
    {
        int? foundStat = player.GetStat(type);
        int stat;
        if (foundStat != null)
        {
            stat = foundStat.Value;
            player.stats[stat] += 1;
        }
    }
}
