using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
  [SerializeField] float AdsFov = 35f;
  bool AimDownSight = false;
  float initialFov;
    // Start is called before the first frame update
    void Start()
    {
      initialFov = Camera.main.fieldOfView;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
          if (AimDownSight == false) {
            AimDownSight = true;
          Camera.main.fieldOfView = AdsFov;
          } else {
            AimDownSight = false;
            Camera.main.fieldOfView = initialFov;
          }
      }
    }
}
