using UnityEngine;
using TMPro;

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
    public int mana;
    public int gem;
    public int divineFavor;
    public int prestigeLevel;

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

    // AddGold
    #region

    // For adding Gold from other scripts
    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldText();
    }

    #endregion

    // SpendGold
    #region

    // For removing Gold from other scripts
    public void SpendGold(int amount)
    {
        gold -= amount;
        UpdateGoldText();
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
