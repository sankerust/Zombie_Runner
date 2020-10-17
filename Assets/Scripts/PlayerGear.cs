using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
  FlashLight flashLight;
  bool flashLightEnabled;
    // Start is called before the first frame update
    void Start()
    {
      flashLight = GetComponentInChildren<FlashLight>();
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown("f"))
    {
      flashLight.SwitchLight();
      flashLightEnabled = flashLight.enabled;
      flashLight.enabled = !flashLightEnabled;
      Debug.Log(flashLightEnabled);
    }
    }
}
