using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  Transform target;
  [SerializeField] float chaseRange = 5f;
  [SerializeField] float turnSpeed = 5f;
  [SerializeField] GameObject patrolLocation1;
  [SerializeField] GameObject patrolLocation2;
  [SerializeField] AudioClip idleSound;
  [SerializeField] AudioClip provokedSound;
  AudioSource audioSource;

  NavMeshAgent navMeshAgent;
  EnemyHealth health;
  float distanceToTarget = Mathf.Infinity;
  [SerializeField] bool isProvoked = false;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = idleSound;
        audioSource.PlayDelayed(Random.Range(0f, 3f));
    }

    void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void Update()
    {
    Patrol();
      if (health.IsDead()) {
        enabled = false;
        navMeshAgent.enabled = false;
      }
      distanceToTarget = Vector3.Distance(target.position, transform.position);

      //if (distanceToTarget > chaseRange * 2) {
        //isProvoked = false;
      //}

      if (isProvoked) {
        EngageTarget();
      } else if (distanceToTarget <= chaseRange) {
        isProvoked = true;
        navMeshAgent.speed = navMeshAgent.speed * 2;
      }
    }


    public void OnTakenDamage() {
      //GetComponent<Animator>().SetTrigger("damageTaken");
      isProvoked = true;
      navMeshAgent.speed = 3f;
    }
    private void Patrol() {
      if (!isProvoked && patrolLocation1 != null) {
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(patrolLocation1.transform.position);

        if (patrolLocation2 != null && Vector3.Distance(transform.position, patrolLocation1.transform.position) <= navMeshAgent.stoppingDistance * 2) 
        {
          navMeshAgent.SetDestination(patrolLocation2.transform.position);
          
          if (Vector3.Distance(transform.position, patrolLocation2.transform.position) <= navMeshAgent.stoppingDistance * 2) {
          navMeshAgent.SetDestination(patrolLocation1.transform.position);
          }
        }
      }

    }

    private void EngageTarget() {
      if (audioSource.clip != provokedSound) {
      audioSource.Stop();
      audioSource.clip = provokedSound;
      audioSource.Play();
      }
      
      FaceTarget();
      if (distanceToTarget > navMeshAgent.stoppingDistance) {
        ChaseTarget();
      } 
      if (distanceToTarget <= navMeshAgent.stoppingDistance) {
        AttackTarget();
      }
    }

    private void ChaseTarget() {
      GetComponent<Animator>().SetBool("attack", false);
      GetComponent<Animator>().SetTrigger("move");
      navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget() {
      GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget() {
      Vector3 direction = (target.position - transform.position).normalized;
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
