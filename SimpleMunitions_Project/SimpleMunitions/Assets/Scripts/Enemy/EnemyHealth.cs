using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoreMountains.Feedbacks;

public class EnemyHealth : MonoBehaviour
{
    //Health
    public float Health = 100;
    public int pointValue = 20;

    private SpawnManager spawnManager;
    private DeathAnimation deathAnimation;
    private GameManager gameManager;
    private NavigateToPlayer navToPlayer;

    #region FeedBacks
    private Transform Feedbacks;
    private MMF_Player Feedback_Damage;
    #endregion

    private void Start()
    {
        #region Get FeedBacks
        Feedbacks = gameObject.transform.Find("Enemy_Feedbacks");
        Feedback_Damage = Feedbacks.Find("Damage").GetComponent<MMF_Player>();
        Feedback_Damage.GetFeedbackOfType<MMF_SpriteRenderer>().BoundSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        #endregion

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        deathAnimation = GetComponent<DeathAnimation>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        navToPlayer = GetComponent<NavigateToPlayer>();
    }


    public bool isDead = false;

    //Detect Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            ifHitByBullet(collision);
            checkIfDead(collision);
        }
    }

    //Check if hit by bullet.
    void ifHitByBullet(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            gameManager.f_ShakeSmall.PlayFeedbacks();
            float damage = collision.GetComponent<Bullet>().Damage;
            Health = Health - damage;

            //play damage Animation
            damagedAnim();
        }
    }

    void checkIfDead(Collider2D collision)
    {
        if (Health <= 0) //Kill if health 0
        {
            Death(collision.gameObject);
        }
    }

    void Death(GameObject impactor)
    {
        navToPlayer.isDead = true;
        gameManager.UpdatePoints(pointValue);
        spawnManager.enemyKilled();
        isDead = true;

        if (deathAnimation != null)
        {
            deathAnimation.play(impactor);
        }
        else
        {
            //Remove Game Object
            Destroy(gameObject);
        }
    }

    

    void damagedAnim()
    {
        if (!Feedback_Damage.IsPlaying)
        {
            Feedback_Damage.PlayFeedbacks();
        }
    }
}
