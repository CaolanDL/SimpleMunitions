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

    //Check if hit by bullet.
    void ifHitByEnemy(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack") | collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.f_ShakeLarge.PlayFeedbacks();
            gameManager.Hearts--;
            Hearts = gameManager.Hearts;
            gameManager.setHearts(Hearts);

            //play damage Animation
            damagedAnim();
        }
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
