using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class RaycastGunTest : MonoBehaviour
{
    private Animator anim;

    public GameObject lrPistolObj;
    public GameObject lrShotgun1Obj;
    public GameObject lrShotgun2Obj;
    public GameObject lrShotgun3Obj;
    public GameObject lrShotgun4Obj;
    public GameObject lrShotgun5Obj;
    public GameObject lrShotgun6Obj;
    private LineRenderer lrPistol;
    private LineRenderer lrShotgun1;
    private LineRenderer lrShotgun2;
    private LineRenderer lrShotgun3;
    private LineRenderer lrShotgun4;
    private LineRenderer lrShotgun5;
    private LineRenderer lrShotgun6;
    
    
    private float range = 100000000f; //How far the gun can shoot (we don't have a range cap so don't change it)
    private float spread = 0; //Controls the spread if the gun has any
    
    public Transform attackPoint; //Point that the gun shoots from
    public ParticleSystem muzzleFlash; //Guess, Einstein
    
    public float fireRate = 0.25f; //How long until the weapon can fire again
    public float altRate = 2.5f;

    private float fireTimer; //Counts time for fireRate to work
    private float altTimer; //Counts time for altRate to work

    public float heal = 0;
    
    public float playerDamage = 10f; //Damage the gun does per bullet
    public float killCounter = 0;

    public float inaccuracyDistance = 5f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        lrPistol = lrPistolObj.GetComponent<LineRenderer>();
        lrShotgun1 = lrShotgun1Obj.GetComponent<LineRenderer>();
        lrShotgun2 = lrShotgun2Obj.GetComponent<LineRenderer>();
        lrShotgun3 = lrShotgun3Obj.GetComponent<LineRenderer>();
        lrShotgun4 = lrShotgun4Obj.GetComponent<LineRenderer>();
        lrShotgun5 = lrShotgun5Obj.GetComponent<LineRenderer>();
        lrShotgun6 = lrShotgun6Obj.GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
        lrPistol.GetComponent<LineRenderer>().enabled = false;
        lrShotgun1.GetComponent<LineRenderer>().enabled = false;
        lrShotgun2.GetComponent<LineRenderer>().enabled = false;
        lrShotgun3.GetComponent<LineRenderer>().enabled = false;
        lrShotgun4.GetComponent<LineRenderer>().enabled = false;
        lrShotgun5.GetComponent<LineRenderer>().enabled = false;
        lrShotgun6.GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire(); //Execute the fuction if we press the left mouse button
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AltFire(); //Execute alternate fire for weapon on press of right mouse button
        }

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime; //Make the timer time
        }
        
        if (altTimer < altRate)
        {
            altTimer += Time.deltaTime; //Make the timer time
        }
        
        

    }

    


    private void Fire()
    {
        
        
        if (fireTimer > fireRate)
        {
            Damager();
            fireAnim();
            fireTimer = 0f; //Reset timer
        }
    }

    private void Damager()
    {
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, attackPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.name.Contains("Enemy"))
            {
                hit.collider.GetComponent<EnemySpaghettiCode>().enemyHealth -= playerDamage;
                if (hit.collider.GetComponent<EnemySpaghettiCode>().enemyHealth <= 0)
                {
                    killCounter++;
                    Debug.Log(killCounter);
                }
                

                heal += 5;

            }

                

        }
    }

    private void fireAnim()
    {
        RaycastHit hit;
        Physics.Raycast(attackPoint.position, attackPoint.transform.forward, out hit, range);
        
        anim.CrossFadeInFixedTime("Fire", 0.01f); //Plays Shooting Animation
        muzzleFlash.Play();
        
        lrPistol.GetComponent<LineRenderer>().enabled = true;
        lrPistol.GetComponent<LineRenderer>().SetPosition(0, muzzleFlash.transform.position);
        lrPistol.GetComponent<LineRenderer>().SetPosition(1, hit.point);
        
        
        
        Invoke(nameof(PostFire),0.045f);
    }
    
    private void AltFire()
    {
        if (altTimer < altRate) return;

        
        int i = 8;
        for (; i > 0; i--)
        {
            RaycastHit hit;
            if (Physics.Raycast(attackPoint.position, GetShootingDirection(), out hit, range))
            {
                if (hit.collider.name.Contains("Enemy"))
                {
                    hit.collider.GetComponent<EnemySpaghettiCode>().enemyHealth -= playerDamage;
                }
                Debug.Log(hit.collider.name);
            }
        }
        
        
        anim.CrossFadeInFixedTime("Fire", 0.01f); //Plays Shooting Animation
        muzzleFlash.Play();

        
        
        lrShotgun1.GetComponent<LineRenderer>().enabled = true;
        lrShotgun1.GetComponent<LineRenderer>().SetPosition(0, muzzleFlash.transform.position);
        lrShotgun1.GetComponent<LineRenderer>().SetPosition(1, SpreadCalculator());
        
        lrShotgun2.GetComponent<LineRenderer>().enabled = true;
        lrShotgun2.GetComponent<LineRenderer>().SetPosition(0, muzzleFlash.transform.position);
        lrShotgun2.GetComponent<LineRenderer>().SetPosition(1, SpreadCalculator());
        
        lrShotgun3.GetComponent<LineRenderer>().enabled = true;
        lrShotgun3.GetComponent<LineRenderer>().SetPosition(0, muzzleFlash.transform.position);
        lrShotgun3.GetComponent<LineRenderer>().SetPosition(1, SpreadCalculator());
        
        lrShotgun4.GetComponent<LineRenderer>().enabled = true;
        lrShotgun4.GetComponent<LineRenderer>().SetPosition(0, muzzleFlash.transform.position);
        lrShotgun4.GetComponent<LineRenderer>().SetPosition(1, SpreadCalculator());
        
        lrShotgun5.GetComponent<LineRenderer>().enabled = true;
        lrShotgun5.GetComponent<LineRenderer>().SetPosition(0, muzzleFlash.transform.position);
        lrShotgun5.GetComponent<LineRenderer>().SetPosition(1, SpreadCalculator());
        
        lrShotgun6.GetComponent<LineRenderer>().enabled = true;
        lrShotgun6.GetComponent<LineRenderer>().SetPosition(0, muzzleFlash.transform.position);
        lrShotgun6.GetComponent<LineRenderer>().SetPosition(1, SpreadCalculator());
        
        
        
        
        altTimer = -2.5f; //Reset timer

        Invoke(nameof(PostFire),0.045f);
    }

    
    
    private Vector3 SpreadCalculator()
    {
        //Spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);
        
        //Calculate Direction with Spread
        Vector3 direction = attackPoint.transform.forward + new Vector3(x, y, 0);

        Vector3 point = attackPoint.transform.position;
        
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.transform.position, direction, out hit, range))
        {
            Debug.Log(hit.collider.name);
            
            point = hit.point;
        }
        
        return point;
        
    }

    Vector3 GetShootingDirection()
    {
        Vector3 targetPos= attackPoint.transform.position + attackPoint.transform.forward * range;
        targetPos = new Vector3(
            targetPos.x + UnityEngine.Random.Range(-spread, spread),
            targetPos.x + UnityEngine.Random.Range(-spread, spread),
            targetPos.x + UnityEngine.Random.Range(-spread, spread)
            );
        
        Vector3 direction = targetPos - attackPoint.position;
        return direction.normalized;
    }
    
    private void PostFire()
    {
        lrPistol.GetComponent<LineRenderer>().enabled = false;
        lrShotgun1.GetComponent<LineRenderer>().enabled = false;
        lrShotgun2.GetComponent<LineRenderer>().enabled = false;
        lrShotgun3.GetComponent<LineRenderer>().enabled = false;
        lrShotgun4.GetComponent<LineRenderer>().enabled = false;
        lrShotgun5.GetComponent<LineRenderer>().enabled = false;
        lrShotgun6.GetComponent<LineRenderer>().enabled = false;
    }

    
}
