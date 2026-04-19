using UnityEngine;
using TMPro;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    // Variables
    #region

    // Script References
    public AreaList areaList;
    public ResourceTracker resourceTracker;
    public BuildingsList buildingsList;

    public int currentArea = 1; // Current area the player is in
    private int areaMax; // Max amount of areas currently in the game, helps to loop.
    private int enemyCounter = 0; // Tracks eney groups killed. Helps to spawn boss

    // Current enemy data and storage for stats
    private AreaList.enemyData currentEnemy;

    private float currentHealth;
    private float maxHealth;
    private int goldReward;

    private float healthMultiplier;
    private float goldMultiplier;
    private int enemyMultiplier;
    public int loop = 1;

    // For determining some logic that should only apply to boss battles
    public bool isBossActive = false;
    private Coroutine bossTimerRoutine;
    public event System.Action OnBossStart;
    public event System.Action OnBossEnd;

    // For determining when to spawn new enemies
    private int enemiesRemaining;

    // Text References
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyNumberText;
    public TextMeshProUGUI areaText;
    public TextMeshProUGUI goldFeedbackText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemyGroupsText;

    #endregion

    // Start
    #region

    // Starting the program spawns the first enemy.
    void Start()
    { 
        SpawnEnemy();
        UpdateAreaText();
        areaMax = areaList.areas.Count - 1;
        Debug.Log(areaMax);
    }

    #endregion

    // Update
    #region

    // Update is called once per frame
    // Used to constantly show the amount of enemies and their health.
    void Update()
    {
        UpdateEnemyHealthText();
        UpdateEnemyNumberText();
        UpdateEnemyGroups();
    }

    #endregion

    // OnEnable
    #region

    void OnEnable()
    {
        OnBossStart += HandleBossStart;
        OnBossEnd += HandleBossEnd;
    }

    #endregion

    // OnDisable
    #region

    void HandleBossStart()
    {
        goldFeedbackText.text = "Boss battle has begun!";
    }

    #endregion

    // HandleBossStart
    #region


    void HandleBossEnd()
    {
        goldFeedbackText.text = "Boss defeated!";
    }

    #endregion

    // HandleBossEnd
    #region

    void OnDisable()
    {
        OnBossStart -= HandleBossStart;
        OnBossEnd -= HandleBossEnd;
    }

    #endregion

    // SpawnEnemy
    #region

    // Spawns groups of enemies when called
    public void SpawnEnemy()
    {
        var area = areaList.getArea(currentArea);

        currentEnemy = area.getRandomEnemy();

        GetMultipliers();

        // Health calculation
        maxHealth = currentEnemy.baseHealth * healthMultiplier;
        currentHealth = maxHealth;

        goldReward = Mathf.RoundToInt(currentEnemy.baseGold * goldMultiplier);

        enemiesRemaining = enemyMultiplier;

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
        UpdateEnemyFeedback();

        if (enemiesRemaining > 0)
        {
            Debug.Log(currentEnemy.name + " defeated! You receive " + goldReward + " gold!");
            currentHealth = maxHealth;
            Debug.Log(enemiesRemaining + " " + currentEnemy.name + " reamining!");
            ResourceTracker.Instance.AddResource(ResourceTracker.resources.gold, goldReward);
        }
        else
        {
            Debug.Log("Enemy group defeated! You receive " + goldReward + " gold!");
            ResourceTracker.Instance.AddResource(ResourceTracker.resources.gold, goldReward);

            enemyCounter += 1;

            // Check to see if a boss battle is happening
            if (!isBossActive)
            {
                // Check to see if area is cleared, and if so, spawn boss
                if (enemyCounter != 49)
                {
                    SpawnEnemy();
                }
                else
                {
                    SpawnBoss();
                }
            }
            else if (isBossActive)
            {
                if (bossTimerRoutine != null)
                {
                    StopCoroutine(bossTimerRoutine);
                }

                timerText.text = "";

                OnBossEnd?.Invoke();

                if (currentArea >= areaMax)
                {
                    currentArea = 1;
                    loop += 1;
                } 
                else
                {
                    currentArea += 1;
                    buildingsList.UnlockBuildings();
                }

                enemyCounter = 0;
                isBossActive = false;

                UpdateAreaText();
                SpawnEnemy();
            }
        }
    }

    #endregion

    // SpawnBoss
    #region

    // Spawns the boss, called when enemyCounter reaches desired amount
    public void SpawnBoss()
    {
        var area = areaList.getArea(currentArea);

        currentEnemy = area.getBoss();

        isBossActive = true;

        // Health calculation
        maxHealth = currentEnemy.baseHealth;
        currentHealth = maxHealth;

        enemiesRemaining = 1;

        Debug.Log("Spawning: " + enemiesRemaining + " " + currentEnemy.name);

        OnBossStart?.Invoke();

        BossTimer();
    }

    #endregion

    // GetMultipliers
    #region

    // Gets the multipliers, which are calculated based off the current area
    public void GetMultipliers() 
    { 
        var area = areaList.getArea(currentArea);

        healthMultiplier = Random.Range((0.9f * currentArea) * loop, (1.2f * currentArea) * loop);
        goldMultiplier = Random.Range((0.9f * currentArea) * loop, (1.2f * currentArea) * loop);
        enemyMultiplier = Random.Range((1 * currentArea) * loop, (3 * currentArea) * loop);
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
        if (!isBossActive)
        {
            enemyNumberText.text = enemiesRemaining + " " + currentEnemy.name;
        }
        else
        {
            enemyNumberText.text = currentEnemy.name;
        }
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

    // UpdateEnemyFeedback
    #region

    // Updates the bottom text to show the player how much gold they received from the previous kill
    public void UpdateEnemyFeedback()
    {
        goldFeedbackText.text = "Enemy defeated! You gained " + goldReward + " gold!";
    }

    #endregion

    // BossTimer
    #region

    // Gives the player 30 seconds to fight the boss
    public void BossTimer()
    {
        if (bossTimerRoutine != null)
        {
            StopCoroutine(bossTimerRoutine);
        }

        bossTimerRoutine = StartCoroutine(BossTimerCoroutine());
    }

    IEnumerator BossTimerCoroutine()
    {
        float timer = 30f;

        while (timer > 0 && isBossActive)
        {
            timerText.text = "Boss Time: " + Mathf.CeilToInt(timer);
            timer -= Time.deltaTime;
            yield return null;
        }

        // If boss still alive when time runs out
        if (isBossActive)
        {
            timerText.text = "Boss Escaped!";

            OnBossEnd?.Invoke();

            isBossActive = false;
            enemyCounter = 0;

            Debug.Log("Boss was not defeated in time!");

            SpawnEnemy();
        }
    }

    #endregion

    // UpdateEnemyGroups
    #region

    // Constantly updates the amount of enemy groups needed to finish area
    public void UpdateEnemyGroups()
    {
        enemyGroupsText.text = enemyCounter + " / 50";
    }

    #endregion
}
