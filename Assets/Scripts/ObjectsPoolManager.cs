using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsPoolManager : MonoBehaviour
{
    public static ObjectsPoolManager instance { get; private set; }

    public ObjectPool<GameObject> pooledEnemy1 { get; private set; }
    public ObjectPool<GameObject> pooledEnemy2 { get; private set; }
    public ObjectPool<GameObject> pooledEnemy3 { get; private set; }

    public ObjectPool<GameObject> pooledObstacle1 { get; private set; }
    public ObjectPool<GameObject> pooledObstacle2 { get; private set; }

    public ObjectPool<GameObject> pooledPowerup1 { get; private set; }
    public ObjectPool<GameObject> pooledPowerup2 { get; private set; }

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
            GameManager.instance.StartMenuStarted += OnStartMenuStarted;
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        DestroyOnExit();
    }

    private void DestroyOnExit()
    {
        pooledEnemy1.Dispose();
        pooledEnemy2.Dispose();
        pooledEnemy3.Dispose();
        pooledObstacle1.Dispose();
        pooledObstacle2.Dispose();
        pooledPowerup1.Dispose();
        pooledPowerup2.Dispose();
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

    public void ReleaseEnemy(GameObject releasedObject, int id)
    {
        if (id == 1)
            pooledEnemy1.Release(releasedObject);

        if (id == 2)
            pooledEnemy2.Release(releasedObject);

        if (id == 3)
            pooledEnemy3.Release(releasedObject);
    }

    public void ReleaseObstacle(GameObject releasedObject, int id)
    {
       if (id == 1)
            pooledObstacle1.Release(releasedObject);

        if (id == 2)
            pooledObstacle2.Release(releasedObject);
    }

    public void ReleasePowerup(GameObject releasedObject, int id)
    {
        if (id == 1)
            pooledPowerup1.Release(releasedObject);

        if (id == 2)
            pooledPowerup2.Release(releasedObject);
    }

    public void PoolEnemy1()
    {
        pooledEnemy1 = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(enemy1ToPool),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 3,
            maxSize: 10);
    }

    public void PoolEnemy2()
    {
        pooledEnemy2 = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(enemy2ToPool),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: warmupEnemy2,
            maxSize: 10);
    }

    public void PoolEnemy3()
    {
        pooledEnemy3 = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(enemy3ToPool),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: warmupEnemy3,
            maxSize: 10);
    }

    public void PoolObstacle1()
    {
        pooledObstacle1 = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(obstacle1ToPool),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: warmupObstacle1,
            maxSize: 10);
    }

    public void PoolObstacle2()
    {
        pooledObstacle2 = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(obstacle2ToPool),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: warmupObstacle2,
            maxSize: 10);
    }

    public void PoolPowerup1()
    {
        pooledPowerup1 = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(powerup1ToPool),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: warmupPoweup1,
            maxSize: 2);
    }

    public void PoolPowerup2()
    {
        pooledPowerup2 = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(powerup2ToPool),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: warmupPoweup2,
            maxSize: 2);
    }

}