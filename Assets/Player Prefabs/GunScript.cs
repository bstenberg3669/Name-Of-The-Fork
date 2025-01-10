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
    
    //Recoil
    public Rigidbody playerRb;
    public float recoilForce;
    
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

    public State state;
    public enum State
    {
        pistol,
        shotgun
    }
    
    private void Awake()
    {
        //make sure mag is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        StateManager();
        
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
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Debug.Log("Reloading");
            Reload();
        }
        //Reload automatically if you try to shoot without any ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            bulletsLeft += (magazineSize-bulletsLeft);
        }
        
        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Sets bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void StateManager()
    {
        //Pistol
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = State.pistol;
            shootForce = 250f;
            upwardForce = 0f;
            timeBetweenShooting = 0.25f;
            spread = 0.05f;
            reloadTime = 0.5f;
            timeBetweenShots = 0f;
            magazineSize = 12;
            bulletsPerTap = 1;
            allowHoldFire = true;
            recoilForce = 0f;
            bulletsLeft += (magazineSize-bulletsLeft);
            
            
        }
        //Shotgun
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            state = State.shotgun;
            shootForce = 175;
            upwardForce = 0f;
            timeBetweenShooting = 0.6f;
            spread = 2f;
            reloadTime = 0.5f;
            timeBetweenShots = 0f;
            magazineSize = 16;
            bulletsPerTap = 8;
            allowHoldFire = false;
            recoilForce = 0f;
            bulletsLeft += (magazineSize-bulletsLeft);
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        
        //Find the exact hit position using a raycast
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.up * upwardForce, ForceMode.Impulse); <--- (for bouncing bullets)
        
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
        bulletsLeft += (magazineSize-bulletsLeft);
        reloading = false;
        
    }
    
}
