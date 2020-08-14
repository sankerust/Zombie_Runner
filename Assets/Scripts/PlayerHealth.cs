using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public float playerHitPoints = 100f;
    public void DamagePlayer(float damage) {
      if (playerHitPoints <= damage) {
        GetComponent<DeathHandler>().HandleDeath();
      } else {
        playerHitPoints -= damage;
        print(playerHitPoints);
      }
    }
}
