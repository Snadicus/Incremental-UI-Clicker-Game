using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatManager : MonoBehaviour
{
    // Variables & References
    #region

    // Contains the file path for save data
    string saveFilePath;

    // Script References
    public ResourceTracker resourceTracker;
    public BuildingsList buildingsList;
    public TeammateManager teammateManager;
    public PlayerAbilities playerAbilities;
    public Player player;
    public EnemySpawner enemySpawner;

    public TextMeshProUGUI goldFeedbackText;

    #endregion

    // Update
    #region

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            resourceTracker.gold += 5000;
            Debug.Log("Cheat Successful!");
            goldFeedbackText.text = "Gold Cheat Activated! +5000 Gold!";
        }

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            resourceTracker.gem += 5000;
            Debug.Log("Cheat Successful!");
            goldFeedbackText.text = "Gem Cheat Activated! +5000 Gems!";
        }

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            resourceTracker.mana = resourceTracker.maxMana;
            Debug.Log("Cheat Successful!");
            goldFeedbackText.text = "Mana Cheat Activated! 5000 Max Mana!";
        }

        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            resourceTracker.divineFavor += 5000;
            Debug.Log("Cheat Successful!");
            goldFeedbackText.text = "Divine Cheat Activated! +5000 Favor!";
        }

        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            enemySpawner.enemyCounter = 48;
            Debug.Log("Cheat Successful!");
            goldFeedbackText.text = "Area Cheat Activated! We are leaving!";
        }
    }

    #endregion
}
