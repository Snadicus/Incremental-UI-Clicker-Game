using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    // Variables
    #region

    // Script References
    public AreaList areaList;
    public ResourceTracker resourceTracker;

    public int currentArea = 0; // Current area the player is in

    // Current enemy data and storage for stats
    private AreaList.enemyData currentEnemy;

    private float currentHealth;
    private float maxHealth;
    private int goldReward;

    // For determining when to spawn new enemies
    private int enemiesRemaining;

    // Text References
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyNumberText;
    public TextMeshProUGUI areaText;

    #endregion

    // Starting the program spawns the first enemy.
    void Start()
    {
        SpawnEnemy();
        UpdateAreaText();
    }

    // Update
    #region

    // Update is called once per frame
    // Used to constantly show the amount of enemies and their health.
    void Update()
    {
        UpdateEnemyHealthText();
        UpdateEnemyNumberText();
    }

    #endregion

    // SpawnEnemy
    #region

    // Spawns groups of enemies when called
    public void SpawnEnemy()
    {
        var area = areaList.getArea(currentArea);

        currentEnemy = area.getRandomEnemy();

        // Health calculation
        maxHealth = currentEnemy.baseHealth * area.healthMultiplier;
        currentHealth = maxHealth;

        goldReward = Mathf.RoundToInt(currentEnemy.baseGold * area.goldMultiplier);

        enemiesRemaining = area.enemyMultiplier;

        Debug.Log("Spawning: " + enemiesRemaining + " " + currentEnemy.name);
    }

    #endregion

    // EnemyAttacked
    #region
    public void EnemyAttacked (float attackPower)
    {
        currentHealth -= attackPower;

        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    #endregion

    // EnemyDie
    #region

    // Enemy dies, spawn new enemy. If the enemy group is not fully defeated, reset health but give gold.
    public void EnemyDie()
    {
        enemiesRemaining--;

        if (enemiesRemaining > 0)
        {
            Debug.Log(currentEnemy.name + " defeated! You receive " + goldReward + " gold!");
            currentHealth = maxHealth;
            Debug.Log(enemiesRemaining + " " + currentEnemy.name + " reamining!");
            ResourceTracker.Instance.AddGold(goldReward);
        }
        else
        {
            Debug.Log("Enemy group defeated! You receive " + goldReward + " gold!");
            ResourceTracker.Instance.AddGold(goldReward);

            SpawnEnemy();
        }
    }

    #endregion

    // UpdateEnemyHealthText
    #region

    // Constantly update enemy health amount
    public void UpdateEnemyHealthText()
    {
        enemyHealthText.text = "Health: " + Mathf.CeilToInt(currentHealth) + "/" + Mathf.CeilToInt(maxHealth);
    }

    #endregion

    // UpdateEnemyNumberText
    #region

    // Constantly update enemy number amount
    public void UpdateEnemyNumberText()
    {
        enemyNumberText.text = enemiesRemaining + " " + currentEnemy.name;
    }

    #endregion

    // UpdateAreaText
    #region

    // Occasionally update area
    public void UpdateAreaText()
    {
        var area = areaList.getArea(currentArea);
        areaText.text = "Area: " + area.areaName;
    }

    #endregion
}
