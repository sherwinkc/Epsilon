using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDistance : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerStateMachine player;
    public Transform objectTransform;
    public float distanceFromSound = 6;
    public float maxVolume;
    public float decreaseVolumeRate = 5f;
    public float increaseVolumeRate = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerStateMachine>();
    }

    private void Start()
    {
        maxVolume = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(objectTransform.transform.position, player.transform.position) < distanceFromSound)
        {
            audioSource.volume += Time.deltaTime / increaseVolumeRate;

            if(audioSource.volume >= maxVolume)
            {
                audioSource.volume = maxVolume;
            }               
        }
        else
        {
            if(audioSource.volume > 0)
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
