using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  [SerializeField] float hitPoints = 100f;
  [SerializeField] AudioClip deathSound;
  AudioSource audioSource;
  bool isDead = false;
  private void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  public bool IsDead() {
    return isDead;
  }

  public void TakeDamage(float damage) {
    GetComponent<EnemyAI>().OnTakenDamage();
    hitPoints -= damage;
    if (hitPoints <= 0 ) {
      Die();
      
    }
  }

  private void Die() {
    if (isDead) return;
    isDead = true;
    GetComponent<Animator>().SetTrigger("killed");
    audioSource.Stop();
    audioSource.PlayOneShot(deathSound);
    StartCoroutine(StopAudio());
    
    //Destroy(gameObject);
  }

  IEnumerator StopAudio() {
    yield return new WaitForSeconds(2f);
    audioSource.Stop();
  }
}
