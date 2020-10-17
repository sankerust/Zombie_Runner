using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] AudioClip painSound;
  [SerializeField] TextMeshProUGUI healthText;
  AudioSource audioSource;
  private void Start() {
    audioSource = GetComponent<AudioSource>();
  }
  private void Update() {
    DisplayHealth();
  }

  private void DisplayHealth() {
    healthText.text = "Condition: " + playerHitPoints.ToString() + "%";
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
