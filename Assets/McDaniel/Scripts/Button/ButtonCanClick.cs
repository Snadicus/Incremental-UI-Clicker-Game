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

    // Upgrade type references
    TeammateManager.Teammates teammate;
    BuildingsList.BuildingData building;

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
        }
        else if (upgradeObject is BuildingsList.BuildingData building)
        {
            this.building = building;
        }
        else
        {
            playerIndex = Convert.ToInt32(upgradeObject);
            Debug.Log(playerIndex);
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
            else if(building != null)
            {
                if (resourceTraker.gold >= building.cost)
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
                cost = building.cost;
                resourceType = building.buyType;

                building.IncreaseCost();
                resourceTraker.SpendResource(resourceType, cost);

                cost += Convert.ToInt32(cost * 0.5f);
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
            default:
                cost = 0;
                return;
        }
    }

}