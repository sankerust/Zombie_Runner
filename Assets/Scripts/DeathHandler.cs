using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
  [SerializeField] Canvas gameOverCanvas;

  private void Start() {
    gameOverCanvas.enabled = false;
  }

  public void HandleDeath() {
    Time.timeScale = 0;
    FindObjectOfType<WeaponSwitcher>().enabled = false;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    gameOverCanvas.enabled = true;
  }

}
