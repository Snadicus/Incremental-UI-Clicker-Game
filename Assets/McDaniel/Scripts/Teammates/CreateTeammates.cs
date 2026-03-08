using UnityEngine;
using TMPro;
public class CreateTeammates : MonoBehaviour
{
    // Get other game components
    GameManager gameManager;
    Teammates Archer, Wizard;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    // Functions from here and below are all about creating and upgrading teammate skills when it's respective upgrade is bought
    public void ArcherLevel()
    {
        // Create Archer if the Archer is not currently on the Teammates list
        if (!gameManager.teammates.Contains("Archer"))
        {
            gameManager.teammates.Add("Archer");
            // Set up Archer teammate. Type, damage, attack speed
            Archer = new Teammates("Archer", 1, 2);
        }
        // If Archer does exist increase level and attack power
        else
        {
            Archer.level += 1;
            Archer.attackPower += 1;
        }
    }

    public void WizardLevel() 
    {
        // Create Wizard if the Wizard is not currently on the Teammates list
        if (!gameManager.teammates.Contains("Wizard"))
        {
            gameManager.teammates.Add("Wizard");
            // Set up Wizard teammate. Type, damage, attack speed
            Wizard = new Teammates("Wizard", 5, 5);
        }
        // Create Wizard if the Wizard is not currently on the Teammates list
        else
        {
            Wizard.level += 1;
            Wizard.attackPower += 1;
        }
    }
}
