using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody myRB;
    Vector3 velocity;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 playerHeightCorrection = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(playerHeightCorrection);
    }

    public void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + velocity * Time.fixedDeltaTime);
    }
}
