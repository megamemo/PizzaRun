using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; //Prefabs unique values set in Editor
    private int speedMultiplier = 5;
    private int zDestroy = -10;

    private Rigidbody objectRb;

    
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
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
            transform.Translate(Vector3.back * speed * Time.deltaTime);

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
