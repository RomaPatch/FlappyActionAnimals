using System;
using UnityEngine;
using System.Collections;

public class FactoryWeapon
{
    public enum WEAPON
    {
        NORMAL,DOUBLE,DIAGONAL
        
    }
    public static Weapon GetWeaponByName(WEAPON weaponName)
    {
        switch (weaponName)
        {
            case WEAPON.NORMAL:
                return new NormalWeapon();
            case WEAPON.DOUBLE:
                return new DoubleWeapon();
            case WEAPON.DIAGONAL:
                return new DiagonalWeapon();
        }
        return new NormalWeapon();
    }

    public static Weapon GetWeaponByIndex(int weaponIndex)
    {
        switch (weaponIndex)
        {
            case 0:
                return new NormalWeapon();
            case 1:
                return new DoubleWeapon();
            case 2:
                return new DiagonalWeapon();
        }
        return new NormalWeapon();
    }
}
