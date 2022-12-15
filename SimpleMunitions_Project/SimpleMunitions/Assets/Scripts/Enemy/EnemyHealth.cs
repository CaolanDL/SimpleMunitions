using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if hit by bullet.
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            float damage = collision.GetComponent<Bullet>().Damage;
            Health = Health - damage;
            Debug.Log(damage);
        }

        if (Health <= 0) //Kill if health 0
        {
            Destroy(gameObject);
        }
    }
}
