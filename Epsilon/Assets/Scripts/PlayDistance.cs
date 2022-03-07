using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDistance : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerStateMachine player;
    public Transform objectTransform;

    public float distanceFromSound = 6;
    [SerializeField] float maxVolume;
    [SerializeField] float fadeUpRate = 0.25f;
    [SerializeField] float fadeDownRate = 0.25f;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerStateMachine>();
    }

    void Update()
    {


        if (Vector2.Distance(objectTransform.transform.position, player.transform.position) < distanceFromSound)
        {
            audioSource.volume += Time.deltaTime * fadeUpRate;

            if(audioSource.volume >= maxVolume)
            {
                audioSource.volume = maxVolume;
            }               
        }
        else
        {
            if(audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime * fadeDownRate;
            }

            if (audioSource.volume <= 0)
            {
                audioSource.volume = 0f;
            }
        }
    }
}
