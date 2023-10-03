using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileFactory : MonoBehaviour
{


    public GameObject waterBullet;
    public GameObject oilBullet;

    public GameObject NewProjectile(LiquidInfo liquid, Vector3 position, Quaternion orientation)
    {

        GameObject prefab;
        if (liquid.liquidType == "Water")
            prefab = waterBullet;
        else if (liquid.liquidType == "Oil")
            prefab = oilBullet;
        else
            throw new Exception();


        GameObject projectile = Instantiate(prefab, position, orientation);

        projectile.GetComponent<LiquidProjectileScript>().SetLiquid(liquid);
        return projectile;
    }

    public GameObject NewProjectile(LiquidInfo liquid)
    {
        return NewProjectile(liquid, new Vector3(), new Quaternion());
    }
}
