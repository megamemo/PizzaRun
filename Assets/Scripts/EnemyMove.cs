using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5.0f; //Prefabs unique values set in Editor
    private int speedMultiplier = 5;
    private int zDestroy = -10;

    private Transform cashedTransform;


    private void Awake()
    {
        cashedTransform = transform;
    }

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
        MoveDown(CalculateSpeed());
        DeactivateOffScreen();
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

    private void MoveDown(float calculatedSpeed)
    {
        cashedTransform.Translate(Vector3.back * calculatedSpeed * Time.deltaTime);
    }

    private void DeactivateOffScreen()
    {
        if (cashedTransform.position.z < zDestroy)
            Destroy(gameObject);
    }

    private float CalculateSpeed()
    {
        float speed = startSpeed + startSpeed * (GameManager.instance.level - 1) / speedMultiplier;
        return speed;
    }

}
