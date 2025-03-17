using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    [SerializeField] private float health = 70f;

    public void ApplyDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}