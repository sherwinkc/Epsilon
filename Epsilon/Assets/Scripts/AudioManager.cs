using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource playerSFX_footsteps_sand;
    [Range(0f,1.5f)]
    public float pitchRangeLow = 0.75f;
    [Range(0f, 1.5f)]
    public float pitchRangeHigh = 1.2f;

    public AudioSource playerSFX_Jump;
    public AudioSource playerSFX_Land;

    //Lift
    public AudioSource liftButton, liftActiveLoop, liftStartStop;
    public AudioClip liftButtonClip;

    //Jetpack
    [SerializeField] AudioSource jetpackStart, jetpackLoop;

    //VO
    public AudioSource helper;

    //Rover
    public AudioSource roverEngine;

    //Death
    public AudioSource deathCrunch;

    //Collect
    public AudioSource collectSFX;
    public AudioSource powerUpSFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_playerSFX_footsteps_sand()
    {
        if (playerSFX_footsteps_sand != null)
        {
            playerSFX_footsteps_sand.pitch = Random.Range(pitchRangeLow, pitchRangeHigh);
            playerSFX_footsteps_sand.Play();
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

    public void PlayJetpackStart()
    {
        if (jetpackStart != null)
        {
            //jetpackStart.Play();
        }
    }

    public void PlayJetpackLoop()
    {
        if (jetpackLoop != null)
        {
            if (!jetpackLoop.isPlaying)
            {
                jetpackLoop.Play();
            }
        }
    }

    public void StopJetpackLoop()
    {
        if (jetpackLoop != null)
        {
            if (jetpackLoop.isPlaying)
            {
                jetpackLoop.Stop();
            }
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

}
