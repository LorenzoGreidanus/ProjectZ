using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private Rigidbody myRB;
    private int currentRotation;

    public void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.mousePosition.x;

            if(x < Screen.width / 2 && x > 0)
            {
                RotateLeft(); 
            }

            if (x > Screen.width / 2 && x < Screen.width)
            {
                RotateRight();
            }
        }
    }

    public void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void RotateLeft()
    {
        transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
