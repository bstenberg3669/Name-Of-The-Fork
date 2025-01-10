using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointOrientation : MonoBehaviour
{
    public float sensX;
    public float sensY;
    
    public Transform orientation;

    float xRotation;
    float yRotation;
    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;
        
        yRotation += mouseX;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
