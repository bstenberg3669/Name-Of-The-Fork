using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public float damage;
    public state damageState;

    public enum state
    {
        pistol,
        shotgun
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        damageState = new state();
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();
    }

    public void StateHandler()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (damageState == state.pistol)
                damage = 10;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (damageState == state.shotgun)
                damage = 5;
        }
    }
}
