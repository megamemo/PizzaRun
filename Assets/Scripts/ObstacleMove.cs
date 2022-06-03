using System;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] private int id; //Prefabs unique values set in Editor
    private float startSpeed; //Speed is equal to Ground speed
    private int speedMultiplier;
    private int zDestroy = -10;

    private Rigidbody obstacleRb;


    private void Start()
    {
        StartGame();

        GameManager.instance.GameRestarted += OnGameRestarted;
    }

    private void FixedUpdate()
    {
        MoveDown(CalculateSpeed());
        DeactivateOffScreen();
    }
    
    private void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.GameRestarted -= OnGameRestarted;
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        RestartGame();
    }

    private void StartGame()
    {
        obstacleRb = GetComponent<Rigidbody>();
        startSpeed = GroundMove.instance.startSpeed;
        speedMultiplier = GroundMove.instance.speedMultiplier;
    }

    private void RestartGame()
    {
        gameObject.SetActive(false);

        StartGame();
    }

    private void MoveDown(float calculatedSpeed)
    {
        obstacleRb.position += Vector3.back * calculatedSpeed * Time.deltaTime;
    }

    private void DeactivateOffScreen()
    {
        if (obstacleRb.position.z < zDestroy)
            gameObject.SetActive(false);
    }

    private float CalculateSpeed()
    {
        float speed = startSpeed + startSpeed * (GameManager.instance.level - 1) / speedMultiplier;
        return speed;
    }

}