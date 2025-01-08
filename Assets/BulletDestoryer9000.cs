using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestoryer9000 : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Terrain"))
        {
            Destroyer9000();
        }
        
    }

    void Destroyer9000()
    {
        
    }

    private void Start()
    {
        
    }

    void Update()
    {
        
    }
}
