using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private int speed = 5;
    private int speedMultiplier = 5;
    private int startPos = 50;

    [SerializeField] GameObject gameManagerObject;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (transform.position.z <= 0.0f)
        {
            transform.Translate(transform.position.x, transform.position.y, startPos);
        }
        else
        {
            transform.Translate(Vector3.back * SpeedLevel() * Time.deltaTime);
        }
    }

    float SpeedLevel()
    {
        float newSpeed = speed + speed * gameManager.levelCurrent / speedMultiplier;

        Debug.Log("Ground speed" + newSpeed);
        return newSpeed;
    }
}
