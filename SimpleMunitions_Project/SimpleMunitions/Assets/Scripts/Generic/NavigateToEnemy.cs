using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateToEnemy : MonoBehaviour
{
    //public enum Mode { Random, Right, Top, Bottom }

    public float startDelay;
    private float _delayTimeOffset;
    private bool _targetActive;

    public GameObject Target;
    public Rigidbody2D rb;


    private void Start()
    {
        Target = PickRandomTarget("Enemy");
        rb = GetComponent<Rigidbody2D>();

        _delayTimeOffset = Time.time + startDelay; //Set initial timer offset
    }

    private void FixedUpdate()
    {
        if (!_targetActive)
        {
            timeCheck();
        }

        moveTowardsTarget();
    }


    public float TurnSpeed = 100;

    void moveTowardsTarget()
    {
        if (Target == null) //if target is destroyed then pick new target
        {
            Target = PickRandomTarget("Enemy");
        }

        if (_targetActive && Target != null) //Turn towards target while targeting active
        {
            float _angleToTarget = Vector2.SignedAngle(Target.transform.position - transform.position, Vector2.up);
            float _rotateAngle = Mathf.MoveTowardsAngle(rb.rotation, -_angleToTarget, TurnSpeed * Time.deltaTime);

            rb.SetRotation(_rotateAngle);
        }
    }

    //Check if timer reached then enable targeting
    void timeCheck()
    {
        if (Time.time >= _delayTimeOffset)
        {
            _targetActive = true;
        }
    }

    //Find a random target with tag
    private GameObject PickRandomTarget(string tag)
    {
        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag(tag);

        if (EnemyList.Length > 0)
        {
            return EnemyList[Random.Range(0, EnemyList.Length)];
        }
        else
        {
            return null;
        }
    }
}
