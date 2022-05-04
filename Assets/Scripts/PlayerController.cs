using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private float topBound = 13.0f;
    private float bottomBound = 5.0f;
    private int playerHealth;
    
    private Rigidbody playerRb;
    public GameObject spawnObject;
    private SpawnManager spawnManager;
    public GameObject gameManagerObject;
    private GameManager gameManager;
    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Button pauseButton; 
    [SerializeField] private GameObject damageLight;
    [SerializeField] private GameObject HealthLight;


    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        spawnManager = spawnObject.GetComponent<SpawnManager>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
        playerHealth = 3;
        HealthUpdate();
        gameOverText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        MovePlayer();
        ConstrainPlayerMovement();
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (!Mathf.Approximately(verticalInput, 0))
        {
            playerRb.AddForce(Vector3.forward * verticalInput * speed, ForceMode.VelocityChange);
            //transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        }

        if (!Mathf.Approximately(horizontalInput, 0))
        {
            playerRb.AddForce(Vector3.right * horizontalInput * speed, ForceMode.VelocityChange);
            //transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        }
    }

    void ConstrainPlayerMovement()
    {
        if (transform.position.z > topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }
        if (transform.position.z < -bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -bottomBound);
        }
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
                HealthUpdate();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            if (playerHealth < 3)
            {
                playerHealth++;
                HealthLight.gameObject.SetActive(true);
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
            GameOver();
        }
    }

    private void DamageLightOff()
    {
        damageLight.gameObject.SetActive(false);
    }

    private void HealthLightOff()
    {
        HealthLight.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        gameManager.gameStopped = true;
        speed = 0;
        gameOverText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
}
