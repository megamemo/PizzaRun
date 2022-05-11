using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private int speedMultiplier = 5;
    private int zDestroy = -10;

    private Rigidbody objectRb;

    
    private void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        objectRb.AddForce(Vector3.forward * -SpeedLevel(), ForceMode.Force);

        if (transform.position.z < zDestroy)
            Destroy(gameObject);
    }

    private float SpeedLevel()
    {
        float newSpeed = speed + speed * (GameManager.instance.levelCurrent - 1) / speedMultiplier;
        return newSpeed;
    }

}
