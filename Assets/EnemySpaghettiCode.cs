using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpaghettiCode : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();    
        }
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(SetTarget), 2f);
    }

    public void SetTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
