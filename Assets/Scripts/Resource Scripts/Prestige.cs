using TMPro;
using UnityEngine;

public class Prestige : MonoBehaviour
{
    // Variables & References
    #region

    // Variables
    private int prestigeCost;
    public TextMeshProUGUI prestigeCostText;
    public TextMeshProUGUI divineFavorText;
    public TextMeshProUGUI prestigeLevelText;

    // References
    public ResourceTracker resourceTracker;
    public EnemySpawner enemySpawner;
    public Player player;
    public TeammateManager teammateManager;
    public PlayerAbilities playerAbilities;
    public BuildingsList buildingsList;

    #endregion

    // Update
    #region

    // Update is called once per frame
    // Used to constantly show the amount of DivineFavor
    void Update()
    {
        UpdateDivineFavor();
        UpdatePrestigeCost();
    }

    #endregion

    // CalculatePrestigeCost
    #region

    int CalculatePrestigeCost()
    {
        if (resourceTracker.prestigeLevel == 0)
            return 100;

        return resourceTracker.prestigeLevel * 200;
    }

    #endregion

    // TryPrestige
    #region

    // Sees if the player can prestige or not, if so then it starts the process
    public void TryPrestige()
    {
        if (resourceTracker.gem < prestigeCost)
        {
            Debug.Log("Not enough gems!");
            return;
        }

        // Spend gems
        resourceTracker.gem -= prestigeCost;

        // Increase prestige level
        resourceTracker.prestigeLevel++;

        // Reward divine favor equal to current prestige level
        resourceTracker.divineFavor += resourceTracker.prestigeLevel;

        ResetProgress();

        Debug.Log($"Prestiged! Level: {resourceTracker.prestigeLevel}, Divine Favor: {resourceTracker.divineFavor}");
    }

    #endregion

    // ResetProgress
    #region

    // Sets everything that should be reset back to default values
    void ResetProgress()
    {
        resourceTracker.PrestigeProgress();
        enemySpawner.PrestigeProgress();
        player.PrestigeProgress();
        teammateManager.PrestigeProgress();
        buildingsList.PrestigeProgress();

        UpdatePrestigeLevel();
        Debug.Log("Progress reset!");
    }

    #endregion

    // UpdateDivineFavor
    #region

    // Updates the bottom text to show the player how much gold they received from the previous kill
    public void UpdateDivineFavor()
    {
        divineFavorText.text = "Divine Favor: " + resourceTracker.divineFavor;
    }

    #endregion

    // UpdatePrestigeLevel
    #region

    // Updates the bottom text to show the player how much gold they received from the previous kill
    public void UpdatePrestigeLevel()
    {
        prestigeLevelText.text = "Prestige Level: " + resourceTracker.prestigeLevel;
    }

    #endregion

    // UpdatePrestigeLevel
    #region

    // Updates the prestige cost text
    void UpdatePrestigeCost()
    {
        prestigeCost = CalculatePrestigeCost();
        prestigeCostText.text = "Cost: " + prestigeCost;
    }

    #endregion
}
