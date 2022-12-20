using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameManager GameManager;
    private DeathAnimation deathAnimation;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public float Health = 3;

    void UpdateHealth(int healthChange, GameObject impactor)
    {
        Health += healthChange;
        CheckIfDead(impactor);
    }

    void CheckIfDead(GameObject impactor)
    {
        if (Health <= 0)
        {
            Death(impactor);
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
