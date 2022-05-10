using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private int speed = 5;
    private int speedMultiplier = 5;
    private int startPos = 50;


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
        float newSpeed = speed + speed * (GameManager.instance.levelCurrent - 1) / speedMultiplier;

        return newSpeed;
    }
}
