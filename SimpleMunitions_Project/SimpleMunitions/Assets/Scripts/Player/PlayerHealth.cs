using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerHealth : MonoBehaviour
{
    //Health
    private int Hearts = 8;
    public int pointValue = 20;

    [SerializeField] private GameObject GameManager;
    private GameManager gameManager;

    private P_DeathAnimation deathAnimation;
    

    #region FeedBacks
    [SerializeField] private MMF_Player f_Damage;
    [SerializeField] private MMF_Player f_Death;
    #endregion

    private void Start()
    {
        gameManager = GameManager.GetComponent<GameManager>();
        deathAnimation = GetComponent<P_DeathAnimation>();
    }


    public bool isDead = false;

    //Detect Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            ifHitByEnemy(collision);
            checkIfDead(collision);
        }
    }

    private bool invincible = false;
    private float damageDelay = 0.8f;

    //Check if hit by bullet.
    void ifHitByEnemy(Collider2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.CompareTag("EnemyAttack") | collision.gameObject.CompareTag("Enemy"))
            {
                gameManager.f_ShakeLarge.PlayFeedbacks();
                Hearts -= 1;
                gameManager.setHearts(Hearts);

                //play damage Animation
                damagedAnim();

                //Invicible Cooldown
                StartCoroutine(InvincibleCoolDown(damageDelay));
            }
        }
    }

    IEnumerator InvincibleCoolDown(float time)
    {
        invincible = true;

        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        invincible = false;
    }


    void checkIfDead(Collider2D collision)
    {
        if (Hearts <= 0) //Kill if health 0
        {
            isDead = true;
            deathAnimation.play();
        }
    }

    void damagedAnim()
    {
        if (!f_Damage.IsPlaying)
        {
            f_Damage.PlayFeedbacks();
        }
    }
}
