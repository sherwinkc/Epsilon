using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsTower : MonoBehaviour
{
    AudioManager audioMan;

    [SerializeField] GameObject lightAndSFX, light2;

    private void Awake()
    {
        audioMan = FindObjectOfType<AudioManager>();
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
}
