using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour, Damagable
{
    public float startHealth;
    protected float health;
    protected bool death;

    protected virtual void Start()
    {
        health = startHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if(health <= 0 && !death)
        {
            Die();
        }
    }

    protected void Die()
    {
        death = true;
        GameObject.Destroy(gameObject);
    }
}
