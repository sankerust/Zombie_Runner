using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
  [SerializeField] int ammoAmount = 5;
  [SerializeField] AmmoType ammoType;
private void OnTriggerEnter(Collider whoCollided)
  {
    if (whoCollided.tag == "Player") {
      FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
      Debug.Log("collided with a pickup");
      Destroy(gameObject);
    }
  }
}
