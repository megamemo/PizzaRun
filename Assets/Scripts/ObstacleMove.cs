using System;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    private float startSpeed; //Speed is equal to Ground speed
    private int speedMultiplier;
    private int zDestroy = -10;
    [SerializeField] private int id;


    private Rigidbody obstacleRb;


    private void Start()
    {
        StartGame();

        GameManager.instance.GameRestarted += OnGameRestarted;
        GameManager.instance.StartMenuStarted += OnStartMenuStarted;
    }

    private void StartGame()
    {
        obstacleRb = GetComponent<Rigidbody>();
        startSpeed = GroundMove.instance.startSpeed;
        speedMultiplier = GroundMove.instance.speedMultiplier;
    }

    private void RestartGame()
    {
        ObjectsPoolManager.instance.ReleaseObstacle(gameObject, id);

        StartGame();
    }

    private void FixedUpdate()
    {
        MoveDown(CalculateSpeed());
        DeactivateOffScreen();
    }
    
    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
            GameManager.instance.StartMenuStarted -= OnStartMenuStarted;
        }
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        RestartGame();
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        //Destroy(gameObject);
    }

    private void MoveDown(float calculatedSpeed)
    {
        obstacleRb.position += Vector3.back * calculatedSpeed * Time.deltaTime;
    }

    private void DeactivateOffScreen()
    {
        if (obstacleRb.position.z < zDestroy)
            ObjectsPoolManager.instance.ReleaseObstacle(gameObject, id);
    }

    private float CalculateSpeed()
    {
        float speed = startSpeed + startSpeed * (GameManager.instance.level - 1) / speedMultiplier;
        return speed;
    }

}