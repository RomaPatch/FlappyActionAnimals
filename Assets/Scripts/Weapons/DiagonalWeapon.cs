﻿using UnityEngine;
using System.Collections;

public class DiagonalWeapon : Weapon {

    private const float BULLET_DISTANCE = 0.5f;

    override public void Shoot()
    {
        GameObject bullet1 = objectPool.GetPooledObject();
        bullet1.transform.position = new Vector3(placeHolderPosition.transform.position.x, placeHolderPosition.transform.position.y + BULLET_DISTANCE, placeHolderPosition.transform.position.z);
        bullet1.transform.rotation = Quaternion.identity;
        bullet1.SetActive(true);

        GameObject bullet2 = objectPool.GetPooledObject();
        bullet2.transform.position = new Vector3(placeHolderPosition.transform.position.x, placeHolderPosition.transform.position.y - BULLET_DISTANCE, placeHolderPosition.transform.position.z);
        bullet2.transform.rotation = Quaternion.identity;
        bullet2.SetActive(true);

        GameObject bullet3 = objectPool.GetPooledObject();
        bullet3.GetComponent<BulletScript>().SetDiagonalDirection(1);
        bullet3.transform.position = new Vector3(placeHolderPosition.transform.position.x, placeHolderPosition.transform.position.y + (BULLET_DISTANCE*2), placeHolderPosition.transform.position.z);
        bullet3.transform.rotation = Quaternion.identity;
        bullet3.SetActive(true);

        GameObject bullet4 = objectPool.GetPooledObject();
        bullet4.GetComponent<BulletScript>().SetDiagonalDirection(-1);
        bullet4.transform.position = new Vector3(placeHolderPosition.transform.position.x, placeHolderPosition.transform.position.y - (BULLET_DISTANCE*2), placeHolderPosition.transform.position.z);
        bullet4.transform.rotation = Quaternion.identity;
        bullet4.SetActive(true);

    }
}
