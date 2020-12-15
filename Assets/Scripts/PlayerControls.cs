using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayController))]
[RequireComponent(typeof (GunController))]
public class PlayerControls : Vitals
{
    [Header("Variables")]
    public Camera playerCam;
    public float moveSpeed;
    public Transform crosshair;

    PlayController controller;
    GunController gunController;

    public float food;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayController>();
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

        void OnTriggerEnter(Collider other)
        {
            if(other.transform.tag == "Food")
            {
                food++;
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
