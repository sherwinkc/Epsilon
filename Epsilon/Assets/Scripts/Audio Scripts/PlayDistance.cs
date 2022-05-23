using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDistance : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerStateMachine player;
    public Transform sound;

    [SerializeField] bool playOnAwake = false;

    [Header("Volume")]
    public float distanceFromSound = 6;
    public float maxVolume;
    public float decreaseVolumeRate = 5f;
    public float increaseVolumeRate = 5f;

    [Header("Panning")]
    [SerializeField] float maxPanAmountLeft = -0.75f;
    [SerializeField] float maxPanAmountRight = 0.75f;
    [SerializeField] float rightPanningRate = 0.75f;
    [SerializeField] float leftPanningRate = 0.75f;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerStateMachine>();
        sound = this.gameObject.transform;

        maxVolume = audioSource.volume;

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
        if (sound.transform.position.x < player.transform.position.x) // if the sound is to the left of the player we want it panned left
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
            audioSource.volume += Time.deltaTime / increaseVolumeRate;

            if (audioSource.volume >= maxVolume)
            {
                audioSource.volume = maxVolume;
            }
        }
        else
        {
            if (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime / decreaseVolumeRate;
            }

            if (audioSource.volume <= 0)
            {
                audioSource.volume = 0f;
            }
        }
    }
}
