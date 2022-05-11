using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 0.7f;
    private float jumpForce = 80.0f;
    private int topBound = 13;
    private int bottomBound = 5;
    private int playerHealth;

    private Rigidbody playerRb;
    [SerializeField] private GameObject spawnObject;
    private SpawnManager spawnManager;
    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    [SerializeField] private GameObject damageLight;
    [SerializeField] private GameObject healthLight;
    [SerializeField] private AudioSource healthSound;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private AudioSource jumpSound;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        spawnManager = spawnObject.GetComponent<SpawnManager>();
        playerHealth = 3;
        HealthUpdate();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ConstrainPlayerMovement();
    }

    private void MovePlayer()
    {
        if (transform.position.y < 0.5f)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            if (!Mathf.Approximately(verticalInput, 0))
                playerRb.AddForce(Vector3.forward * verticalInput * speed, ForceMode.VelocityChange);

            if (!Mathf.Approximately(horizontalInput, 0))
                playerRb.AddForce(Vector3.right * horizontalInput * speed, ForceMode.VelocityChange);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(Vector3.up * jumpForce * Time.fixedDeltaTime);
                jumpSound.Play();
            }
        }
    }

    private void ConstrainPlayerMovement()
    {
        if (transform.position.z > topBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);

        if (transform.position.z < -bottomBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, -bottomBound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (playerHealth > 0)
            {
                damageLight.gameObject.SetActive(true);
                Invoke("DamageLightOff", 0.75f);
                playerHealth--;
                damageSound.Play();
                HealthUpdate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            if (playerHealth < 3)
            {
                playerHealth++;
                healthSound.Play();
                healthLight.gameObject.SetActive(true);
                Invoke("HealthLightOff", 0.75f);
            }

            HealthUpdate();
            Destroy(other.gameObject);
            spawnManager.currentPowerupCount--;
        }
    }

    private void HealthUpdate()
    {
        if (playerHealth == 3)
        {
            health1.gameObject.SetActive(true);
            health2.gameObject.SetActive(true);
            health3.gameObject.SetActive(true);
        }
        else if (playerHealth == 2)
        {
            health1.gameObject.SetActive(true);
            health2.gameObject.SetActive(true);
            health3.gameObject.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            health1.gameObject.SetActive(true);
            health2.gameObject.SetActive(false);
            health3.gameObject.SetActive(false);
        }
        else if (playerHealth == 0)
        {
            health1.gameObject.SetActive(false);
            health2.gameObject.SetActive(false);
            health3.gameObject.SetActive(false);
            GameManager.instance.GameOver();
            speed = 0;
        }
    }

    private void DamageLightOff()
    {
        damageLight.gameObject.SetActive(false);
    }

    private void HealthLightOff()
    {
        healthLight.gameObject.SetActive(false);
    }

}
