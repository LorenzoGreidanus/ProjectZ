using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile bullet;
    public float rateOfFire = 100;
    public float muzzleVelocity = 35;

    public Transform shellEject;
    public Transform shell;
    MuzzleEffects muzzleEffect;

    private Animator anim;

    float timeTillNextShot;

    public void Start()
    {
        muzzleEffect = GetComponent<MuzzleEffects>();
        anim = gameObject.GetComponentInParent<Animator>();
    }

    public void Shoot()
    {
        if(Time.time > timeTillNextShot)
        {
            timeTillNextShot = Time.time + rateOfFire / 1000;
            Projectile newProjectile = Instantiate(bullet, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);

            anim.SetBool("Shoot_b", true);

            Instantiate(shell, shellEject.position, shellEject.rotation);
            muzzleEffect.Activate();
        }
    }

    public void LateUpdate()
    {
        anim.SetBool("Shoot_b", false);
    }
}
