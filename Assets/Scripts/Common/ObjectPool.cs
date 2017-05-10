using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public int pooledQuantity = 10;
    public bool willGrow = true;
    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        for (var i = 0; i < pooledQuantity; i++)
        {
            CreatePooledObject();
        }
    }

    private GameObject CreatePooledObject()
    {
        var pooledObject = (GameObject)Instantiate(objectToPool, transform);
       // pooledObject.transform.parent = transform;
        pooledObject.SetActive(false);
        pooledObjects.Add(pooledObject);

        return pooledObject;
    }

    public GameObject GetPooledObject()
    {
        GameObject pooledObject = null;
        for (var i = 0; i < pooledObjects.Count; i++)
        {
            pooledObject = pooledObjects[i];
            if (pooledObject.activeSelf){continue;}
            return pooledObject;
        }

        if (willGrow)
        {
            pooledObject =  CreatePooledObject();
            return pooledObject;
        }

        return pooledObject;

    }
}
