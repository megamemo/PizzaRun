using System;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField] private int id;

    private float activeDurationLimitMax = 10.0f;
    private float activeDuration;


    private void Start()
    {
        GameManager.instance.GameRestarted += OnGameRestarted;
        GameManager.instance.StartMenuStarted += OnStartMenuStarted;
        PlayerController.instance.Poweruped += OnPoweruped;
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
        if (activeDuration - SpawnManager.instance.lastSpawnTimePowerup >= activeDurationLimitMax)
            SpawnManager.instance.DeactivatePowerup(gameObject, id);
    }

    private void Reset()
    {
        activeDuration = 0;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
            GameManager.instance.StartMenuStarted -= OnStartMenuStarted;
            PlayerController.instance.Poweruped -= OnPoweruped;
        }
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        Reset();

        ObjectsPoolManager.instance.ReleasePowerup(gameObject, id);
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        //Destroy(gameObject);
    }

    private void OnPoweruped(object sender, EventArgs e)
    {
        SpawnManager.instance.DeactivatePowerup(gameObject, id);
    }

}