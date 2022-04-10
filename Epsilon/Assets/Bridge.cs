using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayBridgeOpenSFX()
    {
        if (audioManager != null) audioManager.bridgeOpenSFX.Play();
    }
}
