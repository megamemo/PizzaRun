using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private float startSpeed = 5.0f;
    private int speedMultiplier = 5;
    private int startPos = 50;

    private float offset;
    private Transform cashedTransform;


    private void Awake()
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
