using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 5.0f;
    private int speedMultiplier = 5;
    private int zDestroy = -10;

    private Rigidbody objectRb;
    private GameManager gameManager;

    
    void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        objectRb.AddForce(Vector3.forward * -SpeedLevel(), ForceMode.Force);

        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }

    float SpeedLevel()
    {
        float newSpeed = speed + speed * (gameManager.levelCurrent - 1) / speedMultiplier;
        
        return newSpeed;
    }
}
