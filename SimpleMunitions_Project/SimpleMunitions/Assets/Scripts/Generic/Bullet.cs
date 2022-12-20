using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed, _lifeTime;
    public float Damage;
    public bool IsPropelled;

    public void Init(float speed, float damage, float size, float  lifeTime, float rotation) //Initialise Variables
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

        if (!IsPropelled) { rb.AddRelativeForce(Vector2.up * _speed); }
            
    }

    // Update is called once per frame
    void Update()
    {
        CheckLifeTime();

        if (IsPropelled) { rb.AddRelativeForce(Vector2.up * _speed); }
    }

    private float lifeTimeEnd;

    void InitLifeTime()
    {
        lifeTimeEnd = Time.time + _lifeTime;
    }

    void CheckLifeTime()
    {
        if (_lifeTime > 0  && Time.time > lifeTimeEnd)
        {
            Destroy(gameObject);
        }
    }
}
