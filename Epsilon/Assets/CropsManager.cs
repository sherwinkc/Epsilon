using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsManager : MonoBehaviour
{
    [SerializeField] ParticleSystem sprinklerVFX;
    [SerializeField] AudioSource sprinklerSFX;

    private void Awake()
    {

    }

    public void ActivateSprinklers()
    {
        sprinklerSFX.Play();
        sprinklerVFX.Play();
    }
}
