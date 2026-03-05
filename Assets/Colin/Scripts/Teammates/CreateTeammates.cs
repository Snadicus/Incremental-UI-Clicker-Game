using UnityEngine;
using TMPro;
public class CreateTeammates : MonoBehaviour
{
    GameManger gameManager;
    Teammates Archer, Wizard;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManger>();
    }

    public void ArcherLevel()
    {
        if (!gameManager.teammates.Contains("Archer"))
        {
            gameManager.teammates.Add("Archer");
            Archer = new Teammates("Archer", 1, 2);
        }
        else
        {
            Archer.level += 1;
            Archer.attackPower += 1;
        }
    }

    public void WizardLevel() 
    {
        if (!gameManager.teammates.Contains("Wizard"))
        {
            gameManager.teammates.Add("Wizard");
            Wizard = new Teammates("Wizard", 5, 5);
        }
        else
        {
            Wizard.level += 1;
            Wizard.attackPower += 1;
        }
    }
}
