using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingScript : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;
    public float timeBetweenShooting, reloadTime;
    public int magazineSize;

    private int _bulletsLeft;
    private bool _shooting, _readyToShoot, _reloading;
    
    //public TextMeshProUGUI ammunition;

    void Start()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _bulletsLeft > 0 && _readyToShoot && !_reloading)
        {
            Shoot();
        } else if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }
    }

    private void Shoot()
    {
        _readyToShoot = false;
        Vector3 attackPointPos = transform.position;

        // Find hit position
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    
        // Check if ray hits something
        Vector3 targetPoint = ray.GetPoint(40); // a point far away from player
       
        
        // Calculate direction from attackPoint to targetPoint
        Vector3 direction = targetPoint - attackPointPos;
        
        // Spawn bullet
        GameObject currentBullet = Instantiate(bullet, attackPointPos, Quaternion.identity);
        currentBullet.transform.forward = direction.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        _bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);
    }
    private void ResetShot()
    {
        _readyToShoot = true;
    }

    private void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        _bulletsLeft = magazineSize;
        _reloading = false;
    }
}