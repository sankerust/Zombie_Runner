using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  //float initialFov = Camera.main.fieldOfView;
public void Start() {
    Camera.main.fieldOfView = 65f;
}
    // Start is called before the first frame update
public void ReloadGame() {
  SceneManager.LoadScene(0);
  Time.timeScale = 1;
  FindObjectOfType<WeaponSwitcher>().enabled = true;
  
}

public void QuitGame() {
  // if doesnt quit - might have to do with timescale set to 0
  Application.Quit();
}
}
