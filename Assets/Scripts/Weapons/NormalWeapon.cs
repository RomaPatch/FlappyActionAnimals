using UnityEngine;
using System.Collections;

public class NormalWeapon : Weapon
{

    override public void Shoot()
    {
        GameObject bullet = objectPool.GetPooledObject();
        bullet.transform.position = placeHolderPosition.transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true);
    }
}


