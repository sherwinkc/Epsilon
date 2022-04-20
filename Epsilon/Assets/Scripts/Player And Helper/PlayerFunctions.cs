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

    // A U D I O
    public void PlayFootstepsSFX()
    {
        if (Mathf.Abs(playerStateMachine.Rigidbody.velocity.x) > 0.01f && playerStateMachine.IsGrounded) // Play if moving
        {
            if (audioManager != null && !playerStateMachine.inCinematic) //Prevents playing footsteps in cutscenes
            {
                audioManager.PlayFootstepsSFX(GetComponent<PlayerVFXManager>().material);
            }
        }
    }
    public void PlayPowerUpSFX()
    {
        audioManager.PlayPowerUpSFX();
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
    public void PlayJetpackLoop()
    {
        if (audioManager != null) audioManager.jetpackLoop.Play();
    }

    public void StopJetpackLoop()
    {
        if (audioManager != null) audioManager.jetpackLoop.Stop();
    }

    // C A M E R A
    public void ActivatePlayerCameraDuringClimb()
    {
        if(camManager != null) camManager.isCameraTargetPlayer = true;
    }

    /*public void PlayJetpackStart()
    {
        if (audioManager != null) audioManager.PlayJetpackStart();
    }*/

    // S T A T E S
    public void TriggerInCinematicState()
    {
        playerStateMachine.inCinematic = true;
    }

    public void StopInCinematicState()
    {
        playerStateMachine.inCinematic = false;
    }

    // V F X
    public void SpawnPlayerBeams()
    {
        Instantiate(powerBeams, new Vector2(playerStateMachine.transform.position.x + xOffset, playerStateMachine.transform.position.y + yOffset), 
            playerStateMachine.transform.rotation);
    }

    public void SFX_PlayGrab()
    {
        audioManager.grabSFX.Play();
    }
}