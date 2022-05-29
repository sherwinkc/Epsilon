using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeManager : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] ParticleSystem vfx1, vfx2;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void SFX_PlayOpenSafe()
    {
        audioManager.airDecompressSFX.Play();
        audioManager.openSafeSFX.Play();

        vfx1.Play();
        vfx2.Play();
    }

    public void StopVFX()
    {
        vfx1.Stop();
        vfx2.Stop();
    }
}
