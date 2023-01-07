using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class P_DeathAnimation : MonoBehaviour
{
    //Enemy Accessable
    public bool IsDead = false;

    //Object Vars
    [SerializeField] private GameManager gameManager;

    [SerializeField] private ParticleSystem deathParticles;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void play()
    {
        gameManager.f_ShakeLarge.PlayFeedbacks(); //Do Large ScreenShake
        gameManager.ui_Ctrl.DeadTextActive(true);

        Instantiate(deathParticles, transform.position, Quaternion.identity); //Spawn Death Particles at player position.

        gameObject.SetActive(false); //Disable Player
        IsDead = true;
    }
}
