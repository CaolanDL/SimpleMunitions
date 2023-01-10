using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoreMountains.Feedbacks;

/// <summary>
/// Enemy health and damage
/// </summary>

public class EnemyHealth : MonoBehaviour
{
    #region Health Vars
    public float Health = 100;
    public int pointValue = 20;
    public bool isDead = false;
    #endregion

    #region References
    private SpawnManager spawnManager;
    private DeathAnimation deathAnimation;
    private GameManager gameManager;
    private NavigateToPlayer navToPlayer;
    #endregion

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

    //Detect Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            ifHitByBullet(collision);
            checkIfDead(collision);
        }
    }

    //Check if hit by bullet?
    void ifHitByBullet(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            gameManager.f_ShakeSmall.PlayFeedbacks();
            float damage = collision.GetComponent<Bullet>().Damage;
            Health = Health - damage; //Take Damage

            //play damage Animation
            damagedAnim();
        }
    }

    //Check if enemy has died?
    void checkIfDead(Collider2D collision)
    {
        if (Health <= 0) //Kill if health 0
        {
            Death(collision.gameObject);
        }
    }

    //Run Death sequence
    void Death(GameObject impactor)
    {
        navToPlayer.isDead = true; //Stop moving
        gameManager.UpdatePoints(pointValue); //Add points
        spawnManager.enemyKilled(); //Run enemy killed feedback

        isDead = true;

        //Play death animation if one is assigned
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

    //Run Damage flash feedback
    void damagedAnim() 
    {
        if (!Feedback_Damage.IsPlaying)
        {
            Feedback_Damage.PlayFeedbacks();
        }
    }
}
