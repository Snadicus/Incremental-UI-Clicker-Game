using System;
using TMPro;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    // Important Initial Things
    #region

    // Singleton Access
    public static ResourceTracker Instance;

    // Text References
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI divineText;
    public TextMeshProUGUI prestigeText;

    // Variables
    public int gold;
    public float mana;
    public int gem;
    public int divineFavor;
    public int prestigeLevel;

    // Enums

    public enum resources
    {
        gold,
        mana,
        gem,
        divineFavor,
        prestigeLevel
    };

    #endregion

    // Awake Instance
    #region

    // Create Instance for Singleton Access
    void Awake()
    {
        Instance = this;
    }

    #endregion

    // Update
    #region

    // Update is called once per frame
    void Update()
    {
        UpdateGoldText();
    }

    #endregion

    // AddResource
    #region

    // For adding Resource from other scripts
    public void AddResource(resources resource, int amount)
    {
        switch (resource)
        {
            case resources.gold:
                gold += amount;
                goldText.text = "Gold: " + gold.ToString();
                return;
            case resources.mana:
                mana += amount;
                int manaInt = Convert.ToInt32(mana);
                manaText.text = "Mana: " + manaInt.ToString();
                return;
            case resources.gem:
                gemText.text = "Gem: " + gem.ToString();
                gem += amount;
                return;
            case resources.divineFavor:
                divineText.text = "Divine Favor: " + divineFavor.ToString();
                divineFavor += amount;
                return;
            case resources.prestigeLevel:
                prestigeText.text = "Prestige Level: " + prestigeLevel.ToString();
                prestigeLevel += amount;
                return;
        }
    }

    #endregion

    // SpendResource
    #region
    // For removing Resources from other scripts
    public void SpendResource(resources resource, int amount)
    {
        switch (resource)
        {
            case resources.gold:
                gold -= amount;
                goldText.text = "Gold: " + gold.ToString();
                return;
            case resources.mana:
                mana -= amount;
                manaText.text = "Mana: " + mana.ToString();
                return;
            case resources.gem:
                gem -= amount;
                gemText.text = "Gem: " + gem.ToString();
                return;
            case resources.divineFavor:
                divineFavor -= amount;
                divineText.text = "Divine Favor: " + divineFavor.ToString();
                return;
            case resources.prestigeLevel:
                prestigeLevel -= amount;
                prestigeText.text = "Prestige Level: " + prestigeLevel.ToString();
                return;
        }
    }

    #endregion

    // UpdateGoldText
    #region

    // Constantly update gold counter
    public void UpdateGoldText()
    {
        goldText.text = "Gold: " + gold.ToString();
    }

    #endregion
}
