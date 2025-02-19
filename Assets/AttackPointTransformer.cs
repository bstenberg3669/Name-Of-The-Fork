using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointTransformer : MonoBehaviour
{
    public Transform Gun;
    Vector3 vector3 = new Vector3();
    // Update is called once per frame
    void Update()
    {
        Vectorer();
        gameObject.transform.position = Gun.position;
        gameObject.transform.rotation = Gun.rotation;
    }

    void Vectorer()
    {
        
    }
}
