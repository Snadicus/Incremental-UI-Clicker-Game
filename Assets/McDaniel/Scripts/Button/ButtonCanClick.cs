using UnityEditor.Build.Content;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonCanClick : MonoBehaviour
{
    // Other object references
    [SerializeField] ResourceTracker resourceTraker;
    [SerializeField] TextMeshProUGUI costText, levelText;

    // Mutable variables
    public int goldNeeded;

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
