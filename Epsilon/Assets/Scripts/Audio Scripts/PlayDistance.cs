using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDistance : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerStateMachine player;
    public Transform sound;
    [SerializeField] bool useAudioSourceVolume = false;

    [SerializeField] bool playOnAwake = false;


    [Header("Volume")]
    public float distanceFromSound = 6;
    public float maxVolume;
    [Tooltip("Higher values result in faster volume fade")]
    public float decreaseVolumeRate = 5f;
    [Tooltip("Higher values result in faster volume fade")]
    public float increaseVolumeRate = 5f;

    [Header("Panning")]
    [SerializeField] float maxPanAmountLeft = -0.75f;
    [SerializeField] float maxPanAmountRight = 0.75f;
    [Tooltip("Higher values result in faster panning")]
    [SerializeField] float rightPanningRate = 0.75f;
    [Tooltip("Higher values result in faster panning")]
    [SerializeField] float leftPanningRate = 0.75f;
    [SerializeField] float centerPanRange = 5f;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerStateMachine>();
        sound = this.gameObject.transform;

        if(useAudioSourceVolume) maxVolume = audioSource.volume;

        //InvokeRepeating("CheckToPlay", 0f, 5f);

    }

    private void CheckToPlay() //called every 5 secs
    {
        if (Vector2.Distance(sound.transform.position, player.transform.position) < distanceFromSound * 2f)
        {
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else if (Vector2.Distance(sound.transform.position, player.transform.position) > distanceFromSound * 2f)
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }

        /*if (playOnAwake)
        {
            //check distance
            if (Vector2.Distance(sound.transform.position, player.transform.position) < distanceFromSound * 2f)
            {
                audioSource.Play();
            }
        }*/
    }

    private void Start()
    {
        //InvokeRepeating("CheckPanning", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSoundDistanceFromPlayer();
        CheckPanning();
    }

    private void CheckPanning()
    {
        //check if player is within a small distance
        if (Vector2.Distance(sound.transform.position, player.transform.position) < centerPanRange) // check if player is in a small range so the pan is centered
        {
            if (audioSource.panStereo < 0)
            {
                audioSource.panStereo += Time.deltaTime * rightPanningRate;

                if (audioSource.panStereo >= 0) audioSource.panStereo = 0f;
            }
            else if (audioSource.panStereo > 0)
            {
                audioSource.panStereo -= Time.deltaTime * leftPanningRate;

                if (audioSource.panStereo <= 0) audioSource.panStereo = 0f;
            }
        }
        else if (sound.transform.position.x < player.transform.position.x) // if the sound is to the left of the player we want it panned left
        {
            audioSource.panStereo -= Time.deltaTime * leftPanningRate; //TODO make parameter;

            if (audioSource.panStereo <= maxPanAmountLeft)
            {
                audioSource.panStereo = maxPanAmountLeft;
            }
        }
        else if (sound.transform.position.x > player.transform.position.x) // if the sound is to the right of the player we want it panned right
        {
            if (audioSource.panStereo < maxPanAmountRight)
            {
                audioSource.panStereo += Time.deltaTime * rightPanningRate; //TODO make parameter;
            }
            else
            {
                audioSource.panStereo = maxPanAmountRight;
            }
        }
    }

    private void CheckSoundDistanceFromPlayer()
    {
        if (Vector2.Distance(sound.transform.position, player.transform.position) < distanceFromSound)
        {
            audioSource.volume += Time.deltaTime * increaseVolumeRate;

            if (audioSource.volume >= maxVolume)
            {
                audioSource.volume = maxVolume;
            }
        }
        else
        {
            if (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime * decreaseVolumeRate;
            }

            if (audioSource.volume <= 0)
            {
                audioSource.volume = 0f;
            }
        }
    }
}
