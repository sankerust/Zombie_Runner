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
  [SerializeField] AudioClip dryFireSound;
  [SerializeField] GameObject hitEffect;
  [SerializeField] Ammo ammoSlot;
  [SerializeField] AmmoType ammoType;
  [SerializeField] int magazineSize;
  [SerializeField] float timeBetweenShots = 0.5f;
  [SerializeField] float audioSourceDelay = 0.5f;
  AudioSource audioSource;
  bool canShoot = true;
  int shotsFired = 0;

  private void OnEnable() {
    canShoot = true;
  }
  
    void Start() {
      audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true) {
          StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
  {
    canShoot = false;
    if (ammoSlot.GetAmmoAmount(ammoType) > 0) {
      PlayMuzzleFlash();
      ProcessRayCast();
      PlaySoundFx();
      ammoSlot.ReduceCurrentAmmo(ammoType);
    } else {
      audioSource.PlayOneShot(dryFireSound);
    }

    yield return new WaitForSeconds(timeBetweenShots);
    canShoot = true;
  }

  private void PlaySoundFx() {
    audioSource.PlayOneShot(shotSound);
    audioSource.PlayDelayed(audioSourceDelay);
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
