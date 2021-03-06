﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Camera camera;
    public LayerMask gunLayer;
    private GameObject player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, gunLayer, QueryTriggerInteraction.Collide))
            {
                //Gun equippen die gehit is.
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                }
                if(hit.transform.GetComponent<Gun>().gunCost > player.GetComponent<PlayerControls>().food)
                {
                    return;
                }
                player.GetComponent<GunController>().ShopGun(hit.transform.GetComponent<Gun>().gunIndex);
                print("Succes");
            }
        }
    }
}
