using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Turn towards player and move forward
/// </summary>

public class NavigateToPlayer : MonoBehaviour
{
    #region Variables
    public GameObject TargetPlayer;
    public Rigidbody2D rb;

    public float MoveSpeed = 15, TurnSpeed = 20;
    private float startDistanceDelay = 3f;

    private bool _targetActive;
    public bool isDead = false;
    #endregion


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

    // Sequence to excecute movements
    void motionSequence()
    {
        if (!_targetActive)
        {
            distanceCheck();
        }

        rotateTowardsTarget();

        moveTowardsTarget();
    }

    //Rotate towards target player
    void rotateTowardsTarget()
    {
        if (_targetActive) //Turn towards target while targeting active
        {
            float _angleToTarget = Vector2.SignedAngle(TargetPlayer.transform.position - transform.position, Vector2.up);
            float _rotateAngle = Mathf.MoveTowardsAngle(rb.rotation, -_angleToTarget, TurnSpeed * Time.deltaTime);

            rb.SetRotation(_rotateAngle);
        }
    }

    //Add force towards target player
    void moveTowardsTarget()
    {
        rb.AddRelativeForce(Vector2.up * MoveSpeed);
    }


    /// <summary>
    /// Wait until enemy has moved a distance in the world before targeting player
    /// This prevents enemies from getting stuck on walls when spawned outside the arena
    /// </summary>

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
