using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Footsteps")]
    public AudioSource SFX_footsteps_sand;
    public AudioSource SFX_footsteps_metal;

    [Header("Floats")]
    float pitchRangeLow = 0.8f, pitchRangeHigh = 1f;

    [Header("Jumps")]
    public AudioSource playerSFX_Jump;
    public AudioSource playerSFX_Land;

    [Header("Lift")]
    public AudioSource liftButton, liftActiveLoop, liftStartStop;
    public AudioClip liftButtonClip;

    [Header("Jetpack")]
    public AudioSource jetpackStart, jetpackLoop;
    public AudioSource bootUpSFX;

    [Header("VO")]
    public AudioSource helper;
    public AudioSource helperCollectSFX;

    [Header("Rover")]
    public AudioSource roverEngine, roverEngine2;
    public AudioSource roverGreenLightSFX;
    public AudioSource powerDownSFX;

    [Header("Death")]
    public AudioSource deathCrunch;
    public AudioSource deathSquelch, deathYelp1, deathYelp2, deathYelp3, deathYelp4;

    [Header("Collect")]
    public AudioSource collectSFX;
    public AudioSource powerUpSFX;

    [Header("Jumping Puzzle")]
    public AudioSource puzzleError;
    public AudioSource puzzleCorrect;
    public AudioSource puzzleCorrect2;

    [Header("Bridge")]
    public AudioSource bridgeOpenSFX;

    [Header("Rock")]
    public AudioSource rockSFX;
    public AudioSource rockfallSFX;

    [Header("Interact")]
    public AudioSource helperInteractSFX; //TODO
    public AudioSource frictionSFX; //TODO

    [Header("Collaps Sequence")]
    public AudioSource crashSFX;

    [Header("Player")]
    public AudioSource playerBreathingSFX;
    public AudioSource grabSFX;

    [Header("Safe")]
    public AudioSource airDecompressSFX;
    public AudioSource openSafeSFX;

    [Header("UI")]
    public AudioSource highlightUISFX;
    public AudioSource selectUISFX;
    public AudioSource interactSFX;

    [Header("Player")]
    public AudioSource breathing;
    public AudioSource softGrunt;

    [Header("Datapads")]
    public AudioSource datapadSFXOpen;
    public AudioSource datapadSFXClose;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void PlayFootstepsSFX(AudioMaterial material)
    {
        if (material == AudioMaterial.Sand || material == AudioMaterial.None)
        {
            SFX_footsteps_sand.pitch = Random.Range(pitchRangeLow, pitchRangeHigh);
            SFX_footsteps_sand.Play();
        }
        else if (material == AudioMaterial.Metal)
        {
            SFX_footsteps_metal.pitch = Random.Range(0.7f, 0.9f);
            SFX_footsteps_metal.Play();
        }
    }

    public void Play_playerSFX_Jump()
    {
        if (playerSFX_Jump != null)
        {
            playerSFX_Jump.pitch = Random.Range(pitchRangeLow, pitchRangeHigh);
            playerSFX_Jump.Play();
        }
    }

    public void Play_playerSFX_Land()
    {
        if (playerSFX_Land != null)
        {
            playerSFX_Land.pitch = Random.Range(pitchRangeLow, pitchRangeHigh);
            playerSFX_Land.Play();
        }
    }

    public void Play_LiftButton()
    {
        if (liftButton != null)
        {
            liftButton.PlayOneShot(liftButtonClip, 0.5f);
        }
    }

    public void PlayLiftLoop()
    {
        if (liftActiveLoop != null)
        {
            if(!liftActiveLoop.isPlaying) liftActiveLoop.Play();
        }
    }

    public void PlayHelperAudio()
    {
        if(!helper.isPlaying) helper.Play();
    }

    public void PlayRoverEngine()
    {
        if (roverEngine != null) roverEngine.Play();
    }

    public void PlayDeathCrunchSFX()
    {
        if (deathCrunch != null) deathCrunch.Play();
    }

    public void PlayCollectSFX()
    {
        collectSFX.Play();
    }

    public void PlayPowerUpSFX()
    {
        powerUpSFX.Play();
    }

    public void PlayDeathSounds()
    {
        deathCrunch.Play();

        int randomNumber = Random.Range(0, 4);

        //Debug.Log(randomNumber);

        if (randomNumber == 0) 
        { 
            deathYelp1.Play();        
        }
        else if (randomNumber == 1)
        {
            deathYelp2.Play();
        }
        else if (randomNumber == 2)
        {
            deathYelp3.Play();
        }
        else
        {
            deathYelp4.Play();
        }
    }
}
