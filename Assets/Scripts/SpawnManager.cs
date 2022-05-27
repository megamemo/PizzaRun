using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject powerup;
    private GameObject instantiatedPowerup;

    private float xRange = 15.5f;
    private float yPosEnemy = 0.1f;
    private float zPosEnemy = 17.0f;
    private float yPosPowerup = 0.75f;
    private float zRangePowerup = 5.0f;

    private int startDelayEnemy = 1;
    private int startDelayObstacle = 3;
    private float startDelayPowerup;

    private float repeatDelayEnemyMax = 3.5f;
    private float repeatDelayEnemyMin = 0.5f;
    private float repeatDelayEnemy;
    private int repeatDelayEnemyMultipliler = 15;

    private float repeatDelayObstacleMax = 10.0f;
    private float repeatDelayObstacleMin = 2.0f;
    private float repeatDelayObstacle;
    private int repeatDelayObstacleMultipliler = 5;

    private int repeatDelayPowerup = 20;

    private int enemyCounter;
    private int obstacleCounter;
    private int maxPowerupLimit = 1;
    [HideInInspector] public int powerupCounter;

    private float gameDuration;
    private float spawnTimeEnemy;
    private float spawnTimeObstacle;


    private void Start()
    {
        StartGame();

        GameManager.instance.GameRestarted += OnGameRestarted;
        GameManager.instance.LevelChanged += OnLevelChanged;
    }

    private void StartGame()
    {
        enemyCounter = 0;
        obstacleCounter = 0;
        powerupCounter = 0;

        spawnTimeEnemy = 0.0f;
        spawnTimeObstacle = 0.0f;
        repeatDelayEnemy = repeatDelayEnemyMax;
        repeatDelayObstacle = repeatDelayObstacleMax;

        SetStartDelayPowerup();
    }

    private void Update()
    {
        gameDuration = GetGameDuration();

        RepeatSpawnEnemy();
        RepeatSpawnObstacle();
        RepeatSpawnPowerup();
    }

    private float GetGameDuration()
    {
        float gameDuration = GameManager.instance.gameDuration;

        return gameDuration;
    }

    private void RepeatSpawnEnemy()
    {
        if (GameManager.instance.state == GameManager.GameState.Play)
        {
            if (gameDuration - spawnTimeEnemy < repeatDelayEnemyMin)
                return;

            if (gameDuration >= startDelayEnemy + repeatDelayEnemy * enemyCounter)
            {
                SpawnRandomEnemy();
            }
        }
    }

    private void RepeatSpawnObstacle()
    {
        if (GameManager.instance.state == GameManager.GameState.Play)
        {
            if (gameDuration - spawnTimeObstacle < repeatDelayObstacleMin)
                return;

            if (gameDuration >= startDelayObstacle + repeatDelayObstacle * obstacleCounter)
            {
                SpawnRandomObstacle();
            }
        }
    }

    private void RepeatSpawnPowerup()
    {
        if (GameManager.instance.state == GameManager.GameState.Play)
        {
            if (powerupCounter == 0)
            {
                if (gameDuration - startDelayPowerup >= repeatDelayPowerup)
                {
                    SpawnPowerup();
                }
            }
        }   
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.GameRestarted -= OnGameRestarted;
            GameManager.instance.LevelChanged -= OnLevelChanged;
    }

    private void OnGameRestarted(object sender, System.EventArgs e)
    {
        Destroy(instantiatedPowerup);
        StartGame();
    }

    private void OnLevelChanged(object sender, System.EventArgs e)
    {
        CalculateRepeatDelayEnemy();
        CalculateRepeatDelayObstacle();
    }

    private void SpawnRandomEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yPosEnemy, zPosEnemy);

        Instantiate(enemies[randomEnemy], randomPos, enemies[randomEnemy].gameObject.transform.rotation);

        enemyCounter++;
        spawnTimeEnemy = gameDuration;
    }

    private void SpawnRandomObstacle()
    {
        int randomEnemy = Random.Range(0, obstacles.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yPosEnemy, zPosEnemy);

        Instantiate(obstacles[randomEnemy], randomPos, obstacles[randomEnemy].gameObject.transform.rotation);

        obstacleCounter++;
        spawnTimeObstacle = gameDuration;
    }

    private void SpawnPowerup()
    {
        if (powerupCounter < maxPowerupLimit)
        {
            float xRandomPos = Random.Range(-xRange, xRange);
            float zRandomPos = Random.Range(-zRangePowerup, zRangePowerup);
            Vector3 randomPos = new Vector3(xRandomPos, yPosPowerup, zRandomPos);

            instantiatedPowerup = Instantiate(powerup, randomPos, powerup.gameObject.transform.rotation);

            powerupCounter++;
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

    public void SetStartDelayPowerup()
    {
        startDelayPowerup = gameDuration;
    }

}
