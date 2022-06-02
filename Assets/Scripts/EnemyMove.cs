using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5.0f; //Prefabs unique values set in Editor
    private int speedMultiplier = 5;
    private int zDestroy = -10;
    [SerializeField] private int id;

    private Rigidbody enemyRb;
    [SerializeField] private Animator animatorObject;
    private Animator animator;

    private MoveState moveState;


    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        animator = animatorObject.GetComponent<Animator>();

        GameManager.instance.GameRestarted += OnGameRestarted;
        GameManager.instance.StartMenuStarted += OnStartMenuStarted;
    }

    private void RestartGame()
    {
        ResetAfterCollision();

        ObjectsPoolManager.instance.ReleaseEnemy(gameObject, id);
    }

    private void FixedUpdate()
    {
        MoveDown(CalculateSpeed());
        DeactivateOffScreen();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameRestarted -= OnGameRestarted;
            GameManager.instance.StartMenuStarted -= OnStartMenuStarted;
        }
    }

    private void OnGameRestarted(object sender, EventArgs e)
    {
        RestartGame();
    }

    private void OnStartMenuStarted(object sender, System.EventArgs e)
    {
        //Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveState = MoveState.Stopped;
            animator.GetComponent<Animator>().enabled = false;

            enemyRb.isKinematic = false;
            enemyRb.MoveRotation(CalculateRandomRotation().normalized);

            gameObject.tag = "Untagged";

            Invoke("DeactivateOnCollision", 1.0f);
        }
    }

    private void MoveDown(float calculatedSpeed)
    {
        if (moveState == MoveState.Stopped)
            return;

        else
           enemyRb.position += Vector3.back * calculatedSpeed * Time.deltaTime;
           
    }
    private void DeactivateOnCollision()
    {
        ResetAfterCollision();

        ObjectsPoolManager.instance.ReleaseEnemy(gameObject, id);
    }

        private void ResetAfterCollision()
    {
        moveState = MoveState.Moving;
        animator.GetComponent<Animator>().enabled = true;
        enemyRb.isKinematic = true;
        gameObject.tag = "Enemy";
    }

    private void DeactivateOffScreen()
    {
        if (enemyRb.position.z < zDestroy)
            ObjectsPoolManager.instance.ReleaseEnemy(gameObject, id);
    }

    private Quaternion CalculateRandomRotation()
    {
        float randomRotationY = UnityEngine.Random.Range(45.0f, 330.0f);
        Quaternion rotation = new Quaternion(90.0f, randomRotationY, 0.0f, 0.0f);
        return rotation;
    }

    private float CalculateSpeed()
    {
        float speed = startSpeed + startSpeed * (GameManager.instance.level - 1) / speedMultiplier;
        return speed;
    }

    private enum MoveState
    {
        Moving,
        Stopped
    }

}