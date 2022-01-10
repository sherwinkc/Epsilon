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
}
