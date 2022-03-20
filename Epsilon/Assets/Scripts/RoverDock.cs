using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverDock : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] SpriteRenderer greenLight, redLight;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        redLight.enabled = true;
        greenLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rover"))
        {
            redLight.enabled = false;
            greenLight.enabled = true;

            audioManager.roverGreenLightSFX.Play();
        }
    }
}
