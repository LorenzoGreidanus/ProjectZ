using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody carRB;

    [Header("Car Stats")]
    public float forwardAcceleration;
    public float reverseAcceleration;
    public float maxSpeed;
    public float turnSpeed;
    public float gravityForce;
    private float multiplier = 1000;

    private float speedInput;
    private float turnInput;

    [Header("Car Physics")]
    public LayerMask ground;
    public float groundRayLenght;
    public Transform groundRayPoint;
    private bool isGrounded;


    void Start()
    {
        carRB.transform.parent = null;
    }

    void Update()
    {
        speedInput = 0f;

        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAcceleration * multiplier;
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAcceleration * multiplier;
        }

        turnInput = Input.GetAxis("Horizontal");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

        transform.position = carRB.transform.position;
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(speedInput) > 0)
        {
            carRB.AddForce(transform.forward * -1 * speedInput);
        }
    }
}
