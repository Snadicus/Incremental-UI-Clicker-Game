using UnityEngine;
using TMPro;
using System;
public class UpgradeTeammates : MonoBehaviour
{
    // Get other game components
    TeamManager teamManager;
    public EnemySpawner enemySpawner;
    Teammates Archer, Wizard;

    private void Awake()
    {
        teamManager = GameObject.Find("GameManager").GetComponentInChildren<TeamManager>();
    }

    // Functions from here and below are all about creating and upgrading teammate skills when it's respective upgrade is bought
    public void ArcherLevel()
    {
        // Create Archer if the Archer is not currently on the Teammates list
        if (!teamManager.teammates.Contains("Archer"))
        {
            teamManager.teammates.Add("Archer");
            // Set up Archer teammate. Type, damage, attack speed
            Archer = new Teammates("Archer", 1, 2, enemySpawner);
            StartCoroutine(Archer.Attack());
        }
        // If Archer does exist increase level and attack power
        else
        {
            Archer.level += 1;
            Archer.attackPower += 1;
            if (Archer.level % 5 == 0)
            {
                Archer.attackSpeed -= Archer.attackSpeed * 0.05f;
            }
        }
    }

    public void WizardLevel() 
    {
        // Create Wizard if the Wizard is not currently on the Teammates list
        if (!teamManager.teammates.Contains("Wizard"))
        {
            teamManager.teammates.Add("Wizard");
            // Set up Wizard teammate. Type, damage, attack speed
            Wizard = new Teammates("Wizard", 5, 5, enemySpawner);
            StartCoroutine(Wizard.Attack());
        }
        // Create Wizard if the Wizard is not currently on the Teammates list
        else
        {
            Wizard.level += 1;
            Wizard.attackPower += 1;
            if (Wizard.level % 5 == 0)
            {
                Wizard.attackSpeed -= Wizard.attackSpeed * 0.05f;
            }
        }
    }
}
