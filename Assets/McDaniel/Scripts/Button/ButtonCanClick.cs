using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCanClick : MonoBehaviour
{
    // Other object references
    [SerializeField] ResourceTracker resourceTraker;
    [SerializeField] TextMeshProUGUI costText;

    // Mutable variables
    public int goldNeeded;
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
}
