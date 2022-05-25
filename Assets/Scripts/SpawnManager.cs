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
    private int startDelayPowerup = 15;

    private float repeatDelayEnemy = 3.5f;
    private float repeatDelayEnemyNew;
    private float repeatDelayEnemyMin = 0.5f;
    private int repeatDelayEnemyMultipliler = 15;

    private int repeatDelayObstacle = 10;
    private int repeatDelayPowerup = 20;
    [HideInInspector] public int currentPowerupCount;
    private int maxPowerupCount = 1;


    private void Start()
    {
        StartGame();

        GameManager.instance.GameRestarted += OnGameRestarted;
    }

    private void StartGame()
    {
        currentPowerupCount = 1;


        //Change Coroutines
        Invoke("SpawnRandomEnemy", startDelayEnemy);
        StartCoroutine(SpawnRepeatEnemy());
        InvokeRepeating("SpawnRandomObstacle", startDelayObstacle, repeatDelayObstacle);
        InvokeRepeating("SpawnPowerup", startDelayPowerup, repeatDelayPowerup);
    }

    private void Update()
    {
        DecreaseRepeatDelayEnemy();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
        }
    }

    private void OnGameRestarted(object sender, System.EventArgs e)
    {
        CancelInvoke();
        StopAllCoroutines();
        Destroy(instantiatedPowerup);
        StartGame();
    }

    private void SpawnRandomEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yPosEnemy, zPosEnemy);

        Instantiate(enemies[randomEnemy], randomPos, enemies[randomEnemy].gameObject.transform.rotation);
    }

    private IEnumerator SpawnRepeatEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatDelayEnemyNew);
            SpawnRandomEnemy();
        }
    }

    private void DecreaseRepeatDelayEnemy()
    {
        repeatDelayEnemyNew = repeatDelayEnemy - repeatDelayEnemy * (GameManager.instance.level - 1) / repeatDelayEnemyMultipliler;

        if (repeatDelayEnemyNew < repeatDelayEnemyMin)
            repeatDelayEnemyNew = repeatDelayEnemyMin;
    }

    private void SpawnRandomObstacle()
    {
        int randomEnemy = Random.Range(0, obstacles.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yPosEnemy, zPosEnemy);

        Instantiate(obstacles[randomEnemy], randomPos, obstacles[randomEnemy].gameObject.transform.rotation);
    }

    private void SpawnPowerup()
    {
        if (currentPowerupCount <= maxPowerupCount)
        {
            float xRandomPos = Random.Range(-xRange, xRange);
            float zRandomPos = Random.Range(-zRangePowerup, zRangePowerup);
            Vector3 randomPos = new Vector3(xRandomPos, yPosPowerup, zRandomPos);

            instantiatedPowerup = Instantiate(powerup, randomPos, powerup.gameObject.transform.rotation);
            currentPowerupCount++;
        }
    }

}
