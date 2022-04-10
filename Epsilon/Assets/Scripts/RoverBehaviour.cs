using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverBehaviour : MonoBehaviour
{
    [SerializeField] int batteryCount = 0;
    [SerializeField] int batteriesRequired = 1;

    [SerializeField] BoxCollider2D refillStationEndPoint;

    public float moveSpeed = 1f;

    public bool canMove = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RoverStopPoint"))
        {
            canMove = false;
            FindObjectOfType<AudioManager>().roverEngine.Stop(); //TODO cache audio manager

            if (refillStationEndPoint != null) refillStationEndPoint.enabled = false;
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
        FindObjectOfType<AudioManager>().roverEngine.Play(); //TODO cache audio manager

        if (batteryCount >= batteriesRequired)
        {
            //TODO Require a battery count
        }
        //batteryCount++;
    }
}
