using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  [SerializeField] Camera FPcamera;
  [SerializeField] float range = 100f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
          Shoot();
        }
    }

    private void Shoot() {
      RaycastHit hit;
      Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hit, range);
      print(hit.transform.name);
    }
}
