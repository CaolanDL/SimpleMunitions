using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class DeathAnimation : MonoBehaviour
{
    #region Variable Declaration
    //Private Perma vals
    private float Duration = 0.6f, 
        Distance = 2;

    //Enemy Accessable
    public bool IsDead = false;

    //Private Moving Variables
    private Vector2 velocityInput;

    #region References
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion
    #endregion

    //Play Death Animation
    public void play(GameObject impactor)
    {
        //Get the velocity of the impactor
        velocityInput = impactor.GetComponent<Rigidbody2D>().velocity;
        velocityInput = velocityInput.normalized * Distance;

        //Remove Shadows
        killShadows(); 

        //Run death animation
        animate(); 

        IsDead = true;
    }


    //Slide and fade death animation
    void animate()
    {
        Vector3 localPos = transform.localPosition;

        //Tween to final destination
        Tween moveTween = rb.DOMove(velocityInput + new Vector2(localPos.x, localPos.y), Duration);
        moveTween.SetEase(Ease.OutSine);

        //Tween Sprite Fadeout
        sr.DOFade(0, Duration);

        //Wait till duration complete then destroy
        Sequence deathSequence = DOTween.Sequence();
        deathSequence.AppendInterval(Duration);
        deathSequence.AppendCallback(() => Destroy(gameObject));
    }

    //Search and destroy all shadows
    void killShadows()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform shadow = gameObject.transform.FindRecursive("FakeShadow");
            if (shadow == null) { break; }
            Destroy(shadow.gameObject);
        }
    }
}
