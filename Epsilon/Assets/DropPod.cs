using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPod : MonoBehaviour
{
    AudioManager audioMan;
    [SerializeField] GameObject jetpack, signal;
    [SerializeField] ParticleSystem vfx1, vfx2;
    [SerializeField] BoxCollider2D boxColl;

    public Animator animator;

    public float delayTime = 3f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioMan = FindObjectOfType<AudioManager>();

        boxColl.enabled = false;
        animator.enabled = false;
        jetpack.SetActive(false);
    }

    public void OpenDropPod()
    {
        animator.enabled = true;
        boxColl.enabled = true;
        jetpack.SetActive(true);

        audioMan.machineSFX.Play();
        PlayAirSounds();
        signal.SetActive(false);

        vfx1.Play();
        vfx2.Play();
    }

    private void PlayAirSounds()
    {
        audioMan.airDecompressSFX.Play();
        audioMan.openSafeSFX.Play();
    }

    public void StopVFX()
    {
        vfx1.Stop();
        vfx2.Stop();
    }

    public void PlayEndSFX()
    {
        audioMan.machineEndSFX.Play();
    }
}
