using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
  [SerializeField] int amountRestored = 25;
  [SerializeField] AudioClip pickupSound;
  AudioSource audioSource;
  PlayerHealth playerHealth;
  private void Start() {
    playerHealth = FindObjectOfType<PlayerHealth>();
    audioSource = GetComponent<AudioSource>();
  }
  private void OnTriggerEnter(Collider whoCollided)
  {

    if (whoCollided.tag == "Player")
    {
      if (playerHealth.playerHitPoints < 100) {
        playerHealth.playerHitPoints += amountRestored;
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        if(playerHealth.playerHitPoints > 100) {
          playerHealth.playerHitPoints = 100;
        }
        Destroy(gameObject);
      }
    }
  }
}
