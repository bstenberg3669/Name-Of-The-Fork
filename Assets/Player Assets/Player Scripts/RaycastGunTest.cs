using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGunTest : MonoBehaviour
{
    private Animator anim;
    
    private float range = 100000000f; //How far the gun can shoot (we don't have a range cap so don't change it)

    public Transform attackPoint; //Point that the gun shoots from
    public ParticleSystem muzzleFlash; //Guess Einstein
    
    public float fireRate = 0.1f; //How long until the weapon can fire again

    private float fireTimer; //Counts time for fireRate to work
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire(); //Execute the fuction if we press/hold the left mouse button
        }

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime; //Make the timer time
        }
    }

    

    private void Fire()
    {
        if (fireTimer < fireRate) return;

        RaycastHit hit;

        if (Physics.Raycast(attackPoint.position, attackPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.collider.name);
        }
        
        anim.CrossFadeInFixedTime("Fire", 0.01f); //Plays Shooting Animation
        muzzleFlash.Play();

        fireTimer = -0.1f; //Reset timer
    }
}
