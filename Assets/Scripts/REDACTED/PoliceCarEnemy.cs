using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarEnemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float rotatingSpeed;

    private GameObject target;
    private Rigidbody myRB;

    public void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        Vector3 targetLocation = transform.position - target.transform.position;
        targetLocation.Normalize();

        float value = Vector3.Cross(targetLocation, transform.forward).y;

        myRB.angularVelocity = rotatingSpeed * value * new Vector3(0, 1, 0);
        myRB.velocity = transform.forward * speed;
    }
}
