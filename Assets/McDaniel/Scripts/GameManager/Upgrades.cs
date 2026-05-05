using System;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] TeammateManager teammateManager;
    [SerializeField] BuildingsList buildingsList;
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] ResourceTracker resourceTracker;
    [SerializeField] Player player;

    public enum upgradeTypes
    {
        Player,
        Building,
        Teammate,
        Abilities
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
        foreach (PlayerAbilities.AbilityData ability in playerAbilities.abilities)
        {
            if (ability.name == type)
            {
                AbilityUpgrade(type);
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

    void BuildingUpgrade(string type)
    {
        BuildingsList.BuildingData? currentBuilding = buildingsList.GetBuildingData(type);
        if (currentBuilding == null) return;

        if (currentBuilding.level <= 0)
        {
            if (resourceTracker.GetResource(currentBuilding.buyType) >= currentBuilding.cost)
            {
                resourceTracker.SpendResource(currentBuilding.buyType, currentBuilding.cost);
                currentBuilding.level = 1;
                buildingsList.StartCoroutine(currentBuilding.GainIncome());
            }
        }
        else
        {
            int upgradeCost = currentBuilding.GetUpgradeCost();
            if (resourceTracker.GetResource(currentBuilding.upgradeType) >= upgradeCost)
            {
                resourceTracker.SpendResource(currentBuilding.upgradeType, upgradeCost);
                currentBuilding.level++;
                currentBuilding.income += Mathf.Max(1, currentBuilding.baseIncome / 2);
            }
        }
    }

    void TeammateUpgrade(string type)
    {
        TeammateManager.Teammates? currentTeammate = teammateManager.GetTeammate(type);
        if (currentTeammate == null) return;

        // Check if player can afford it before upgrading
        if (resourceTracker.GetResource(currentTeammate.buyType) < currentTeammate.cost) return;

        resourceTracker.SpendResource(currentTeammate.buyType, currentTeammate.cost);

        if (currentTeammate.level <= 0)
        {
            currentTeammate.level = 1;
            teammateManager.StartCoroutine(currentTeammate.Attack());
        }
        else
        {
            currentTeammate.level += 1;
            currentTeammate.attackPower += 1;
        }

        currentTeammate.IncreaseCost();
    }

    void AbilityUpgrade(string type)
    {
        PlayerAbilities.AbilityData? currentAbility = playerAbilities.GetAbility(type);
        if (currentAbility == null)
        {
            return;
        }
        if (currentAbility.level <= 0)
        {
            currentAbility.level += 1;
            StartCoroutine(currentAbility.DecreaseCooldown());
        }
        else
        {
            currentAbility.level += 1;
            return;
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
        foreach (PlayerAbilities.AbilityData ability in playerAbilities.abilities)
        {
            if (ability.name == type)
            {
                return upgradeTypes.Abilities;
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
        foreach (PlayerAbilities.AbilityData ability in playerAbilities.abilities)
        {
            if (ability.name == type)
            {
                return ability;
            }
        }
        return player.GetStat(type);
    }
    #endregion
}