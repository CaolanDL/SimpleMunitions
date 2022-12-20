using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private SpawnManager spawnManager;
    private DeathAnimation deathAnimation;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        deathAnimation = GetComponent<DeathAnimation>();
    }

    //Health
    public float Health = 100;


    //Detect Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if hit by bullet.
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            float damage = collision.GetComponent<Bullet>().Damage;
            Health = Health - damage;
        }

        if (Health <= 0) //Kill if health 0
        {
            Death(collision.gameObject);
        }
    }

    void Death(GameObject impactor)
    {
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
}
