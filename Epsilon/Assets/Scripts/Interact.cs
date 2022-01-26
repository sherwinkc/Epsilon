using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] GameObject lift;
    public bool isLiftOn = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLiftOn = !isLiftOn;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLiftOn)
        {
            if (collision.gameObject.CompareTag("Lift"))
            {
                Debug.Log(collision.transform.name);
                collision.GetComponentInParent<LiftManager>().isMoving = true;
            }
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isLiftOn)
        {
            if (collision.gameObject.CompareTag("Lift"))
            {
                //Debug.Log(collision.transform.name);
                if (collision.GetComponentInParent<LiftManager>() != null) collision.GetComponentInParent<LiftManager>().isMoving = true;
                //if (!audioManager.liftButton.isPlaying) audioManager.Play_LiftButton();
                if (!audioManager.liftActiveLoop.isPlaying) audioManager.PlayLiftLoop();
                //if (!audioManager.liftStartStop.isPlaying) audioManager.liftStartStop.Play();
            }
        }
        else if (!isLiftOn)
        {
            if (collision.GetComponentInParent<LiftManager>() != null) collision.GetComponentInParent<LiftManager>().isMoving = false;
            if (audioManager.liftActiveLoop.isPlaying) audioManager.liftActiveLoop.Stop();
            audioManager.liftStartStop.Stop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
