using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerFunctions : MonoBehaviour
{
    AudioManager audioManager;
    PlayerStateMachine playerStateMachine;
    CameraManager camManager;

    public GameObject powerBeams;
    public float xOffset, yOffset;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        camManager = FindObjectOfType<CameraManager>();
    }

    public void PlayFootsteps()
    {
        if (Mathf.Abs(playerStateMachine.Rigidbody.velocity.x) > 0.01f)
        {
            if (audioManager != null && !playerStateMachine.inCinematic) //Prevents playing footsteps in cutscenes
            {
                audioManager.Play_playerSFX_footsteps_sand();
            }
        }
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

    public void ActivatePlayerCameraDuringClimb()
    {
        if(camManager != null) camManager.isCameraTargetPlayer = true;
    }

    /*public void PlayJetpackStart()
    {
        if (audioManager != null) audioManager.PlayJetpackStart();
    }*/

    public void PlayJetpackLoop()
    {
        if (audioManager != null) audioManager.jetpackLoop.Play();
    }

    public void StopJetpackLoop()
    {
        if (audioManager != null) audioManager.jetpackLoop.Stop();
    }

    public void TriggerInCinematicState()
    {
        playerStateMachine.inCinematic = true;
    }

    public void StopInCinematicState()
    {
        playerStateMachine.inCinematic = false;
    }

    public void PlayPowerUpSFX()
    {
        audioManager.PlayPowerUpSFX();
    }

    public void SpawnPlayerBeams()
    {
        Instantiate(powerBeams, new Vector2(playerStateMachine.transform.position.x + xOffset, playerStateMachine.transform.position.y + yOffset), 
        playerStateMachine.transform.rotation);
    }
}
