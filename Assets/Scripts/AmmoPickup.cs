using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
  [SerializeField] int ammoAmount = 5;
  [SerializeField] AmmoType ammoType;
  [SerializeField] AudioClip pickupSound;
  AudioSource audioSource;
private void OnTriggerEnter(Collider whoCollided)
  {
    
    if (whoCollided.tag == "Player") {
      audioSource = GetComponent<AudioSource>();
      FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
      AudioSource.PlayClipAtPoint(pickupSound, transform.position);
      Destroy(gameObject);
    }
  }
}
