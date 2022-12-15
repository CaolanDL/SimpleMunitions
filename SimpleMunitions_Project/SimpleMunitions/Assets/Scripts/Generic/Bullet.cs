using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    public float Damage;

    public void Init(float speed, float damage, float size) //Initialise Variables
    {
        _speed = speed;
        Damage = damage;

        transform.localScale.Set(size, size, size);
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion Rotation = Quaternion.AngleAxis(transform.rotation.z, Vector3.forward); //Get angle of motion

        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
