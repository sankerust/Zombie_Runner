using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  Transform target;
  [SerializeField] float chaseRange = 5f;
  [SerializeField] float turnSpeed = 5f;
  NavMeshAgent navMeshAgent;
  EnemyHealth health;
  float distanceToTarget = Mathf.Infinity;
  bool isProvoked = false;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void Update()
    {
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
      }
    }


    public void OnTakenDamage() {
      GetComponent<Animator>().SetTrigger("damageTaken");
      isProvoked = true;
    }

    private void EngageTarget() {
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
