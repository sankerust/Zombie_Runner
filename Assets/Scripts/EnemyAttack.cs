using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  PlayerHealth target;
  [SerializeField] float damage = 40f;
  [SerializeField] AudioClip clawsHitSound;
  AudioSource audioSource;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    public void AttackHitEvent() {
      if (target == null) { return; }
      target.DamagePlayer(damage);
      audioSource.PlayOneShot(clawsHitSound);
      target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }
}
