using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    private float speed = 5.0f;
    private int speedMultiplier = 5;
    private int zDestroy = -10;


    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * -SpeedLevel() * Time.deltaTime);

        if (transform.position.z < zDestroy)
            Destroy(gameObject);
    }

    private float SpeedLevel()
    {
        float newSpeed = speed + speed * (GameManager.instance.levelCurrent - 1) / speedMultiplier;
        return newSpeed;
    }

}