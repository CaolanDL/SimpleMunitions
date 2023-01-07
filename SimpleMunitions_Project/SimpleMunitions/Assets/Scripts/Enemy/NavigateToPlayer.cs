using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateToPlayer : MonoBehaviour
{
    public GameObject TargetPlayer;
    public Rigidbody2D rb;

    public float MoveSpeed = 15, TurnSpeed = 20;
    private float startDistanceDelay = 3f;

    private bool _targetActive;
    public bool isDead = false;


    private void Start()
    {
        TargetPlayer = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        setOldPos();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            motionSequence();
        }
    }


    void motionSequence()
    {
        if (!_targetActive)
        {
            distanceCheck();
        }

        rotateTowardsTarget();

        moveTowardsTarget();
    }


    void rotateTowardsTarget()
    {
        if (_targetActive) //Turn towards target while targeting active
        {
            float _angleToTarget = Vector2.SignedAngle(TargetPlayer.transform.position - transform.position, Vector2.up);
            float _rotateAngle = Mathf.MoveTowardsAngle(rb.rotation, -_angleToTarget, TurnSpeed * Time.deltaTime);

            rb.SetRotation(_rotateAngle);
        }
    }

    void moveTowardsTarget()
    {
        rb.AddRelativeForce(Vector2.up * MoveSpeed);
    }


    private Vector2 oldPosition;
    private float totalDistance = 0;
    //Check if start delay distance reached then enable targeting
    void distanceCheck()
    {
        Vector3 distanceVector = rb.position - oldPosition;
        float frameDistance = distanceVector.magnitude;
        totalDistance += frameDistance;

        setOldPos();

        if (totalDistance > startDistanceDelay)
        {
            _targetActive = true;
        }
    }

    void setOldPos()
    {
        oldPosition = rb.position;
    }
}
