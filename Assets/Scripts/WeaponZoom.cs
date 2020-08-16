using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
  [SerializeField] float AdsFov = 35f;
  [SerializeField] float zoomOutSensitivity = 2f;
  [SerializeField] float zoomInSensitivity = 0.5f;
  [SerializeField] RigidbodyFirstPersonController FpsController;
  bool AimDownSight = false;
  float initialFov;
  
    // Start is called before the first frame update
    void Start()
    {
      initialFov = Camera.main.fieldOfView;
      //FpsController = GetComponent<RigidbodyFirstPersonController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
          if (AimDownSight == false) {
            AimDownSight = true;
            Camera.main.fieldOfView = AdsFov;
            FpsController.mouseLook.XSensitivity = zoomInSensitivity;
            FpsController.mouseLook.YSensitivity = zoomInSensitivity;
          } else {
            
            AimDownSight = false;
            Camera.main.fieldOfView = initialFov;
            FpsController.mouseLook.XSensitivity = zoomOutSensitivity;
            FpsController.mouseLook.YSensitivity = zoomOutSensitivity;
          }
      }
    }
}
