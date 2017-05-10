using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalsData
{
    public enum animals
    {
        BIRD = 0,
        DUCK = 1
    }

    public static IDictionary animalsColliderSize = new Dictionary<animals, Vector2>()
    {
        {animals.BIRD,new Vector2(2,1) },
        {animals.DUCK,new Vector2(1.6f,0.84f) },

    };

    public static IDictionary animalsResourceName = new Dictionary<animals, String>()
    {
        {animals.BIRD,"playerBird" },
        {animals.DUCK,"playerDuck" },

    };

    public static animals GetAnimalByIndex(int index)
    {
        return (animals) index;
    }
}
