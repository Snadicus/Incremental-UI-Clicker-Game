using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonCanClick : MonoBehaviour
{
    // Other object references
    [SerializeField] ResourceTracker resourceTraker;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Upgrades upgrades;
    [SerializeField] Player player;

    // Mutable variables
    int playerIndex;
    public bool attack;

    // Upgrade type references
    TeammateManager.Teammates teammate;
    BuildingsList.BuildingData building;
    PlayerAbilities.AbilityData ability;

    // Variables for script to know what object to look at and change
    Upgrades.upgradeTypes upgradeType;
    object upgradeObject;

    // States
    public enum ButtonStates
    {
        Locked,
        Available,
        Purchased
    }

    void Start()
    {
        // Get the upgrade type and game object
        upgradeType = upgrades.GetUpgradeType(gameObject.name);
        upgradeObject = upgrades.GetUpgradeObject(gameObject.name);
        if (upgradeObject is TeammateManager.Teammates teammate)
        {
            this.teammate = teammate;
            costText.text = "Cost: " + teammate.cost;
        }
        else if (upgradeObject is BuildingsList.BuildingData building)
        {
            this.building = building;
            costText.text = "Cost: " + building.cost;
        }
        else if (upgradeObject is PlayerAbilities.AbilityData ability)
        {
            this.ability = ability;
            if (costText != null)
            {
                costText.text = "Cost: " + ability.cost;
            }
        }
        else
        {
            playerIndex = Convert.ToInt32(upgradeObject);
            costText.text = "Cost: " + player.statsCost[playerIndex];
        }
    }

    // If the button can't click, then it should be darker and button disabled. 
    // If button can be clicked, it should be brighter and button enabled.
    // If purchased, highlight, buy the upgrade, increase cost of upgrade, then check if it should be enabled or disabled.
    #region
    void Update()
    {
        /* When amount of gold is greater than gold needed, enable button
         * letting player upgrade. Else disable button.
         */
        if (upgradeType == Upgrades.upgradeTypes.Building || upgradeType == Upgrades.upgradeTypes.Teammate)
        {
            if(teammate != null)
            {
               if (resourceTraker.gold >= teammate.cost)
                {
                    gameObject.GetComponent<Button>().enabled = true;
                    return;
                }
               else
                {
                    gameObject.GetComponent<Button>().enabled = false;
                    return;
                }
            }
            else if (building != null)
            {
                int cost = (building.level <= 0)
                    ? building.cost
                    : building.GetUpgradeCost();

                ResourceTracker.resources type = (building.level <= 0)
                    ? building.buyType
                    : building.upgradeType;

                if (resourceTraker.GetResource(type) >= cost && building.unlocked)
                {
                    gameObject.GetComponent<Button>().enabled = true;
                    return;
                }
                else
                {
                    gameObject.GetComponent<Button>().enabled = false;
                    return;
                }
            }
        }
        else if (upgradeType == Upgrades.upgradeTypes.Player)
        {
            if (resourceTraker.gold >= player.statsCost[playerIndex])
            {
                gameObject.GetComponent<Button>().enabled = true;
                return;
            }
            else
            {
                gameObject.GetComponent<Button>().enabled = false;
                return;
            }
        }
        else if (upgradeType == Upgrades.upgradeTypes.Abilities)
        {
            if (!attack)
            {

                if (resourceTraker.gold >= ability.cost)
                {
                    gameObject.GetComponent<Button>().enabled = true;
                }
                else
                {
                    gameObject.GetComponent<Button>().enabled = false;
                }
            }
            else
            {
                if (resourceTraker.mana >= ability.manaCost && ability.time <= 0)
                {
                    gameObject.GetComponent<Button>().enabled = true;
                }
                else
                {
                    gameObject.GetComponent<Button>().enabled = false;
                }
            }
        }

        if (teammate != null)
        {
            costText.text = "Cost: " + teammate.cost;
        }
        else if (building != null)
        {
            int displayCost = (building.level <= 0) ? building.cost : building.GetUpgradeCost();
            costText.text = "Cost: " + displayCost;
        }
    }
    #endregion

    // Increaase the cost of upgrades after getting upgrade
    public void IncreaseCost()
    {
        int cost;
        ResourceTracker.resources resourceType;
        switch (upgradeType)
        {
            case Upgrades.upgradeTypes.Player:
                cost = player.statsCost[playerIndex];
                resourceType = ResourceTracker.resources.gold;

                player.IncreaseCost(playerIndex);
                resourceTraker.SpendResource(resourceType, cost);

                cost += Convert.ToInt32(cost * 0.5f);
                costText.text = "Cost: " + cost;
                return;
            case Upgrades.upgradeTypes.Building:
                cost = (building.level <= 0) ? building.cost : building.GetUpgradeCost();
                resourceType = (building.level <= 0) ? building.buyType: building.upgradeType;

                resourceTraker.SpendResource(resourceType, cost);

                cost = Mathf.RoundToInt(building.baseUpgradeCost * Mathf.Pow(building.upgradeCostMultiplier, building.level));
                costText.text = "Cost: " + cost;
                return;
            case Upgrades.upgradeTypes.Teammate:
                cost = teammate.cost;
                resourceType = teammate.buyType;

                teammate.IncreaseCost();
                resourceTraker.SpendResource(resourceType, cost);

                cost += Convert.ToInt32(cost * 0.5f);
                costText.text = "Cost: " + cost;
                return;
            case Upgrades.upgradeTypes.Abilities:
                cost = ability.cost;
                resourceType = ResourceTracker.resources.gold;

                ability.IncreaseCost();
                resourceTraker.SpendResource(resourceType, cost);

                cost += Convert.ToInt32(cost * 0.5f);
                costText.text = "Cost: " + cost;
                return;
            default:
                cost = 0;
                return;
        }
    }

}