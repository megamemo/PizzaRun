using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolManager : MonoBehaviour
{
    public static ObjectsPoolManager instance { get; private set; }

    private List<GameObject> pooledEnemy1;
    private List<GameObject> pooledEnemy2;
    private List<GameObject> pooledEnemy3;

    private List<GameObject> pooledObstacle1;
    private List<GameObject> pooledObstacle2;

    private List<GameObject> pooledPowerup1;
    private List<GameObject> pooledPowerup2;

    [SerializeField] private GameObject enemy1ToPool;
    [SerializeField] private GameObject enemy2ToPool;
    [SerializeField] private GameObject enemy3ToPool;

    [SerializeField] private GameObject obstacle1ToPool;
    [SerializeField] private GameObject obstacle2ToPool;

    [SerializeField] private GameObject powerup1ToPool;
    [SerializeField] private GameObject powerup2ToPool;

    [SerializeField] private int warmupEnemy1;
    [SerializeField] private int warmupEnemy2;
    [SerializeField] private int warmupEnemy3;

    [SerializeField] private int warmupObstacle1;
    [SerializeField] private int warmupObstacle2;

    [SerializeField] private int warmupPoweup1;
    [SerializeField] private int warmupPoweup2;


    private void Awake()
    {
        InstanciateObjectsPool();
    }

    private void Start()
    {
        PoolObjects();

        GameManager.instance.MainMenuStarted += OnStartMenuStarted;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.MainMenuStarted += OnStartMenuStarted;
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        DestroyOnExit();
    }

    private void PoolObjects()
    {
        PoolEnemy1();
        PoolEnemy2();
        PoolEnemy3();
        PoolObstacle1();
        PoolObstacle2();
        PoolPowerup1();
        PoolPowerup2();
    }

    private void DestroyOnExit()
    {
        DestroyPooledEnemy1();
        DestroyPooledEnemy2();
        DestroyPooledEnemy3();
        DestroyPooledObstacle1();
        DestroyPooledObstacle2();
        DestroyPooledPowerup1();
        DestroyPooledPowerup2();
    }

    public GameObject GetPooledEnemy1()
    {
        for (int i = 0; i < pooledEnemy1.Count; i++)
        {
            if (!pooledEnemy1[i].activeSelf)
                return pooledEnemy1[i];
        }
        return null;
    }

    public GameObject GetPooledEnemy2()
    {
        for (int i = 0; i < pooledEnemy2.Count; i++)
        {
            if (!pooledEnemy2[i].activeSelf)
                return pooledEnemy2[i];
        }
        return null;
    }

    public GameObject GetPooledEnemy3()
    {
        for (int i = 0; i < pooledEnemy3.Count; i++)
        {
            if (!pooledEnemy3[i].activeSelf)
                return pooledEnemy3[i];
        }
        return null;
    }

    public GameObject GetPooledObstacle1()
    {
        for (int i = 0; i < pooledObstacle1.Count; i++)
        {
            if (!pooledObstacle1[i].activeSelf)
                return pooledObstacle1[i];
        }
        return null;
    }

    public GameObject GetPooledObstacle2()
    {
        for (int i = 0; i < pooledObstacle2.Count; i++)
        {
            if (!pooledObstacle2[i].activeSelf)
                return pooledObstacle2[i];
        }
        return null;
    }

    public GameObject GetPooledPowerup1()
    {
        for (int i = 0; i < pooledPowerup1.Count; i++)
        {
            if (!pooledPowerup1[i].activeSelf)
                return pooledPowerup1[i];
        }
        return null;
    }

    public GameObject GetPooledPowerup2()
    {
        for (int i = 0; i < pooledPowerup2.Count; i++)
        {
            if (!pooledPowerup2[i].activeSelf)
                return pooledPowerup2[i];
        }
        return null;
    }

    private void PoolEnemy1()
    {
        pooledEnemy1 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupEnemy1; i++)
        {
            tmp = Instantiate(enemy1ToPool);
            tmp.SetActive(false);
            pooledEnemy1.Add(tmp);
        }
    }

    private void PoolEnemy2()
    {
        pooledEnemy2 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupEnemy2; i++)
        {
            tmp = Instantiate(enemy2ToPool);
            tmp.SetActive(false);
            pooledEnemy2.Add(tmp);
        }
    }

    private void PoolEnemy3()
    {
        pooledEnemy3 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupEnemy3; i++)
        {
            tmp = Instantiate(enemy3ToPool);
            tmp.SetActive(false);
            pooledEnemy3.Add(tmp);
        }
    }

    private void PoolObstacle1()
    {
        pooledObstacle1 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupObstacle1; i++)
        {
            tmp = Instantiate(obstacle1ToPool);
            tmp.SetActive(false);
            pooledObstacle1.Add(tmp);
        }
    }

    private void PoolObstacle2()
    {
        pooledObstacle2 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupObstacle2; i++)
        {
            tmp = Instantiate(obstacle2ToPool);
            tmp.SetActive(false);
            pooledObstacle2.Add(tmp);
        }
    }

    private void PoolPowerup1()
    {
        pooledPowerup1 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupPoweup1; i++)
        {
            tmp = Instantiate(powerup1ToPool);
            tmp.SetActive(false);
            pooledPowerup1.Add(tmp);
        }
    }

    private void PoolPowerup2()
    {
        pooledPowerup2 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupPoweup2; i++)
        {
            tmp = Instantiate(powerup2ToPool);
            tmp.SetActive(false);
            pooledPowerup2.Add(tmp);
        }
    }

    private void DestroyPooledEnemy1()
    {
        for (int i = 0; i < pooledEnemy1.Count; i++)
        {
            Destroy(pooledEnemy1[i]);
        }

        pooledEnemy1.Clear();
    }

    private void DestroyPooledEnemy2()
    {
        for (int i = 0; i < pooledEnemy2.Count; i++)
        {
            Destroy(pooledEnemy2[i]);
        }

        pooledEnemy2.Clear();
    }

    private void DestroyPooledEnemy3()
    {
        for (int i = 0; i < pooledEnemy3.Count; i++)
        {
            Destroy(pooledEnemy3[i]);
        }

        pooledEnemy3.Clear();
    }

    private void DestroyPooledObstacle1()
    {
        for (int i = 0; i < pooledObstacle1.Count; i++)
        {
            Destroy(pooledObstacle1[i]);
        }

        pooledObstacle1.Clear();
    }

    private void DestroyPooledObstacle2()
    {
        for (int i = 0; i < pooledObstacle2.Count; i++)
        {
            Destroy(pooledObstacle2[i]);
        }

        pooledObstacle2.Clear();
    }

    private void DestroyPooledPowerup1()
    {
        for (int i = 0; i < pooledPowerup1.Count; i++)
        {
            Destroy(pooledPowerup1[i]);
        }

        pooledPowerup1.Clear();
    }

    private void DestroyPooledPowerup2()
    {
        for (int i = 0; i < pooledPowerup2.Count; i++)
        {
            Destroy(pooledPowerup2[i]);
        }

        pooledPowerup2.Clear();
    }

    private void InstanciateObjectsPool()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

}