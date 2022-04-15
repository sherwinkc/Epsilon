using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAllAudioGameObjects : MonoBehaviour
{
    public AudioSource[] audioSources;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSources = FindObjectsOfType<AudioSource>();
     
    }
}
