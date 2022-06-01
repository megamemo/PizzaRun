using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public static GroundMove instance { get; private set; }

    [HideInInspector] public float startSpeed { get; private set; }  = 5.0f;
    [HideInInspector] public int speedMultiplier { get; private set; } = 5;
    private int startPos = 50;

    private float offset;
    private Transform cashedTransform;


    private void Awake()
    {
        InstanciateGroundMove();
    }

    private void InstanciateGroundMove()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        cashedTransform = transform;
    }

    private void Update()
    {
        MoveDown(CalculateSpeed());
    }

    private void MoveDown(float calculatedSpeed)
    {
        offset += calculatedSpeed * Time.deltaTime;
        offset %= startPos;

        var pos = cashedTransform.position;
        pos.z = startPos - offset;
        cashedTransform.position = pos;
    }

    private float CalculateSpeed()
    {
        float speed = startSpeed + startSpeed * (GameManager.instance.level - 1) / speedMultiplier;
        return speed;
    }

}
