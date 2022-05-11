using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] obstacles;
    public GameObject powerup;

    private float xRange = 15.5f;
    private float yEnemyPos = 0.1f;
    private float zEnemyPos = 17.0f;
    private float yPowerupPos = 0.75f;
    private float zPowerupRange = 5.0f;

    private int startTimeEnemy = 1;
    private int startTimeObstacle = 3;
    private int startTimePowerup = 15;

    private float repeatTimeEnemy = 3.5f;
    private float repeatTimeEnemyNew;
    private float repeatTimeEnemyMin = 0.5f;
    private int repeatTimeEnemyMultipliler = 15;

    private int repeatTimeObstacle = 10;
    private int repeatTimePowerup = 20;
    [HideInInspector] public int currentPowerupCount;
    private int limitPowerupCount = 1;


    private void Awake()
    {
        currentPowerupCount = 1;
        Invoke("SpawnRandomEnemy", startTimeEnemy);
        StartCoroutine(SpawnRepeatEnemy());
        InvokeRepeating("SpawnRandomObstacle", startTimeObstacle, repeatTimeObstacle);
        InvokeRepeating("SpawnPowerup", startTimePowerup, repeatTimePowerup);
    }

    private void FixedUpdate()
    {
        RepeatEnemy();
    }

    private void SpawnRandomEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yEnemyPos, zEnemyPos);

        Instantiate(enemies[randomEnemy], randomPos, enemies[randomEnemy].gameObject.transform.rotation);
    }

    private IEnumerator SpawnRepeatEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatTimeEnemyNew);
            SpawnRandomEnemy();
        }
    }

    private void RepeatEnemy()
    {
        repeatTimeEnemyNew = repeatTimeEnemy - repeatTimeEnemy * (GameManager.instance.levelCurrent - 1) / repeatTimeEnemyMultipliler;

        if (repeatTimeEnemyNew < repeatTimeEnemyMin)
            repeatTimeEnemyNew = repeatTimeEnemyMin;
    }

    private void SpawnRandomObstacle()
    {
        int randomEnemy = Random.Range(0, obstacles.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yEnemyPos, zEnemyPos);

        Instantiate(obstacles[randomEnemy], randomPos, obstacles[randomEnemy].gameObject.transform.rotation);
    }

    private void SpawnPowerup()
    {
        if (currentPowerupCount <= limitPowerupCount)
        {
            float xRandomPos = Random.Range(-xRange, xRange);
            float zRandomPos = Random.Range(-zPowerupRange, zPowerupRange);
            Vector3 randomPos = new Vector3(xRandomPos, yPowerupPos, zRandomPos);

            Instantiate(powerup, randomPos, powerup.gameObject.transform.rotation);
            currentPowerupCount++;
        }
    }

}
