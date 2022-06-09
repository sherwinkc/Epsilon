using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverBehaviour : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] int batteryCount = 0;
    [SerializeField] int batteriesRequired = 1;

    [SerializeField] BoxCollider2D refillStationEndPoint;

    public Animator animator;

    public float moveSpeed = 1f;
    public bool canMove = false;

    //Red & Green Lights
    public GameObject greenLight1, greenLight2, greenLight3, redLight1, redLight2, redLight3;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();

        DisableAllGreenLights();
        EnableAllRedLights();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove) GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RoverStopPoint"))
        {
            canMove = false;
            audioManager.roverEngine.Stop();
            audioManager.roverStopSFX.Play();

            DisableAllGreenLights();
            EnableAllRedLights();
        }
    }

    public void MoveRover()
    {
        canMove = true;
        audioManager.roverEngine.Play();
        //audioManager.roverEngine2.Play();

        if (batteryCount >= batteriesRequired)
        {
            //TODO Require a battery count
        }
        //batteryCount++;
    }

    private void DisableAllGreenLights()
    {
        greenLight1.SetActive(false);
        greenLight2.SetActive(false);
        greenLight3.SetActive(false);
    }

    private void EnableAllGreenLights()
    {
        greenLight1.SetActive(true);
        greenLight2.SetActive(true);
        greenLight3.SetActive(true);
    }

    private void EnableAllRedLights()
    {
        redLight1.SetActive(true);
        redLight2.SetActive(true);
        redLight3.SetActive(true);
    }

    private void DisableAllRedLights()
    {
        redLight1.SetActive(false);
        redLight2.SetActive(false);
        redLight3.SetActive(false);
    }
}
