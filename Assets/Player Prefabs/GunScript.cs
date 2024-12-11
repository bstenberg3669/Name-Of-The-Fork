using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class GunScript : MonoBehaviour
{
    //bullet
    public GameObject bullet;
    
    //bullet force
    public float shootForce, upwardForce;
    
    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowHoldFire;
    
    int bulletsLeft, bulletsShot;
    
    //Bools
    bool shooting, readyToShoot, reloading;
    
    //Reference
    public Camera fpsCamera;
    public Transform attackPoint;
    
    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;
    
    //For bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure mag is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        
        //Set ammo display, if it exists
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
        }
            
    }

    private void MyInput()
    {
        //Check if you can hold to fire
        if (allowHoldFire) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        
        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically if you try to shoot without any ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();
        
        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Sets bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        
        //Find the exact hit position using a raycast
        Ray ray = fpsCamera.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //Check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
        {
            targetPoint = ray.GetPoint(75); //Far away point for the bullet to go towards if it doese'nt run into an object
        }
        
        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        
        //Calculate spread
        //Debug.Log(Random.Range(1,2));
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);
        
        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add the spread to the last direction
        
        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //Instantiates bullet with 'currentBullet' object
        //Rotate bullet to proper direction
        currentBullet.transform.forward = directionWithSpread.normalized;
        
        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse); <-- Allows for shots that bounce or have vertical jazz
        
        //Instantiate muzzle flash
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
        
        bulletsLeft--;
        bulletsShot++;
        
        //Invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        
        //if you want mutliple bullets per shot (shotgun, burst rifle, etc.)
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        
    }
    
}
