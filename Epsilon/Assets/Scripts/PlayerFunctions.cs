using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
    AudioManager audioManager;
    PlayerStateMachine playerStateMachine;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    public void PlayFootsteps()
    {
        if(audioManager != null) audioManager.Play_playerSFX_footsteps_sand();
    }

    public void PlayJumpSFX()
    {
        if (audioManager != null) audioManager.Play_playerSFX_Jump();
    }

    public void PlayLandingSFX()
    {
        if (audioManager != null) audioManager.Play_playerSFX_Land();
    }

    public void PlayImpactVFX()
    {
        if(playerStateMachine != null) playerStateMachine._impactEffect.Play();
    }
}
