using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 5.0f;
    private int speedMultiplier = 5;
    private int zDestroy = -10;

    private Rigidbody objectRb;

    
    void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
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
        float newSpeed = speed + speed * (GameManager.instance.levelCurrent - 1) / speedMultiplier;
        
        return newSpeed;
    }
}
