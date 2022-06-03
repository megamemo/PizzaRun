using System;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField] private int id; //Prefabs unique values set in Editor
    [SerializeField] private float activeDurationLimitMax = 10.0f; //Prefabs unique values set in Editor
    private float activeDuration;


    private void Start()
    {
        GameManager.instance.GameRestarted += OnGameRestarted;
        PlayerController.instance.Poweruped += OnPoweruped;
    }

    private void Update()
    {
        ActivateDurationTimer();
        DeactivateOnDurationLimit();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
            PlayerController.instance.Poweruped -= OnPoweruped;
        }
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        Reset();

        gameObject.SetActive(false);
    }

    private void OnPoweruped(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
        SpawnManager.instance.DeactivatePowerup(gameObject, id);
    }

    private void ActivateDurationTimer()
    {
        if (gameObject.activeInHierarchy)
            activeDuration = GameManager.instance.gameDuration;
    }

    private void DeactivateOnDurationLimit()
    {
        if (activeDuration - SpawnManager.instance.lastSpawnTimePowerup >= activeDurationLimitMax)
        {
            gameObject.SetActive(false);
            SpawnManager.instance.DeactivatePowerup(gameObject, id);
        }
    }

    private void Reset()
    {
        activeDuration = 0.0f;
    }

}