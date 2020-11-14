using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GunController : MonoBehaviour
{
    public Transform weaponLocation;
    public Gun startingGun;
    Gun equipedGun;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if(startingGun != null)
        {
            EquipWeapon(startingGun);
            anim.SetInteger("WeaponType_int", 1);
        }
    }
    public void EquipWeapon(Gun gunToEquip)
    {
        if(equipedGun != null)
        {
            Destroy(equipedGun.gameObject);
        }
        equipedGun = Instantiate(gunToEquip, weaponLocation.position, weaponLocation.rotation) as Gun;
        equipedGun.transform.parent = weaponLocation;
    }

    public void OnMouseHold()
    {
        if(equipedGun != null)
        {
            equipedGun.OnTriggerHold();
        }
    }

    public void OnMouseRelease()
    {
        if (equipedGun != null)
        {
            equipedGun.OnTriggerRelease();
        }
    }

    public float GunHeight()
    {
        return weaponLocation.position.y;
    }
}
