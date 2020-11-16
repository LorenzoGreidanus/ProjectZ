using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FireModes {Auto, Burst, Single};
    public FireModes fireMode;

    public Transform[] muzzle;
    public Projectile bullet;
    public float rateOfFire = 100;
    public float muzzleVelocity = 35;

    public int burstAmount;
    int burstShotsLeft;

    public Transform shellEject;
    public Transform shell;
    MuzzleEffects muzzleEffect;

    private Animator anim;

    float timeTillNextShot;

    bool mouseReleased;


    public void Start()
    {
        muzzleEffect = GetComponent<MuzzleEffects>();
        anim = gameObject.GetComponentInParent<Animator>();
        burstShotsLeft = burstAmount;
    }

    void Shoot()
    {
        if(Time.time > timeTillNextShot)
        {

            if(fireMode == FireModes.Burst)
            {
                if (burstShotsLeft == 0)
                    return;
                burstShotsLeft--;
                anim.SetBool("Shoot_b", true);
            }
            else if(fireMode == FireModes.Single)
            {
                if (!mouseReleased)
                    return;
                anim.SetBool("Shoot_b", true);
            }

            for (int i = 0; i < muzzle.Length; i++)
            {
                timeTillNextShot = Time.time + rateOfFire / 1000;
                Projectile newProjectile = Instantiate(bullet, muzzle[i].position, muzzle[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
            }
            Instantiate(shell, shellEject.position, shellEject.rotation);
            muzzleEffect.Activate();
        }
    }

    public void Aim(Vector3 aimPoint)
    {
        transform.LookAt(aimPoint);
    }

    public void OnTriggerHold()
    {
        Shoot();
        mouseReleased = false;
        anim.SetBool("Shoot_b", true);
    }

    public void OnTriggerRelease()
    {
        mouseReleased = true;
        burstShotsLeft = burstAmount;
        anim.SetBool("Shoot_b", false);
    }
}
