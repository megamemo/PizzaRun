using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private float speed = 5.0f;
    private int speedMultiplier = 5;
    private int startPos = 50;


    private void Update()
    {
        if (transform.position.z <= 0.0f)
            transform.Translate(transform.position.x, transform.position.y, startPos);
        else
            transform.Translate(Vector3.back * SpeedLevel() * Time.deltaTime);
    }

    private float SpeedLevel()
    {
        float newSpeed = speed + speed * (GameManager.instance.level - 1) / speedMultiplier;
        return newSpeed;
    }

}
