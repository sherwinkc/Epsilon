using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CommsTower : MonoBehaviour
{
    AudioManager audioMan;

    [Header("Screenshake")]
    CinemachineImpulseSource impulse;
    [SerializeField] float force;

    [SerializeField] GameObject lightAndSFX, light2;

    public bool isScreenshaking = false;

    private void Awake()
    {
        audioMan = FindObjectOfType<AudioManager>();
        impulse = FindObjectOfType<CinemachineImpulseSource>();
    }

    public void TurnOnLight()
    {
        if (lightAndSFX != null || light2 != null)
        {
            lightAndSFX.SetActive(true);
            light2.SetActive(true);
        }
    }

    public void PlayCommsTowerEndSFX()
    {
        if (audioMan != null) audioMan.commsTowerSFXEnd.Play();
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
    }
}
