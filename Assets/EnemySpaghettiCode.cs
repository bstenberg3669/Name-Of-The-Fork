using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpaghettiCode : MonoBehaviour
{
    public NavMeshAgent agent;

    public LineRenderer bullet;
    
    public Transform target;
    public Transform lagTarget;
    
    float distanceToTarget;
    float lagTimer = 0;
    public float health;
    public float damage;
    
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
        bullet.SetPosition(0, gameObject.transform.position);
    }

    public void SetTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            distanceToTarget = Vector3.Distance(transform.position, target.position);
        }

        if (distanceToTarget < 20f)
        {
            Attack();
        }
    }

    public void Attack()
    {
            lagTarget.position = target.position;
            lagTimer += Time.deltaTime;

            if (lagTimer > 0.75f)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(lagTarget.position, lagTarget.transform.forward, out hit, 45f))
                {
                    Debug.Log(hit.collider.name);
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
}
