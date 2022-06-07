using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverDock : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] SpriteRenderer greenLight, redLight;

    public Animator animToOpen;

    [SerializeField] float timeBeforeOpeningBridge = 2f;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        redLight.enabled = true;
        greenLight.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rover"))
        {
            collision.GetComponent<RoverBehaviour>().animator.SetTrigger("MoveArm");
            audioManager.smallMotorSFX.Play();

            Invoke("OpenBridge", timeBeforeOpeningBridge);
        }
    }

    private void OpenBridge()
    {
        redLight.enabled = false;
        greenLight.enabled = true;

        if (animToOpen != null) animToOpen.SetTrigger("Open"); //TODO Animator is being called 60fps

        audioManager.roverGreenLightSFX.Play();
    }
}
