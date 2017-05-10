using System;
using UnityEngine;
using System.Collections;

public abstract class Weapon
{
    protected ObjectPool objectPool;
    protected GameObject placeHolderPosition;

    protected Weapon()
    {
        objectPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
        placeHolderPosition = GameObject.Find("spawnBullets");
    }

    public abstract void Shoot();
   
}
