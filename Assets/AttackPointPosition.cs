using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointPosition : MonoBehaviour
{
    public float startX;
    public float startY;
    public float startZ;

    public float startRX;
    public float startRY;
    public float startRZ;
    
    public static float posX;
    public static float posY;
    public static float posZ;

    public static float rotX;
    public static float rotY;
    public static float rotZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
        
        transform.rotation = Quaternion.Euler(RaycastGunTest.returnAttackPointRX, RaycastGunTest.returnAttackPointRY, RaycastGunTest.returnAttackPointRZ);

        if (posX != RaycastGunTest.returnAttackPointX || posY != RaycastGunTest.returnAttackPointY ||
            posZ != RaycastGunTest.returnAttackPointZ)
        {
            transform.position = new Vector3(calc(posX, RaycastGunTest.returnAttackPointX), calc(posY, RaycastGunTest.returnAttackPointY), calc(posZ, RaycastGunTest.returnAttackPointZ));
        }
        
    }

    public float difference(float current, float next)
    {
        return Mathf.Abs(current - next);
    }

    public float calc(float current, float next)
    {
        float result = 0f;

        if (next > current)
        {
            result = current += difference(current, next);
        }

        if (next < current)
        {
            result = current -= difference(current, next);
        }
        
        float finalCalc = result;
        
        return finalCalc;
    }
}
