using UnityEditor.Build.Content;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCanClick : MonoBehaviour
{
    // Other object references
    GameManager gameManager;

    // Mutable variables
    public float goldNeeded;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        /* When amount of gold is greater than gold needed, enable button
         * letting player upgrade. Else disable button.
         */
        if (gameManager.resources["Gold"] >= goldNeeded)
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
        goldNeeded *= 2 + goldNeeded;
    }
}
