using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFunctions : MonoBehaviour
{
    AudioManager audioManager;
    PlayerStateMachine playerStateMachine;
    CameraManager camManager;
    LevelManager levelMan;
    Collector collector;
    Animator anim;
    Interact interact;

    public GameObject powerBeams;
    [SerializeField] Transform beamOrigin;
    public float xOffset, yOffset;

    //[SerializeField] GameObject endGate;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        camManager = FindObjectOfType<CameraManager>();
        levelMan = FindObjectOfType<LevelManager>();
        collector = GetComponent<Collector>();
        anim = GetComponent<Animator>();
        interact = GetComponent<Interact>();
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

    public void ExitInCinematicState()
    {
        playerStateMachine.inCinematic = false;
    }

    public void StopInCinematicState()
    {
        //if statement prevent movement after all the orbs have been collected
        if (collector.orbs < 3) playerStateMachine.inCinematic = false;
    }

    // V F X
    public void SpawnPlayerBeams()
    {
        if (beamOrigin == null) return;
        
        Instantiate(powerBeams, new Vector2(beamOrigin.transform.position.x, beamOrigin.transform.position.y), 
            beamOrigin.transform.rotation);
    }

    public void SFX_PlayGrab()
    {
        audioManager.grabSFX.Play();
    }

    public void LoadFinalRoom()
    {
        //obsolete
    }

    public void PlaySoftGruntSFX()
    {
        audioManager.breathing.Stop();

        audioManager.softGrunt.pitch = Random.Range(0.75f, 1f);
        audioManager.softGrunt.Play();
    }

    public void PlayBeathingSFX()
    {
        if(!audioManager.breathing.isPlaying) audioManager.breathing.Play();
    }

    public void TurnOffInteractLerp()
    {
        interact.isLerping = false;
    }

    public void SwitchOnHelmetLight()
    {
        Collapse collapse = FindObjectOfType<Collapse>();
        collapse.helmetLight.SetActive(true);

        audioManager.helmetLightOnSFX.Play();
        FindObjectOfType<Letterbox>().MoveOut();
    }

    public void LetterboxMoveIn()
    {
        FindObjectOfType<Letterbox>().MoveIn();
    }

    public void LetterboxMoveOut()
    {
        FindObjectOfType<Letterbox>().MoveOut();
    }
}
