﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public float damage = 1;
    float speed = 10f;

    float lifeTime = 5;
    float bulletCorrection = 0.1f;

    public void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollsion = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if(initialCollsion.Length > 0)
        {
            OnHitObjects(initialCollsion[0]);
        }
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
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        Damagable damagableObject = hit.collider.GetComponent<Damagable>();
        if(damagableObject != null)
        {
            damagableObject.TakeHit(damage, hit);
        }
        GameObject.Destroy(this.gameObject);
    }

    void OnHitObjects(Collider c)
    {
        Damagable damagableObject = c.GetComponent<Damagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}