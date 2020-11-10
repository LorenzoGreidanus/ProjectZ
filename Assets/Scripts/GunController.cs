using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponLocation;
    public Gun startingGun;
    Gun equipedGun;

    private void Start()
    {
        if(startingGun != null)
        {
            EquipWeapon(startingGun);
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

    public void Shoot()
    {
        if(equipedGun != null)
        {
            equipedGun.Shoot();
        }
    }
}
