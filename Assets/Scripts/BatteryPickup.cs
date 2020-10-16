using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
  [SerializeField] float restoreAngle = 70f;
  [SerializeField] float intensityAmount = 5f;
  [SerializeField] AudioClip pickupSound;
  AudioSource audioSource;
  private void OnTriggerEnter(Collider whoCollided)
  {
    if (whoCollided.tag == "Player")
    {
      whoCollided.GetComponentInChildren<FlashLight>().RestoreLightAngle(restoreAngle);
      whoCollided.GetComponentInChildren<FlashLight>().RestoreLightIntensity(intensityAmount);
      AudioSource.PlayClipAtPoint(pickupSound, transform.position);
      Destroy(gameObject);
    }
  }
}
