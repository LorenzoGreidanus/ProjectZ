using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile bullet;
    public float rateOfFire = 100;
    public float muzzleVelocity = 35;

    float timeTillNextShot;

    public void Shoot()
    {
        if(Time.time > timeTillNextShot)
        {
            timeTillNextShot = Time.time + rateOfFire / 1000;
            Projectile newProjectile = Instantiate(bullet, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
