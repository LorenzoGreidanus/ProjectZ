using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]
public class PlayerControls : MonoBehaviour
{
    public Camera playerCam;
    public float moveSpeed;
    PlayerController controller;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        //playerCam = Camera.main;
    }

 
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDist;

        if(groundPlane.Raycast(ray, out rayDist))
        {
            Vector3 point = ray.GetPoint(rayDist);
            //Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }
    }
}
