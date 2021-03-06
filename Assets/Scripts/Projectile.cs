﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    private float damage = 1;
    float speed = 10f;

    public Color trailColor;

    float lifeTime = 5;
    float bulletCorrection = 0.1f;

    public void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollsion = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if(initialCollsion.Length > 0)
        {
            OnHitObjects(initialCollsion[0], transform.position);
        }

        GetComponent<TrailRenderer>().startColor = trailColor;
    }

    public void Damage(int _damage)
    {
        damage = _damage;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + bulletCorrection, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObjects(hit.collider, hit.point);
        }
    }

    void OnHitObjects(Collider c, Vector3 hitPoint)
    {
        IDamagable damagableObject = c.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        Destroy(gameObject);
    }
}
