using System;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField] private int id;

    private float activeDurationLimitMax = 10.0f;
    private float activeDuration;
    private float lastSpawnTime;

    private void Start()
    {
        GameManager.instance.GameRestarted += OnGameRestarted;
        GameManager.instance.StartMenuStarted += OnStartMenuStarted;
        PlayerController.instance.Poweruped += OnPoweruped;
        SpawnManager.instance.PowerupSpawned += OnPowerupSpawned;
    }

    private void Update()
    {
        ActivateDurationTimer();
        DeactivateOnDurationLimit();
    }

    private void ActivateDurationTimer()
    {
        if (gameObject.activeInHierarchy)
        {
            activeDuration = GameManager.instance.gameDuration;
        }
    }

    private void DeactivateOnDurationLimit()
    {
        if (activeDuration - lastSpawnTime >= activeDurationLimitMax)
        {
            SpawnManager.instance.DeactivatePowerup(gameObject, id);
        }
    }

    private void Reset()
    {
        activeDuration = 0;
        lastSpawnTime = 0;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
            GameManager.instance.StartMenuStarted -= OnStartMenuStarted;
            PlayerController.instance.Poweruped -= OnPoweruped;
            SpawnManager.instance.PowerupSpawned -= OnPowerupSpawned;
        }
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        Reset();

        ObjectPool.instance.ReleasePowerup(gameObject, id);
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnPoweruped(object sender, EventArgs e)
    {
        SpawnManager.instance.DeactivatePowerup(gameObject, id);
    }

    private void OnPowerupSpawned(object sender, EventArgs e)
    {
        lastSpawnTime = GameManager.instance.gameDuration;
    }

}