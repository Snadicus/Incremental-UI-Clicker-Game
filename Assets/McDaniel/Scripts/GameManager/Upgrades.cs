using System;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] TeammateManager teammateManager;
    [SerializeField] BuildingsList buildingsList;
    [SerializeField] Player player;
    
    public enum upgradeTypes
    {
        Player,
        Building,
        Teammate
    }

    public void Upgrade(string type)
    {
        foreach (TeammateManager.Teammates teammate in teammateManager.teammates)
        {
            if (teammate.teammateType == type)
            {
                TeammateUpgrade(type);
                return;
            }
        }
        foreach (BuildingsList.BuildingData building in buildingsList.buildings)
        {
            if (building.name == type)
            {
                BuildingUpgrade(type);
                return;
            }
        }
        PlayerUpgrade(type);
    }
    // Different Upgrade logic for the different types of upgrades
    #region
    // Player upgrade
    void PlayerUpgrade(string type)
    {
        // Get the stat that correlates to the name
        int? foundStat = player.GetStat(type);
        int stat;
        if (foundStat != null)
        {
            stat = foundStat.Value;
            player.stats[stat] += 1;
        }
    }

    // Building upgrade
    void BuildingUpgrade(string type)
    {
        // Get the correct building being worked with
        BuildingsList.BuildingData? currentBuilding = buildingsList.GetBuildingData(type);
        // If building has not been bought, add on level to it
        if (currentBuilding.level <= 0)
        {
            currentBuilding.level = 1;
            StartCoroutine(currentBuilding.GainIncome());
            return;
        }
        currentBuilding.level++;
        currentBuilding.income += currentBuilding.income + (currentBuilding.level / currentBuilding.income);
    }

    void TeammateUpgrade(string type)
    {
        TeammateManager.Teammates? currentTeammate = teammateManager.GetTeammate(type);
        if (currentTeammate == null)
        {
            return;
        }

        if (currentTeammate.level <= 0)
        {
            currentTeammate.level += 1;
            StartCoroutine(currentTeammate.Attack());
        }
        else
        {
            currentTeammate.level += 1;
            currentTeammate.attackPower += 1;
        }
    }
    #endregion

    // Give information about what is being/can be upgraded
    #region
    // Return string value of the type of upgrade
    public upgradeTypes GetUpgradeType(string type)
    {
        foreach (TeammateManager.Teammates teammate in teammateManager.teammates)
        {
            if (teammate.teammateType == type)
            {
                return upgradeTypes.Teammate;
            }
        }
        foreach (BuildingsList.BuildingData building in buildingsList.buildings)
        {
            if (building.name == type)
            {
                return upgradeTypes.Building;

            }
        }
        return upgradeTypes.Player;
    }

    // Retrun an object of what is being asked for to check for things like cost
    public object GetUpgradeObject(string type)
    {
        foreach (TeammateManager.Teammates teammate in teammateManager.teammates)
        {
            if (teammate.teammateType == type)
            {
                return teammate;
            }
        }
        foreach (BuildingsList.BuildingData building in buildingsList.buildings)
        {
            if (building.name == type)
            {
                return building;

            }
        }
        return player.GetStat(type);
    }
    #endregion
}