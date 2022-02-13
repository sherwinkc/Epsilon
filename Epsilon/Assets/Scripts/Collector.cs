using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Collector : MonoBehaviour
{
    AudioManager audioManager;    
    public PlayableDirector playableDirector;
    public int orbs = 0;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
        {
            playableDirector.Play();
            if(collision != null) Destroy(collision.gameObject);
            orbs++;

            audioManager.PlayCollectSFX();
        }
    }
}
