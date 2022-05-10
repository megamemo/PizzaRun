using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 5.0f;
    private int speedMultiplier = 5;
    private int zDestroy = -10;


    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * -SpeedLevel() * Time.deltaTime);

        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }

    float SpeedLevel()
    {
        float newSpeed = speed + speed * (GameManager.instance.levelCurrent - 1) / speedMultiplier;

        return newSpeed;
    }
}