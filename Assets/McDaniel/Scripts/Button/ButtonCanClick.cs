using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCanClick : MonoBehaviour
{
    // Other object references
    [SerializeField] ResourceTracker resourceTraker;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] 

    BuildingsList buildingsUpgrade;
    TeammateManager teammateManager;
    string type;

    void Awake()
    {
        buildingsUpgrade = GameObject.Find("Buildings").GetComponent<BuildingsList>();
        teammateManager = GameObject.Find("TeamManager").GetComponent<TeammateManager>();
        foreach (BuildingsList.BuildingData buildings in buildingsUpgrade.buildings)
        {
            if (gameObject.name == buildings.name)
            {
                type = "Buildings";
                return;
            }
        }
        foreach (TeammateManager.Teammates teammate in teammateManager.teammates)
        {
            if (gameObject.name == teammate.teammateType)
            {
                type = "Teammates";
                return;
            }
        }
    }

    // Mutable variables
    public int goldNeeded;
    public string upgradeType;
    // If the button can't click, then it should be darker and button disabled. 
    // If button can be clicked, it should be brighter and button enabled.
    // If purchased, highlight, buy the upgrade, increase cost of upgrade, then check if it should be enabled or disabled.
    void Update()
    {
        /* When amount of gold is greater than gold needed, enable button
         * letting player upgrade. Else disable button.
         */
        if (resourceTraker.gold >= goldNeeded)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Button>().enabled = false;
        }
    }

    // Increaase the cost of upgrades after getting upgrade
    public void IncreaseCost()
    {
        resourceTraker.SpendGold(goldNeeded);
        goldNeeded *= 2;
        costText.text = "Cost: " + goldNeeded;
    }

    public void AttackBar()
    {

    }
}