using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    [Header("Camera Object")]
    public Transform car;

    [Header("Camera Variables")]
    public float forwardSpeed;
    public float xOffset;
    public float zOffset;
    public bool smoothFollow;

    public void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (car)
        {
            Vector3 newPos = transform.position;
            newPos.x = car.position.x + xOffset;
            newPos.z = car.position.z + zOffset;

            if(!smoothFollow)
            {
                transform.position = newPos;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, newPos, forwardSpeed * Time.deltaTime);
            }
        }
        
    }
}
