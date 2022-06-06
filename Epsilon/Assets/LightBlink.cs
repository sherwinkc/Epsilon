using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBlink : MonoBehaviour
{
    [SerializeField] Light2D light2D;
    [SerializeField] float flashTime;

    [SerializeField] bool isOn = false;

    [SerializeField] AudioSource beepSFX;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }


    void Start()
    {
        InvokeRepeating("LightSwitch", 0f, flashTime);
    }

    private void LightSwitch()
    {
        if (isOn)
        {
            light2D.enabled = false;
            isOn = false;
        }
        else
        {
            light2D.enabled = true;
            isOn = true;
            if (beepSFX != null) beepSFX.Play();
        }
    }

}
