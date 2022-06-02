using System.Collections.Generic;
using UnityEngine;

public class ObjectPool1 : MonoBehaviour
{
    public static ObjectPool1 instance { get; private set; }

    public List<GameObject> pooledEnemy1 { get; private set; }
    public List<GameObject> pooledEnemy2 { get; private set; }
    public List<GameObject> pooledEnemy3 { get; private set; }

    public List<GameObject> pooledObstacle1 { get; private set; }
    public List<GameObject> pooledObstacle2 { get; private set; }

    public List<GameObject> pooledPowerup1 { get; private set; }
    public List<GameObject> pooledPowerup2 { get; private set; }

    [SerializeField] private GameObject enemy1ToPool;
    [SerializeField] private GameObject enemy2ToPool;
    [SerializeField] private GameObject enemy3ToPool;

    [SerializeField] private GameObject obstacle1ToPool;
    [SerializeField] private GameObject obstacle2ToPool;

    [SerializeField] private GameObject powerup1ToPool;
    [SerializeField] private GameObject powerup2ToPool;

    [SerializeField] private int warmupEnemies1;
    [SerializeField] private int warmupEnemies2;
    [SerializeField] private int warmupEnemies3;

    [SerializeField] private int warmupObstacles1;
    [SerializeField] private int warmupObstacles2;

    [SerializeField] private int warmupPoweups1;
    [SerializeField] private int warmupPoweups2;

    private void Awake()
    {
        InstanciateObjectsPool();
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

    private void Start()
    {
        PoolObjects();

        GameManager.instance.StartMenuStarted += OnStartMenuStarted;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.StartMenuStarted += OnStartMenuStarted;
        }
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        DestroyOnExit();
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

    public GameObject GetPooledEnemy1()
    {
        var pooledObject = pooledEnemy1[0];
        pooledObject.SetActive(true);
        pooledEnemy1.RemoveAt(0);
        return pooledObject;
    }

    public GameObject GetPooledEnemy2()
    {
        var pooledObject = pooledEnemy2[0];
        pooledObject.SetActive(true);
        pooledEnemy2.RemoveAt(0);
        return pooledObject;
        /*
        if (pooledEnemy2.Count == 0)
            return null;

        else
        {
            var pooledObject = pooledEnemy2[0];
            pooledObject.SetActive(true);
            pooledEnemy2.RemoveAt(0);
            return pooledObject;
        }
        */
    }

    public GameObject GetPooledEnemy3()
    {
        var pooledObject = pooledEnemy3[0];
        pooledObject.SetActive(true);
        pooledEnemy3.RemoveAt(0);
        return pooledObject;
        /*
        if (pooledEnemy3.Count == 0)
            return null;

        else
        {
            var pooledObject = pooledEnemy3[0];
            pooledObject.SetActive(true);
            pooledEnemy3.RemoveAt(0);
            return pooledObject;
        }
        */
    }

    public GameObject GetPooledObstacle1()
    {
        if (pooledObstacle1.Count == 0)
            return null;

        else
        {
            var pooledObject = pooledObstacle1[0];
            pooledObject.SetActive(true);
            pooledObstacle1.RemoveAt(0);
            return pooledObject;
        }
    }

    public GameObject GetPooledObstacle2()
    {
        if (pooledObstacle2.Count == 0)
            return null;

        else
        {
            var pooledObject = pooledObstacle2[0];
            pooledObject.SetActive(true);
            pooledObstacle2.RemoveAt(0);
            return pooledObject;
        }
    }

    public GameObject GetPooledPowerup1()
    {
        if (pooledPowerup1.Count == 0)
            return null;

        else
        {
            var pooledObject = pooledPowerup1[0];
            pooledObject.SetActive(true);
            pooledPowerup1.RemoveAt(0);
            return pooledObject;
        }
    }

    public GameObject GetPooledPowerup2()
    {
        if (pooledPowerup2.Count == 0)
            return null;

        else
        {
            var pooledObject = pooledPowerup2[0];
            pooledObject.SetActive(true);
            pooledPowerup2.RemoveAt(0);
            return pooledObject;
        }
    }

    public void ReleaseEnemy(GameObject releasedObject, int id)
    {
        releasedObject.SetActive(false);

        if (id == 1)
        {
            pooledEnemy1.Add(releasedObject);
        }

        if (id == 2)
            pooledEnemy2.Add(releasedObject);

        if (id == 3)
            pooledEnemy2.Add(releasedObject);

    }

    public void ReleaseObstacle(GameObject releasedObject, int id)
    {
        releasedObject.SetActive(false);

        if (id == 1)
            pooledObstacle1.Add(releasedObject);

        if (id == 2)
            pooledObstacle2.Add(releasedObject);
    }

    public void ReleasePowerup(GameObject releasedObject, int id)
    {
        releasedObject.SetActive(false);

        if (id == 1)
            pooledPowerup1.Add(releasedObject);

        if (id == 2)
            pooledPowerup2.Add(releasedObject);
    }

    public void PoolEnemy1()
    {
        pooledEnemy1 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupEnemies1; i++)
        {
            tmp = Instantiate(enemy1ToPool);
            tmp.SetActive(false);
            pooledEnemy1.Add(tmp);
        }
    }

    public void PoolEnemy2()
    {
        pooledEnemy2 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupEnemies2; i++)
        {
            tmp = Instantiate(enemy2ToPool);
            tmp.SetActive(false);
            pooledEnemy2.Add(tmp);
        }
    }

    public void PoolEnemy3()
    {
        pooledEnemy3 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupEnemies3; i++)
        {
            tmp = Instantiate(enemy3ToPool);
            tmp.SetActive(false);
            pooledEnemy3.Add(tmp);
        }
    }

    public void PoolObstacle1()
    {
        pooledObstacle1 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupObstacles1; i++)
        {
            tmp = Instantiate(obstacle1ToPool);
            tmp.SetActive(false);
            pooledObstacle1.Add(tmp);
        }
    }

    public void PoolObstacle2()
    {
        pooledObstacle2 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupObstacles2; i++)
        {
            tmp = Instantiate(obstacle2ToPool);
            tmp.SetActive(false);
            pooledObstacle2.Add(tmp);
        }
    }

    public void PoolPowerup1()
    {
        pooledPowerup1 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupPoweups1; i++)
        {
            tmp = Instantiate(powerup1ToPool);
            tmp.SetActive(false);
            pooledPowerup1.Add(tmp);
        }
    }

    public void PoolPowerup2()
    {
        pooledPowerup2 = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < warmupPoweups2; i++)
        {
            tmp = Instantiate(powerup2ToPool);
            tmp.SetActive(false);
            pooledPowerup2.Add(tmp);
        }
    }

    public void DestroyPooledEnemy1()
    {
        for (int i = 0; i < pooledEnemy1.Count; i++)
        {
            Destroy(pooledEnemy1[i]);
        }
    }

    public void DestroyPooledEnemy2()
    {
        for (int i = 0; i < pooledEnemy2.Count; i++)
        {
            Destroy(pooledEnemy2[i]);
        }
    }

    public void DestroyPooledEnemy3()
    {
        for (int i = 0; i < pooledEnemy3.Count; i++)
        {
            Destroy(pooledEnemy3[i]);
        }
    }

    public void DestroyPooledObstacle1()
    {
        for (int i = 0; i < pooledObstacle1.Count; i++)
        {
            Destroy(pooledObstacle1[i]);
        }
    }

    public void DestroyPooledObstacle2()
    {
        for (int i = 0; i < pooledObstacle2.Count; i++)
        {
            Destroy(pooledObstacle2[i]);
        }
    }

    public void DestroyPooledPowerup1()
    {
        for (int i = 0; i < pooledPowerup1.Count; i++)
        {
            Destroy(pooledPowerup1[i]);
        }
    }

    public void DestroyPooledPowerup2()
    {
        for (int i = 0; i < pooledPowerup2.Count; i++)
        {
            Destroy(pooledPowerup2[i]);
        }
    }

}
