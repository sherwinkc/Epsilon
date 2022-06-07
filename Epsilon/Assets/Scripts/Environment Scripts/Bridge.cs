using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bridge : MonoBehaviour
{
    AudioManager audioManager;

    [Header("Screenshake")]
    CinemachineImpulseSource impulse;
    [SerializeField] float force;

    [SerializeField] bool bridge1, bridge2;

    public bool isScreenshaking = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        impulse = FindObjectOfType<CinemachineImpulseSource>();
    }

    public void PlayBridgeOpenSFX()
    {
        if (audioManager != null)
        {
            audioManager.bridgeOpenSFX.Play();
            if(audioManager.avalancheSFX != null) audioManager.avalancheSFX.Play();
            //if (bridge2) audioManager.bridgeOpenSFX2.Play();
        }
    }

    private void Update()
    {
        if (isScreenshaking) impulse.GenerateImpulse(force);
    }

    public void Screenshake()
    {
        isScreenshaking = true;
    }

    public void EndScreenshake()
    {
        isScreenshaking = false;
        audioManager.lockInSFX.Play();
    }
}
