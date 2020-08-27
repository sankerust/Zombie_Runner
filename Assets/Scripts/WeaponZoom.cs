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

    void Start()
    {
      initialFov = Camera.main.fieldOfView;
      //FpsController = GetComponent<RigidbodyFirstPersonController>();
    }

    private void OnDisable() {
      ZoomOut();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
          if (AimDownSight == false) {
            ZoomIn();
          } else {
            ZoomOut();
                }
    }
    }

  private void ZoomOut()
  {
    AimDownSight = false;
    Camera.main.fieldOfView = initialFov;
    FpsController.mouseLook.XSensitivity = zoomOutSensitivity;
    FpsController.mouseLook.YSensitivity = zoomOutSensitivity;
  }

  private void ZoomIn()
  {
    AimDownSight = true;
    Camera.main.fieldOfView = AdsFov;
    FpsController.mouseLook.XSensitivity = zoomInSensitivity;
    FpsController.mouseLook.YSensitivity = zoomInSensitivity;
  }
}
