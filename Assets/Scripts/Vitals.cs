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

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
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
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
