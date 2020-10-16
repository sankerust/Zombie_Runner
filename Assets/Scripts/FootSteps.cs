using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FootSteps : MonoBehaviour
{
    RigidbodyFirstPersonController cc;
    void Start()
    {
        cc = GetComponent<RigidbodyFirstPersonController>();
        Debug.Log(cc);
    }

    // Update is called once per frame
    void Update()
    {
      if (cc.Grounded == true && cc.Velocity.magnitude > 1f && GetComponent<AudioSource>().isPlaying == false)
      {
        GetComponent<AudioSource>().Play();
      }
    }
}
