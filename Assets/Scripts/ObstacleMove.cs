using System;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    private float speed = 5.0f;
    private int speedMultiplier = 5;
    private int zDestroy = -10;


    private void Start()
    {
        GameManager.instance.GameRestarted += OnGameRestarted;
    }

    private void StartGame()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * -SpeedLevel() * Time.deltaTime);

        if (transform.position.z < zDestroy)
            Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
        }
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        StartGame();
    }

    private float SpeedLevel()
    {
        float newSpeed = speed + speed * (GameManager.instance.level - 1) / speedMultiplier;
        return newSpeed;
    }

}