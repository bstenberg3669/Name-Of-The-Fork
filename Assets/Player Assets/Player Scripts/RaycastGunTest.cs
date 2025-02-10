using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGunTest : MonoBehaviour
{
    public float range = 100000000f;
    public float fireRate = 0.1f;

    public Transform attackPoint;

    private float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        
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

        fireTimer = 0.0f; //Reset timer
    }
}
