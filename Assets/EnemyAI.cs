using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
   public NavMeshAgent agent;

   public Transform player;
   
   public LayerMask whatIsGround, whatIsPlayer;

   public Transform projectile;
   
   public Transform projectileSpawn;
   
   public float projectileSpeed;

   public float health;
   //Patrol
   public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;
   
   //Attack
   public float fireRate;
   bool alreadyFired;
   
   //States
   public float sightRange, attackRange;
   public bool playerInSight, playerInAttackRange;

   private void Awake()
   {
      player = GameObject.Find("PlayerWithGun").transform;
      agent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
      //Check for sight and attack range
      playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
      playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
      
      if (!playerInSight && !playerInAttackRange) Patroling();
      if (playerInSight && !playerInAttackRange) Chasing();
      if (playerInSight && playerInAttackRange) Attacking();
      
   }

   private void Patroling()
   {
      if (!walkPointSet) SearchWalkPoint();
      
      if (walkPointSet)
         agent.SetDestination(walkPoint);
      
      Vector3 distanceToWalkPoint = transform.position - walkPoint;
      
      //Arrived at Walkpoint
      if (distanceToWalkPoint.magnitude < 1f)
         walkPointSet = false;
   }

   private void SearchWalkPoint()
   {
      //Calculate random point in range
      float randomZ = Random.Range(-walkPointRange, walkPointRange);
      float randomX = Random.Range(-walkPointRange, walkPointRange);
      
      walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
      
      if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
         walkPointSet = true;
   }
   
   private void Chasing()
   {
      agent.SetDestination(player.position);
   }
   
   private void Attacking()
   {
      agent.SetDestination(player.position);
      
      transform.LookAt(player);

      if (!alreadyFired)
      {
         
         
         var proj = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
         proj.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
         
         
         
         alreadyFired = true;
         Invoke(nameof(ResetAttack), fireRate);
      }
   }

   private void ResetAttack()
   {
      alreadyFired = false;
   }

   public void TakeDamage(int damage)
   {
      health -= damage;

      if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
   }

   private void DestroyEnemy()
   {
      Destroy(gameObject);
   }

   private String bulletLife(Rigidbody rigidbody)
   {
      Destroy(rigidbody);
      return "bullet gone";
   }

   
}
