using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private float jumpForce = 70.0f;
    private int topBound = 13;
    private int bottomBound = 5;
    private int playerHealth;
    private Vector3 startPos = new Vector3(0, 0.46f, 0);

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
        spawnManager = spawnObject.GetComponent<SpawnManager>();
    }

    private void Start()
    {
        StartGame();

        GameManager.instance.GameRestarted += OnGameRestarted;
    }

    private void StartGame()
    {
        playerHealth = 3;
        HealthUpdate();
    }

    private void Update()
    {
        MovePlayer();
        ConstrainPlayerMovement();
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
        transform.position = startPos;
    }

    private void MovePlayer()
    {
        if (GameManager.instance.state != GameManager.GameState.GameOver)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            bool jumpInput = Input.GetKeyDown(KeyCode.Space);

            if (!Mathf.Approximately(verticalInput, 0))
                transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

            if (!Mathf.Approximately(horizontalInput, 0))
                transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

            if (transform.position.y < 0.5f)
            {
                if (jumpInput)
                {
                    transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
                    jumpSound.Play();
                }
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
            spawnManager.powerupCounter--;
            spawnManager.SetStartDelayPowerup();
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
