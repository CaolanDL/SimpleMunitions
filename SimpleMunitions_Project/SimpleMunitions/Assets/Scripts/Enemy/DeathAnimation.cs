using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class DeathAnimation : MonoBehaviour
{
    //Private Perma vals
    private float Duration = 0.3f, 
        Distance = 2;

    //Enemy Accessable
    public bool IsDead = false;

    //Private Moving Variables
    private Vector2 velocityInput;

    //Object Vars
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void play(GameObject impactor)
    {
        velocityInput = impactor.GetComponent<Rigidbody2D>().velocity;
        velocityInput = velocityInput.normalized * Distance;

        killShadows();

        animate();
        IsDead = true;
    }


    void animate()
    {
        Vector3 localPos = transform.localPosition;

        Tween moveTween = rb.DOMove(velocityInput + new Vector2(localPos.x, localPos.y), Duration);
        moveTween.SetEase(Ease.OutSine);

        sr.DOFade(0, Duration);

        Sequence deathSequence = DOTween.Sequence();
        deathSequence.AppendInterval(Duration);
        deathSequence.AppendCallback(() => Destroy(gameObject));
    }

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
