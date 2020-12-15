using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GunController : MonoBehaviour
{
    public Transform weaponLocation;
    public Gun[] allGuns;
    Gun equipedGun;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (allGuns != null)
        {
            EquipWeapon(allGuns[0]);
            anim.SetInteger("WeaponType_int", 1);
        }
    }

    private void EquipWeapon(Gun gunToEquip)
    {
        if(equipedGun != null)
        {
            Destroy(equipedGun.gameObject);
        }
        equipedGun = Instantiate(gunToEquip, weaponLocation.position, weaponLocation.rotation) as Gun;
        equipedGun.transform.parent = weaponLocation;
    }

    public void ShopGun(int weaponIndex)
    {
        EquipWeapon(allGuns[weaponIndex]);
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

    public void Aim(Vector3 aimPoint)
    {
        if (equipedGun != null)
        {
            equipedGun.Aim(aimPoint);
        }
    }
}
