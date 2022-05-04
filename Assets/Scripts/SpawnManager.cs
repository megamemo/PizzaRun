using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] obstacles;
    public GameObject powerup;

    public int randomEnemy;

    private float xRange = 15.5f;
    private float yEnemyPos = 0.1f;
    private float zEnemyPos = 16.0f;
    private float yPowerupPos = 0.75f;
    private float zPowerupRange = 5.0f;

    private int startTimeEnemy = 2;
    private int startTimeObstacle = 3;
    private int startTimePowerup = 15;
    private int repeatTimeEnemy = 3;
    private int repeatTimeObstacle = 10;
    private int repeatTimePowerup = 20;
    [HideInInspector] public int currentPowerupCount;
    private int limitPowerupCount = 1;


    void Awake()
    {
        currentPowerupCount = 1;
        InvokeRepeating("SpawnRandomEnemy", startTimeEnemy, repeatTimeEnemy);
        InvokeRepeating("SpawnRandomObstacle", startTimeObstacle, repeatTimeObstacle);
        InvokeRepeating("SpawnPowerup", startTimePowerup, repeatTimePowerup);
    }

    void SpawnRandomEnemy()
    {
        randomEnemy = Random.Range(0, enemies.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yEnemyPos, zEnemyPos);

        Instantiate(enemies[randomEnemy], randomPos, enemies[randomEnemy].gameObject.transform.rotation);
    }

    void SpawnRandomObstacle()
    {
        int randomEnemy = Random.Range(0, obstacles.Length);
        float xRandomPos = Random.Range(-xRange, xRange);
        Vector3 randomPos = new Vector3(xRandomPos, yEnemyPos, zEnemyPos);

        Instantiate(obstacles[randomEnemy], randomPos, obstacles[randomEnemy].gameObject.transform.rotation);
    }

    void SpawnPowerup()
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
