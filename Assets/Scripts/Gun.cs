using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FireModes {Auto, Burst, Single};
    public FireModes fireMode;

    [Header("Gun Misc")]
    public Transform[] muzzle;
    public Projectile bullet;
    public Transform shellEject;
    public Transform shell;
    MuzzleEffects muzzleEffect;

    [Header("Gun stats")]
    public float rateOfFire = 100;
    public float muzzleVelocity = 35;
    public int burstAmount;
    private int burstShotsLeft;
    public int totalMagSize;

    [Header("Shop Stats")]
    public int cost;
    public int gunIndex;
    public int damage;

    private bool isReloading;
    private int bulletsLeft;
    private Animator anim;
    private float timeTillNextShot;
    private bool mouseReleased;


    public void Start()
    {
        muzzleEffect = GetComponent<MuzzleEffects>();
        anim = gameObject.GetComponentInParent<Animator>();
        burstShotsLeft = burstAmount;
    }

    public void FixedUpdate()
    {
        if(!isReloading && bulletsLeft == 0)
        {
            StartCoroutine("Reload");
        }
    }

    void Shoot()
    {
        if(!isReloading && bulletsLeft > 0 && Time.time > timeTillNextShot)
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
                if (bulletsLeft == 0)
                    break;

                bulletsLeft--;
                timeTillNextShot = Time.time + rateOfFire / 1000;
                Projectile newProjectile = Instantiate(bullet, muzzle[i].position, muzzle[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
                newProjectile.Damage(damage);
            }
            Instantiate(shell, shellEject.position, shellEject.rotation);
            muzzleEffect.Activate();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("Reload_b", true);
        yield return new WaitForSeconds(1.5f);

        anim.SetBool("Reload_b", false);
        isReloading = false;
        bulletsLeft = totalMagSize;
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
