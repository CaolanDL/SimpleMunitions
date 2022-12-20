using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class DeathAnimation : MonoBehaviour
{
    //Private Perma vals
    private float Duration = 1f, 
        Distance = 5;

    //Enemy Accessable
    public bool IsDead = false;

    //Private Moving Variables
    private Vector2 velocityInput;

    //Object Vars
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void killShadows()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject shadow = gameObject.transform.FindRecursive("FakeShadow").gameObject;
            if(shadow = null) { break; }
            Destroy(shadow);
        }
    }

    public void play(GameObject impactor)
    {
        Vector2 velocityInput = impactor.GetComponent<Rigidbody2D>().velocity;
        velocityInput = velocityInput.normalized * Distance;

        killShadows();

        animate();
        IsDead = true;
    }


    void animate()
    {
        rb.DOMove(velocityInput + rb.position, Duration);
        sr.DOColor(new Color(255, 255, 255), Duration);
        sr.DOFade(255, Duration);
        Sequence deathSequence = DOTween.Sequence();
        deathSequence.AppendInterval(Duration);
        deathSequence.AppendCallback(() => Destroy(gameObject));
    }
}
