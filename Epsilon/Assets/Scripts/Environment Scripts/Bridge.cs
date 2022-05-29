using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] bool bridge1, bridge2;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayBridgeOpenSFX()
    {
        if (audioManager != null)
        {
            if (bridge1) audioManager.bridgeOpenSFX.Play();
            if (bridge2) audioManager.bridgeOpenSFX2.Play();
        }

    }
}
