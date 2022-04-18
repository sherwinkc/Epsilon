using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFX_footsteps_sand;
    public AudioSource SFX_footsteps_metal;

    float pitchRangeLow = 0.8f, pitchRangeHigh = 1f;

    public AudioSource playerSFX_Jump;
    public AudioSource playerSFX_Land;

    //Lift
    public AudioSource liftButton, liftActiveLoop, liftStartStop;
    public AudioClip liftButtonClip;

    //Jetpack
    public AudioSource jetpackStart, jetpackLoop;

    //VO
    public AudioSource helper;

    //Rover
    public AudioSource roverEngine, roverEngine2;
    public AudioSource roverGreenLightSFX;

    //Helper
    public AudioSource helperCollectSFX;

    //Death
    public AudioSource deathCrunch;
    public AudioSource deathSquelch, deathYelp1, deathYelp2, deathYelp3, deathYelp4;

    //Collect
    public AudioSource collectSFX;
    public AudioSource powerUpSFX;

    //Button Puzzle
    public AudioSource puzzleError;
    public AudioSource puzzleCorrect;
    public AudioSource puzzleCorrect2;

    //Bridge / Doors
    public AudioSource bridgeOpenSFX;

    //Rock
    public AudioSource rockSFX;
    public AudioSource rockfallSFX;

    //Interact
    public AudioSource helperInteractSFX; //TODO

    public AudioSource frictionSFX; //TODO

    public AudioSource crashSFX;

    public AudioSource playerBreathingSFX;

    public AudioSource grabSFX;

    public AudioSource bootUpSFX;

    public AudioSource airDecompressSFX;
    public AudioSource openSafeSFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void Play_playerSFX_footsteps_sand()
    {
        if (SFX_footsteps_sand != null)
        {
            SFX_footsteps_sand.pitch = Random.Range(pitchRangeLow, pitchRangeHigh);
            SFX_footsteps_sand.Play();
        }
    }*/

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
