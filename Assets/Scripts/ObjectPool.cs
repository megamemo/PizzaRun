using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }

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
        for (int i = 0; i < pooledEnemy1.Count; i++)
        {
            var pooledObject = pooledEnemy1[i];
            pooledEnemy1.RemoveAt(i);
            pooledObject.SetActive(true);
            return pooledObject;
        }
        return null;
    }

    public GameObject GetPooledEnemy2()
    {
        for (int i = 0; i < pooledEnemy2.Count; i++)
        {
            var pooledObject = pooledEnemy2[i];
            pooledEnemy2.RemoveAt(i);
            pooledObject.SetActive(true);
            return pooledObject;
        }
        return null;
    }

    public GameObject GetPooledEnemy3()
    {
        for (int i = 0; i < pooledEnemy3.Count; i++)
        {
            var pooledObject = pooledEnemy3[i];
            pooledEnemy3.RemoveAt(i);
            pooledObject.SetActive(true);
            return pooledObject;
        }
        return null;
    }

    public GameObject GetPooledObstacle1()
    {
        for (int i = 0; i < pooledObstacle1.Count; i++)
        {
            var pooledObject = pooledObstacle1[i];
            pooledObstacle1.RemoveAt(i);
            pooledObject.SetActive(true);
            return pooledObject;
        }
        return null;
    }

    public GameObject GetPooledObstacle2()
    {
        for (int i = 0; i < pooledObstacle2.Count; i++)
        {
            var pooledObject = pooledObstacle2[i];
            pooledObstacle2.RemoveAt(i);
            pooledObject.SetActive(true);
            return pooledObject;
        }
        return null;
    }

    public GameObject GetPooledPowerup1()
    {
        for (int i = 0; i < pooledPowerup1.Count; i++)
        {
            var pooledObject = pooledPowerup1[i];
            pooledPowerup1[i].SetActive(true);
            pooledPowerup1.RemoveAt(i);
            return pooledObject;
        }
        return null;
    }

    public GameObject GetPooledPowerup2()
    {
        for (int i = 0; i < pooledPowerup2.Count; i++)
        {
            var pooledObject = pooledPowerup2[i];
            pooledPowerup2[i].SetActive(true);
            pooledPowerup2.RemoveAt(i);
            return pooledObject;
        }
        return null;
    }

    public void ReleaseEnemy(GameObject releasedObject, int id)
    {
        releasedObject.SetActive(false);

        if (id == 1)
            pooledEnemy1.Add(releasedObject);

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
