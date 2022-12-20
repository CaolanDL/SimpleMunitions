using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


public class WeaponShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletObject, MuzzleFlash;
    private AudioSource _fireSound;

    //Start first timer
    private void Start(){
        _fireSound = GetComponent<AudioSource>();

        ResetTimer();
    }

    private void Update()
    {
        //Fire Bullet when timer ends and player is shooting.
        if (Time.time >= _timer  && _shooting)
        {
            ResetTimer();
            FireBullet();
        }
    }

    [Header("Base Settings")]
    public float CoolDown = 0.2f;

    [Header("Bullet Settings")]
    public float Damage = 50;
    public float BulletSpeed = 1f;
    public float BulletSize = 1;
    public float BulletLifeTime = 0;

    [Header("Spread Settings")]
    public float Bullet_Count = 1;
    public float Bullet_SpreadAngle = 15;

    [Header("Offset")]
    public float ForwardOffset = 0;

    public bool InheritVelocity = false;

    //Create new bullet at location
    public void FireBullet()
    {
        for (int i = 0; i < Bullet_Count; i += 1)
        {
            GameObject instance = Instantiate(BulletObject, transform.position, transform.rotation);

            float rotationOffset = (Bullet_SpreadAngle / Bullet_Count) * i - (Bullet_SpreadAngle/2);  //Distribute projectiles by Defined Spread
            
            instance.transform.Translate(0, ForwardOffset, 0, Space.Self); //Forward Offset of projectile

            if (InheritVelocity)
            {
                Vector3 parentVelocity = transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity;
                instance.gameObject.GetComponent<Rigidbody2D>().AddForce(parentVelocity, ForceMode2D.Impulse);
            }

            if (instance.gameObject.TryGetComponent(out Bullet bullet))
            {
                bullet.Init(BulletSpeed, Damage, BulletSize, BulletLifeTime, rotationOffset);
            }
        }

        playMuzzleFlash();
        ShootAnimation();
        ShootSoundEffect();
    }

    //Spawn Muzzle Flash Icon if assigned
    void playMuzzleFlash()
    {
        if (MuzzleFlash != null)
        {
            GameObject flash = Instantiate(MuzzleFlash, transform.position, transform.rotation);
            flash.transform.SetParent(gameObject.transform);
            flash.transform.Translate(0, 0, 0, Space.Self);
        }
    }

    //Play shooting sound effect if assigned
    void ShootSoundEffect()
    {
        if (_fireSound != null)
        {
            _fireSound.Play();
        }
    }


    //Recieve Player Fire Input
    public bool _shooting = false;

    public void SetFiring(bool fireActive)
    {
        _shooting = fireActive;
    }


    //Timer
    private float _timer = 0;

    private void ResetTimer()
    {
        _timer = Time.time + CoolDown;
    }


    //Recoil Animation 
    [SerializeField]
    private float Recoil_Distance = 0.2f, Recoil_Speed = 0.1f;

    void ShootAnimation()
    {
        transform.DOPunchPosition(transform.localRotation * Vector2.down * Recoil_Distance, Recoil_Speed, 0, 0, false);
    }
}
