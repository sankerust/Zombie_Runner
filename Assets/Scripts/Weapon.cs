﻿using System;
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
  [SerializeField] AudioClip dryFireSound;
  [SerializeField] GameObject hitEffect;
  [SerializeField] Ammo ammoSlot;
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
    if (ammoSlot.GetAmmoAmount() > 0) {
      PlayMuzzleFlash();
      ProcessRayCast();
      PlaySoundFx();
      ammoSlot.ReduceCurrentAmmo();
    } else {
      print("out of ammo");
      audioSource.PlayOneShot(dryFireSound);
    }
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
      CreateHitImpact(hit);
      EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
      if (target == null) { return; }
      target.TakeDamage(damage);
    }
    else
    {
      return;
    }
  }

  private void CreateHitImpact(RaycastHit hit) {
    GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    Destroy(impact, 0.1f);
  }
}
