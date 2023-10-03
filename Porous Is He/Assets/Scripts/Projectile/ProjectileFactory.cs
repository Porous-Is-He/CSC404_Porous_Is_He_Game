using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileFactory : MonoBehaviour
{
    GameObject NewProjectile(LiquidInfo liquid)
    {
        GameObject projectile;

        if (liquid.liquidType == "Water") {
            projectile = Instantiate(Resources.Load("Prefab/Projectiles/WaterProjectile", typeof(GameObject)) as GameObject);
        } else if (liquid.liquidType == "Oil")
        {
            projectile = Instantiate(Resources.Load("Prefab/Projectiles/WaterProjectile", typeof(GameObject)) as GameObject);
        } else {
            throw new Exception();
        }

        projectile.GetComponent<LiquidProjectileScript>().SetLiquid(liquid);
        return projectile;
    }

}
