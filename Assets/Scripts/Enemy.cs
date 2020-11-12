using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : Vitals
{
    public enum State {Idle, Chasing, Attacking};
    State currentState;

    public ParticleSystem deathEffect;

    NavMeshAgent pathfinding;
    Transform target;

    Vitals targetVitals;

    float attackDistance = 1.5f;
    float attackRate = 1;

    public float damage = 1;

    float nextAttackTime;
    float myCollision;
    float targetCollision;

    bool hasTarget;

    protected override void Start()
    {
        base.Start();
        pathfinding = GetComponent<NavMeshAgent>();


        if(GameObject.FindGameObjectWithTag("Player").transform != null)
        {
            currentState = State.Chasing;
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetVitals = target.GetComponent<Vitals>();

            targetVitals.OnDeath += OnTargetDeath;

            myCollision = GetComponent<CapsuleCollider>().radius;
            targetCollision = GetComponent<CapsuleCollider>().radius;

            StartCoroutine("UpdatePath");
        }

        StartCoroutine("UpdatePath");
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if(damage >= health)
        {
            Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    public void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    public void Update()
    {
        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float squareDistanceTarget = (target.position - transform.position).sqrMagnitude;

                if (squareDistanceTarget < Mathf.Pow(attackDistance + myCollision + targetCollision, 2))
                {
                    nextAttackTime = Time.time + attackRate;
                    StartCoroutine("Attack");
                }
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinding.enabled = false;

        Vector3 originalPos = transform.position;
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 attackPos = target.position - targetDirection * (myCollision);

        float attackSpeed = 3;
        float percent = 0;

        bool didDamage = false;

        while(percent <= 1)
        {
            if (percent >= 0.5f && !didDamage)
            {
                didDamage = true;
                targetVitals.TakeDamage(damage);
            } 
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent,2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPos, attackPos, interpolation);

            yield return null;
        }

        currentState = State.Chasing;
        pathfinding.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        
        while (hasTarget)
        {
            if(currentState == State.Chasing)
            {
                Vector3 targetDirection = (target.position - transform.position).normalized;
                Vector3 targetPos = target.position - targetDirection * (myCollision + targetCollision + attackDistance/2);

                if (!death)
                {
                    pathfinding.SetDestination(targetPos);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
