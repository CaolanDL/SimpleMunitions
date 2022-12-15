using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameManager GameManager;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    public float Health = 3;

    void UpdateHealth(int healthChange)
    {
        Health += healthChange;
    }

    void CheckIfDead()
    {
        if (Health <= 0)
        {

        }
    }
}
