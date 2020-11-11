using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour, Damagable
{
    public float startHealth;
    protected float health;
    protected bool death;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !death)
        {
            Die();
        }
    }

    protected void Die()
    {
        death = true;
        if(OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
