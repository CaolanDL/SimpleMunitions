using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateToPlayer : MonoBehaviour
{
    public GameObject TargetPlayer;
    public Rigidbody2D rb;

    private void Start()
    {
        TargetPlayer = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public float MoveSpeed = 15;

    private void FixedUpdate()
    {
        rotateTowardsTarget();

        rb.AddRelativeForce(Vector2.up * MoveSpeed);
    }

    public float TurnSpeed = 20;

    void rotateTowardsTarget()
    {
        float _angleToTarget = Vector2.SignedAngle(TargetPlayer.transform.position - transform.position, Vector2.up);
        float _rotateAngle = Mathf.MoveTowardsAngle(rb.rotation, -_angleToTarget, TurnSpeed * Time.deltaTime);

        rb.SetRotation(_rotateAngle);
    }
}
