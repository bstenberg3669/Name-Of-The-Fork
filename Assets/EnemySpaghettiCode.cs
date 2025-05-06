using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpaghettiCode : MonoBehaviour
{
    public NavMeshAgent agent;

    public LineRenderer bullet;

    public Transform target;
    public Transform lagTarget;

    float distanceToTarget;
    public float eyeSight = 40f;
    public float stormtrooperSyndrome = 10f;
    public float ffFreeze = 4f;
    float lagTimer = 0;
    
    public float enemyHealth;
    public float enemyDamage = 5f;
    public Transform attackPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        bullet.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(SetTarget), 2f);
        bullet.SetPosition(0, attackPoint.transform.position);
        Death();
    }
    
    

public void SetTarget()
    {
        if (target != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position);
        }

        if (distanceToTarget < eyeSight)
        {
            agent.SetDestination(target.position);
        }
        
        if (distanceToTarget < stormtrooperSyndrome)
        {
            Attack();
        }
        
        if (distanceToTarget < ffFreeze)
        {
            agent.isStopped = true;
        }
        
        if (distanceToTarget > ffFreeze)
        {
            agent.isStopped = false;
        }
        
    }

    public void Attack()
    {
            lagTimer += Time.deltaTime;

            if (lagTimer > 1.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(attackPoint.position, attackPoint.transform.forward, out hit))
                {
                    if (hit.collider.name == "PlayerCapsule")
                    {
                        hit.collider.GetComponent<PlayerHealth>().playerHealth -= enemyDamage;
                    }
                }

                bullet.enabled = true;
                bullet.SetPosition(1, lagTarget.position);

                Invoke(nameof(TrailCancel), 0.05f);

                lagTimer = -0.05f;

            }
    }

    public void TrailCancel()
    {
        bullet.enabled = false;
    }

    public void Death()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);   
            
        }
    }
}
