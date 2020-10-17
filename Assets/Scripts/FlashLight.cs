using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashLight : MonoBehaviour
{
  [SerializeField] float lightDecay = 0.05f;
  [SerializeField] float angleDecay = 0.5f;
  [SerializeField] float minAngle = 40f;
  [SerializeField] TextMeshProUGUI chargeText;
  [SerializeField] AudioClip clickSound;
  AudioSource audioSource;

  Light myLight;

  private void Start() {
    myLight = GetComponent<Light>();
    audioSource = GetComponent<AudioSource>();
  }

  public void SwitchLight() {
    bool isEnabled = myLight.enabled;
    myLight.enabled = !isEnabled;
    audioSource.clip = clickSound;
    audioSource.Play();
  }

  private void Update() {
    DecreaseLightAngle();
    DecreaseLightIntensity();
    chargeText.text = "charge: " + (Mathf.FloorToInt(myLight.intensity) * 20).ToString() + "%";
  }

  public void RestoreLightAngle(float restoreAngle) {
    myLight.spotAngle = restoreAngle;
  }

  public void RestoreLightIntensity(float intensityAmount)
  {
    myLight.intensity += intensityAmount;
  }

  private void DecreaseLightAngle() {
    if (myLight.spotAngle <= minAngle) {
      return;
    } else {
      myLight.spotAngle -= angleDecay * Time.deltaTime;
    }
  }

  private void DecreaseLightIntensity(){
    myLight.intensity -= lightDecay * Time.deltaTime;
  }
}
