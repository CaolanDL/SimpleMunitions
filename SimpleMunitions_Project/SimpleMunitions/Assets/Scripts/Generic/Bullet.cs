using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed, _lifeTime;
    public float Damage;
    public bool IsPropelled;

    //Initialise Variables  (used after instantiation)
    public void Init(float speed, float damage, float size, float  lifeTime, float rotation) 
    {
        _speed = speed;
        _lifeTime = lifeTime;
        Damage = damage;

        transform.localScale = Vector3.one * size;

        rb = GetComponent<Rigidbody2D>();
        rb.SetRotation(rb.rotation + rotation);

    }

    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitLifeTime();

        //If the bullet is not actively propelled then only add initial force
        if (!IsPropelled) { rb.AddRelativeForce(Vector2.up * _speed); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckLifeTime();

        //If bullet is actively propelled then add force every frame
        if (IsPropelled) { rb.AddRelativeForce(Vector2.up * _speed); }
    }

    private float lifeTimeEnd;

    //Define lifetime
    void InitLifeTime()
    {
        lifeTimeEnd = Time.time + _lifeTime;
    }

    //Check if lifetime is complete
    void CheckLifeTime()
    {
        //Only use lifetime if > 0
        if (_lifeTime > 0  && Time.time > lifeTimeEnd)
        {
            Destroy(gameObject);
        }
    }
}
