using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    // Player game components
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    // Upgrading different stats
    public void StrengthUpgrade()
    {
        player.stats[0] += 1;
    }
    public void AgilityUpgrade()
    {
        player.stats[1] += 1;
    }
    public void IntelligenceUpgrade()
    {
        player.stats[2] += 1;
    }
    public void WisdomUpgrade()
    {
        player.stats[3] += 1;
    }
}
