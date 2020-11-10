using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Enemy : Vitals
{
    NavMeshAgent pathfinding;
    Transform target;
    protected override void Start()
    {
        base.Start();
        pathfinding = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("UpdatePath");
    }
    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        
        while (target != null)
        {
            Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
            if (!death)
            {
                pathfinding.SetDestination(targetPos);
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
