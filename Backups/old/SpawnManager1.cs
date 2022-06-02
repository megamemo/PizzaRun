using System;
using System.Collections;
using UnityEngine;

public class SpawnManager1 : MonoBehaviour
{
    public static SpawnManager1 instance { get; private set; }
    [SerializeField] private GameObject healthObject;
    private GameObject pooledHealth;

    int randomEnemiesCount = 3;
    int randomObstaclesCount = 2;
    int randomPowerupCount = 2;
    int randomEnemy;
    int randomObstacle;
    int randomPowerup;

    private float xRange = 15.5f;
    private float yPosEnemy = 0.1f;
    private float zPosEnemy = 17.0f;
    private float yPosPowerup = 0.75f;
    private float zRangePowerupMin = -5.0f;
    private float zRangePowerupMax = 12.0f;

    private int startDelayEnemy = 1;
    private int startDelayObstacle = 3;
    private float startDelayHealth;
    private float startDelayPowerup;

    private float repeatDelayEnemyMax = 3.5f;
    private float repeatDelayEnemyMin = 0.5f;
    private float repeatDelayEnemy;
    private int repeatDelayEnemyMultipliler = 15;

    private float repeatDelayObstacleMax = 10.0f;
    private float repeatDelayObstacleMin = 2.0f;
    private float repeatDelayObstacle;
    private int repeatDelayObstacleMultipliler = 5;

    private int repeatDelayHealth = 20;
    private int repeatDelayPowerup = 18;

    private int enemyCounter;
    private int obstacleCounter;
    private int maxHealthLimit = 1;
    private int maxPowerupLimit = 1;
    [HideInInspector] public int healthCounter;
    [HideInInspector] public int powerupCounter;

    private float gameDuration;
    private float lastSpawnTimeEnemy;
    private float lastSpawnTimeObstacle;
    public float lastSpawnTimePowerup { get; private set; }

    public HealthState healthState { get; private set; }
    public PowerupState powerupState { get; private set; }


    private void Awake()
    {
        InstanciateSpawnManager();
    }

    private void InstanciateSpawnManager()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        StartGame();
        PoolHealth();

        GameManager.instance.GameRestarted += OnGameRestarted;
        GameManager.instance.LevelChanged += OnLevelChanged;
        GameManager.instance.StartMenuStarted += OnStartMenuStarted;
        PlayerController.instance.HealthIncreased += OnHealthIncreased;
        PlayerController.instance.HealthMaxed += OnHealthMaxed;
    }

    private void StartGame()
    {
        enemyCounter = 0;
        obstacleCounter = 0;
        healthCounter = 0;
        powerupCounter = 0;

        lastSpawnTimeEnemy = 0.0f;
        lastSpawnTimeObstacle = 0.0f;
        lastSpawnTimePowerup = 0.0f;

        repeatDelayEnemy = repeatDelayEnemyMax;
        repeatDelayObstacle = repeatDelayObstacleMax;

        healthState = HealthState.None;
        startDelayHealth = 0;

        powerupState = PowerupState.ReadyToSpawn;
        startDelayPowerup = 0;
    }

    private void Update()
    {
        gameDuration = GetGameDuration();

        RepeatSpawnEnemy();
        RepeatSpawnObstacle();
        RepeatSpawnHealth();
        RepeatSpawnPowerup();
    }

    private float GetGameDuration()
    {
        float gameDuration = GameManager.instance.gameDuration;

        return gameDuration;
    }

    private void PoolHealth()
    {
        Vector3 pos = new Vector3(0, yPosPowerup, zPosEnemy);

        pooledHealth = Instantiate(healthObject, pos, healthObject.gameObject.transform.rotation);

        pooledHealth.SetActive(false);
    }

    private void RepeatSpawnEnemy()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Play)
        {
            if (gameDuration - lastSpawnTimeEnemy >= repeatDelayEnemy)
                SelectRandomEnemy();

            else
                if (gameDuration - startDelayEnemy >= 0 && enemyCounter == 0)
                    SelectRandomEnemy();
        }
    }
    /*
    private void RepeatSpawnEnemy()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Play)
        {
            if (gameDuration - lastSpawnTimeEnemy < repeatDelayEnemyMin)
                return;

            if (gameDuration >= startDelayEnemy + repeatDelayEnemy * enemyCounter)
                SelectRandomEnemy();
        }
    }
    */
    private void RepeatSpawnObstacle()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Play)
        {
            if (gameDuration - lastSpawnTimeObstacle < repeatDelayObstacleMin)
                return;

            if (gameDuration >= startDelayObstacle + repeatDelayObstacle * obstacleCounter)
                SelectRandomObstacle();
        }
    }

    private void RepeatSpawnHealth()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Play)
        {
            if (healthCounter == 0 && healthState == HealthState.ReadyToSpawn)
                healthState = HealthState.AlreadySpawning;

            if (healthState == HealthState.AlreadySpawning && gameDuration - startDelayHealth >= repeatDelayHealth)
                SpawnHealth();
        }
    }

    private void RepeatSpawnPowerup()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Play)
        {
            if (powerupCounter == 0 && powerupState == PowerupState.ReadyToSpawn)
                powerupState = PowerupState.AlreadySpawning;

            if (powerupState == PowerupState.AlreadySpawning && gameDuration - startDelayPowerup >= repeatDelayPowerup)
                SelectRandomPowerup();
        }   
    }

    public void StartSpawningHealth()
    {
        if (healthState == HealthState.AlreadySpawning)
            return;

        healthState = HealthState.ReadyToSpawn;

        SetStartDelayHealth();
    }

    public void DeactivateHealth()
    {
        pooledHealth.SetActive(false);

        healthState = HealthState.ReadyToSpawn;

        if (healthCounter > 0)
            healthCounter--;

        SetStartDelayHealth();
    }

    public void StopSpawningHealth()
    {
        pooledHealth.SetActive(false);

        healthState = HealthState.None;

        if (healthCounter > 0)
            healthCounter--;
    }

    public void StartSpawningPowerup()
    {
        if (powerupState == PowerupState.AlreadySpawning)
            return;

        powerupState = PowerupState.ReadyToSpawn;

        SetStartDelayPowerup();
    }

    public void DeactivatePowerup(GameObject powerup, int id)
    {
        ObjectPool.instance.ReleasePowerup(powerup, id);

        powerupState = PowerupState.ReadyToSpawn;

        if (powerupCounter > 0)
            powerupCounter--;

        SetStartDelayPowerup();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
            GameManager.instance.LevelChanged -= OnLevelChanged;
            PlayerController.instance.HealthIncreased -= OnHealthIncreased;
            PlayerController.instance.HealthMaxed -= OnHealthMaxed;
        }
    }

    private void OnGameRestarted(object sender, System.EventArgs e)
    {
        StartGame();
        pooledHealth.SetActive(false);
    }

    private void OnLevelChanged(object sender, System.EventArgs e)
    {
        CalculateRepeatDelayEnemy();
        CalculateRepeatDelayObstacle();
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        Destroy(pooledHealth);
    }

    private void OnHealthIncreased(object sender, System.EventArgs e)
    {
        DeactivateHealth();
    }

    private void OnHealthMaxed(object sender, System.EventArgs e)
    {
        StopSpawningHealth();
    }

    private void SelectRandomEnemy()
    {
        randomEnemy = UnityEngine.Random.Range(1, randomEnemiesCount + 1);

        switch (randomEnemy)
        {
            case 1:
                SpawnEnemy(ObjectPool.instance.GetPooledEnemy1());
                break;
            case 2:
                SpawnEnemy(ObjectPool.instance.GetPooledEnemy2());
                break;
            case 3:
                SpawnEnemy(ObjectPool.instance.GetPooledEnemy3());
                break;
            default:
                break;
        }
    }

    private void SelectRandomObstacle()
    {
        randomObstacle = UnityEngine.Random.Range(1, randomObstaclesCount + 1);

        switch (randomObstacle)
        {
            case 1:
                SpawnObstacle(ObjectPool.instance.GetPooledObstacle1());
                break;
            case 2:
                SpawnObstacle(ObjectPool.instance.GetPooledObstacle2());
                break;
            default:
                break;
        }
    }

    private void SelectRandomPowerup()
    {
        randomPowerup = UnityEngine.Random.Range(1, randomPowerupCount + 1);

        switch (randomPowerup)
        {
            case 1:
                SpawnPowerup(ObjectPool.instance.GetPooledPowerup1());
                break;
            case 2:
                SpawnPowerup(ObjectPool.instance.GetPooledPowerup2());
                break;
            default:
                break;
        }
    }

    private void SpawnEnemy(GameObject pooledEnemy)
    {
        float xRandomPos = UnityEngine.Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yPosEnemy, zPosEnemy);

        GameObject enemy = pooledEnemy;
        
        if (enemy == null)
            Debug.Log("SpawnEnemy null");
        //return;
        else
        {
            enemy.transform.position = randomPos;
            enemy.transform.rotation = Quaternion.identity;

            enemyCounter++;
            lastSpawnTimeEnemy = gameDuration;
        }
    }

    private void SpawnObstacle(GameObject pooledObstacle)
    {
        float xRandomPos = UnityEngine.Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yPosEnemy, zPosEnemy);

        GameObject obstacle = pooledObstacle;

        if (obstacle == null)
            return;

        obstacle.transform.position = randomPos;
        obstacle.transform.rotation = Quaternion.identity;

        obstacleCounter++;
        lastSpawnTimeObstacle = gameDuration;
    }

    private void SpawnHealth()
    {
        if (healthCounter < maxHealthLimit)
        {
            float xRandomPos = UnityEngine.Random.Range(-xRange, xRange);
            float zRandomPos = UnityEngine.Random.Range(zRangePowerupMin, zRangePowerupMax);
            Vector3 randomPos = new Vector3(xRandomPos, yPosPowerup, zRandomPos);

            pooledHealth.SetActive(true);

            pooledHealth.transform.position = randomPos;

            healthCounter++;
            healthState = HealthState.ReadyToSpawn;
        }
    }

    private void SpawnPowerup(GameObject pooledPowerup)
    {
        if (powerupCounter < maxPowerupLimit)
        {
            float xRandomPos = UnityEngine.Random.Range(-xRange, xRange);
            float zRandomPos = UnityEngine.Random.Range(zRangePowerupMin, zRangePowerupMax);
            Vector3 randomPos = new Vector3(xRandomPos, yPosPowerup, zRandomPos);

            GameObject powerup = pooledPowerup;

            if (powerup == null)
                return;

            powerup.transform.position = randomPos;
            powerup.transform.rotation = Quaternion.identity;

            powerupCounter++;
            powerupState = PowerupState.ReadyToSpawn;

            lastSpawnTimePowerup = gameDuration;

        }
    }

    private void CalculateRepeatDelayEnemy()
    {
        repeatDelayEnemy = repeatDelayEnemyMax - repeatDelayEnemyMax * (GameManager.instance.level - 1) / repeatDelayEnemyMultipliler;

        if (repeatDelayEnemy < repeatDelayEnemyMin)
            repeatDelayEnemy = repeatDelayEnemyMin;
    }

    private void CalculateRepeatDelayObstacle()
    {
        repeatDelayObstacle = repeatDelayObstacleMax - repeatDelayObstacleMax * (GameManager.instance.level - 1) / repeatDelayObstacleMultipliler;

        if (repeatDelayObstacle < repeatDelayObstacleMin)
            repeatDelayObstacle = repeatDelayObstacleMin;
    }

    public void SetStartDelayHealth()
    {
        startDelayHealth = gameDuration;
    }

    public void SetStartDelayPowerup()
    {
        startDelayPowerup = gameDuration;
    }

    public enum HealthState
    {
        None,
        ReadyToSpawn,
        AlreadySpawning
    }

    public enum PowerupState
    {
        None,
        ReadyToSpawn,
        AlreadySpawning
    }

}
