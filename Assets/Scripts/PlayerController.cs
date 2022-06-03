using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }

    private float speed = 10f;
    private float jumpForce = 70.0f;
    private int topBound = 13;
    private int bottomBound = 5;
    public int playerHealth { get; private set; }
    private Vector3 startPos = new Vector3(0.0f, 0.46f, 0.0f);
    private float collisionTime;

    [SerializeField] private GameObject spawnObject;
    private SpawnManager spawnManager;
    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    [SerializeField] private GameObject damageLight;
    [SerializeField] private GameObject healthLight;
    [SerializeField] private GameObject slowmotionLight;
    [SerializeField] private AudioSource healthSound;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private AudioSource damagePowerupSound;
    [SerializeField] private AudioSource jumpSound;

    private Rigidbody playerRb;

    public event EventHandler HealthIncreased;
    public event EventHandler HealthMaxed;
    public event EventHandler Poweruped;


    private void Awake()
    {
        InstanciatePlayerController();
    }

    private void Start()
    {
        StartGame();

        GameManager.instance.GameRestarted += OnGameRestarted;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ConstrainPlayerMovement();
        ConstrainPlayerOnCollision();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.GameRestarted -= OnGameRestarted;
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        CancelInvoke();
        StartGame();

        playerRb.position = startPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            WorkCollisionEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
            WorkTriggerHealth();

        if (other.gameObject.CompareTag("Powerup"))
            WorkTriggerPowerupDamage();

        if (other.gameObject.CompareTag("PowerupSlowmotion"))
            WorkTriggerPowerupSlowmotion();
    }

    private void WorkCollisionEnemy()
    {
        if (playerHealth > 0)
        {
            playerHealth--;
            HealthUpdate();

            damageSound.Play();

            damageLight.gameObject.SetActive(true);
            Invoke("DamageLightOff", 0.75f);

            collisionTime = GameManager.instance.gameDuration;

            ConstrainPlayerOnCollision();

            spawnManager.StartSpawningHealth();
        }
    }

    private void WorkTriggerHealth()
    {
        if (playerHealth < 3)
        {
            playerHealth++;
            HealthUpdate();

            healthSound.Play();

            healthLight.gameObject.SetActive(true);
            Invoke("HealthLightOff", 0.75f);
        }

        if (playerHealth < 3)
            HealthIncreased?.Invoke(this, EventArgs.Empty);
        else
            HealthMaxed?.Invoke(this, EventArgs.Empty);
    }

    private void WorkTriggerPowerupDamage()
    {
        if (playerHealth > 0)
        {
            playerHealth--;
            HealthUpdate();

            damagePowerupSound.Play();

            damageLight.gameObject.SetActive(true);
            Invoke("DamageLightOff", 0.75f);

            spawnManager.StartSpawningHealth();

            Poweruped?.Invoke(this, EventArgs.Empty);
        }
    }

    private void WorkTriggerPowerupSlowmotion()
    {
        slowmotionLight.gameObject.SetActive(true);
        Invoke("SlowmotionLightOff", 7.5f);

        ActivateSlowmotion();
        Invoke("DeactivateSlowmotion", 7.5f);

        Poweruped?.Invoke(this, EventArgs.Empty);
    }

    private void ActivateSlowmotion()
    {
        GameManager.instance.WorkSlowmotion(GameManager.TimeState.SlowMotion);
    }

    private void DeactivateSlowmotion()
    {
        GameManager.instance.WorkSlowmotion(GameManager.TimeState.Normal);
    }

    private void StartGame()
    {
        playerRb = GetComponent<Rigidbody>();
        spawnManager = spawnObject.GetComponent<SpawnManager>();

        playerHealth = 3;
        collisionTime = 0.0f;

        SlowmotionLightOff();
        HealthUpdate();
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

    private void MovePlayer()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameOver)
            return;

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);

        if (!Mathf.Approximately(verticalInput, 0.0f))
            playerRb.position += Vector3.forward * verticalInput * speed * Time.deltaTime;

        if (!Mathf.Approximately(horizontalInput, 0.0f))
            playerRb.position += Vector3.right * horizontalInput * speed * Time.deltaTime;

        if (playerRb.position.y < 0.5f)
        {
            if (jumpInput)
            {
                playerRb.position += Vector3.up * jumpForce * Time.deltaTime;
                jumpSound.Play();
            }
        }
    }

    private void ConstrainPlayerMovement()
    {
        if (playerRb.position.z > topBound)
            playerRb.position = new Vector3(playerRb.position.x, playerRb.position.y, topBound);

        if (playerRb.position.z < -bottomBound)
            playerRb.position = new Vector3(playerRb.position.x, playerRb.position.y, -bottomBound);
    }

    private void ConstrainPlayerOnCollision()
    {
        if (GameManager.instance.gameDuration - collisionTime < 0.6f)
            playerRb.position = new Vector3(playerRb.position.x, 0.46f, playerRb.position.z);
    }

    private void DamageLightOff()
    {
        damageLight.gameObject.SetActive(false);
    }

    private void HealthLightOff()
    {
        healthLight.gameObject.SetActive(false);
    }

    private void SlowmotionLightOff()
    {
        slowmotionLight.gameObject.SetActive(false);
    }

    private void InstanciatePlayerController()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

}
