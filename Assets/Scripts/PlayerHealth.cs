using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] AudioClip painSound;
  AudioSource audioSource;
  private void Start() {
    audioSource = GetComponent<AudioSource>();
  }
  public float playerHitPoints = 100f;
    public void DamagePlayer(float damage) {
      if (playerHitPoints <= damage) {
        GetComponent<DeathHandler>().HandleDeath();
      } else {
        playerHitPoints -= damage;
        audioSource.PlayOneShot(painSound);
      }
    }
}
