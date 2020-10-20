using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
  [SerializeField] Camera FPcamera;
  [SerializeField] float range = 100f;
  [SerializeField] float damage = 30f;
  [SerializeField] ParticleSystem muzzleFlash;
  [SerializeField] AudioClip shotSound;
  [SerializeField] AudioClip dryFireSound;
  [SerializeField] AudioClip reloadSound;
  [SerializeField] AudioClip shellCasingSound;
  [SerializeField] AudioClip switchSound;
  [SerializeField] GameObject hitEffect;
  [SerializeField] Ammo ammoSlot;
  [SerializeField] AmmoType ammoType;
  [SerializeField] int magazineSize;
  [SerializeField] float timeBetweenShots = 0.5f;
  [SerializeField] float audioSourceDelay = 0.5f;
  [SerializeField] float reloadTime = 2f;
  [SerializeField] TextMeshProUGUI ammoText;
  AudioSource audioSource;
  bool canShoot = true;
  int shotsFired = 0;
  bool isReloading = false;
  int loadedAmmo;

  private void OnEnable() {
    //audioSource.clip = switchSound;
    audioSource.PlayOneShot(switchSound);
    StartCoroutine(SwitchShootDelay());
    isReloading = false;
  }


  
    void Start() {
      audioSource = GetComponent<AudioSource>();
    }

  IEnumerator SwitchShootDelay()
  {
    canShoot = false;
    yield return new WaitForSeconds(switchSound.length);
    canShoot = true;
  }
    void Update()
    {
      if (!isReloading) {
      if (Input.GetKeyDown("r") && ammoSlot.GetAmmoAmount(ammoType) > 0 && loadedAmmo != magazineSize)
      {
        StartCoroutine(StartReload());
        return;
      }
      if (Input.GetMouseButtonDown(0) && canShoot == true)
      {
        StartCoroutine(Shoot());
      }
      } else {
      }
      DisplayAmmo();

    }

    private void DisplayAmmo() {
      int currentAmmo = ammoSlot.GetAmmoAmount(ammoType);
      ammoText.text = loadedAmmo.ToString() + "/" + currentAmmo.ToString();
    }
    IEnumerator Shoot()
  {
    canShoot = false;
    if (loadedAmmo > 0) {
      PlayMuzzleFlash();
      ProcessRayCast();
      PlaySoundFx();
      loadedAmmo--;
    } else {
      audioSource.PlayOneShot(dryFireSound);
    }

    yield return new WaitForSeconds(timeBetweenShots);
    canShoot = true;
  }

  IEnumerator StartReload() {
    isReloading = true;
    canShoot = false;
    audioSource.clip = reloadSound;
    audioSource.Play();
    
    int needToLoad = magazineSize - loadedAmmo;
    int ammoLeft = ammoSlot.GetAmmoAmount(ammoType);
    

    reloadTime = audioSource.clip.length;
    yield return new WaitForSeconds(reloadTime);

    if (ammoLeft >= needToLoad)
    {
      loadedAmmo += needToLoad;
      ammoSlot.ReduceCurrentAmmo(ammoType, needToLoad);
    }
    else
    {
      loadedAmmo += ammoLeft;
      ammoSlot.ReduceCurrentAmmo(ammoType, ammoLeft);
    }

    canShoot = true;
    isReloading = false;
    audioSource.clip = shellCasingSound;
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
