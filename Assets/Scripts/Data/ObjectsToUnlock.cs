using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ObjectsToUnlock
{
    public List<ObjectToUnlock> objectsToUnlock;
}

[Serializable]
public class ObjectToUnlock
{
    public String type;
    public int attribute;

}
