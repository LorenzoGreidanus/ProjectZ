﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]
[RequireComponent(typeof (GunController))]
public class PlayerControls : Vitals
{
    public Camera playerCam;
    public float moveSpeed;
    PlayerController controller;
    GunController gunController;

    public Transform crosshair;

    private Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = gameObject.GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.GunHeight());
        float rayDist;

        if (groundPlane.Raycast(ray, out rayDist))
        {
            Vector3 point = ray.GetPoint(rayDist);
            controller.LookAt(point);
            crosshair.position = point;

            if((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1)
            {
                gunController.Aim(point);
            }

        }

        if (Input.GetMouseButton(0))
        {
            gunController.OnMouseHold();
        }

        if (Input.GetMouseButtonUp(0))
        {
            gunController.OnMouseRelease();
        }
    }
}
