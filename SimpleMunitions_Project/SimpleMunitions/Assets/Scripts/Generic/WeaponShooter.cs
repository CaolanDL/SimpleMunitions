using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public GameObject BulletObject;

    //Start first timer
    private void Start(){ ResetTimer(); }

    private void Update()
    {
        //Fire Bullet when timer ends and player is shooting.
        if (Time.time >= _timer  && _shooting)
        {
            FireBullet();
            ResetTimer();
        }
    }

    public float BulletSpeed = 1f;
    public float Damage = 50;
    public float BulletSize = 1;

    //Create new bullet at location
    public void FireBullet()
    {
        GameObject instance = Instantiate(BulletObject, transform.position, transform.rotation);
        instance.GetComponent<Bullet>().Init(BulletSpeed, Damage, BulletSize);
    }


    //Recieve Player Fire Input
    public bool _shooting = false;

    public void SetFiring(bool fireActive)
    {
        _shooting = fireActive;
    }


    //Timer
    public float CoolDown = 0.2f;
    private float _timer = 0;

    private void ResetTimer()
    {
        _timer = Time.time + CoolDown;
    }
}
