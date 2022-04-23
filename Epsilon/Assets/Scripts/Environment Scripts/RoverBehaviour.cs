using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverBehaviour : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] int batteryCount = 0;
    [SerializeField] int batteriesRequired = 1;

    [SerializeField] BoxCollider2D refillStationEndPoint;

    public float moveSpeed = 1f;
    public bool canMove = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        
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
            audioManager.roverEngine2.Stop();

            if (refillStationEndPoint != null) 
            { 
                refillStationEndPoint.enabled = false;
                //audioManager.powerDownSFX.Play(); // this is playing in both Docks
            }
        }

        if (collision.gameObject.CompareTag("BatteryDeposit"))
        {
            collision.gameObject.GetComponent<DepositManager>().isRoverDockedHere = true;
        }


        /*if (collision.gameObject.CompareTag("Battery"))
        {
            canMove = true;
            FindObjectOfType<AudioManager>().roverEngine.Play(); //TODO cache audio manager

            if (batteryCount >= batteriesRequired)
            {
                //TODO Require a battery count
            }
            //batteryCount++;
        }*/
    }

    public void MoveRover()
    {
        canMove = true;
        audioManager.roverEngine.Play();
        audioManager.roverEngine2.Play();

        if (batteryCount >= batteriesRequired)
        {
            //TODO Require a battery count
        }
        //batteryCount++;
    }
}
