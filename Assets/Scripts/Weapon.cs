using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  [SerializeField] Camera FPcamera;
  [SerializeField] float range = 100f;
  [SerializeField] float damage = 30f;
  [SerializeField] ParticleSystem muzzleFlash;
  [SerializeField] AudioClip shotSound;
  AudioSource audioSource;
  
    void Start() {
      audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
          Shoot();
        }
    }

    private void Shoot()
  {
    PlayMuzzleFlash();
    PlaySoundFx();
    ProcessRayCast();
  }

  private void PlaySoundFx() {
    audioSource.PlayOneShot(shotSound);
    audioSource.PlayDelayed(0.3f);
  }

  private void PlayMuzzleFlash()
  {
    muzzleFlash.Play();
  }

  private void ProcessRayCast()
  {
    RaycastHit hit;
    if (Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hit, range))
    {
      Debug.Log("hit this object: " + hit.transform.name);
      EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
      // todo add hit effects
      if (target == null) { return; }
      target.TakeDamage(damage);

    }
    else
    {
      return;
    }
  }
}
