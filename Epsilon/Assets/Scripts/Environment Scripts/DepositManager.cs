using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositManager : MonoBehaviour
{
    public RoverBehaviour rover;
    public GameObject redLines, greenLines, batterySprite;
    public BoxCollider2D boxCollider;

    public bool isRoverDockedHere;

    private void Awake()
    {
        redLines.SetActive(true);
        greenLines.SetActive(false);
        batterySprite.SetActive(false);

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            //Debug.Log("Deposit COllided with Battery");

            boxCollider.enabled = false;
            redLines.SetActive(false);
            greenLines.SetActive(true);
            batterySprite.SetActive(true);

            //Debug.Log("Deposit COllided with Battery");

            //start Rover
            //if (rover != null) rover.MoveRover();
        }

        //Debug.Log("Deposit Manager collided with " + collision.gameObject.name);
    }

    public void ChangeDepositState()
    {
        boxCollider.enabled = false;
        redLines.SetActive(false);
        greenLines.SetActive(true);
        batterySprite.SetActive(true);
        //FindObjectOfType<DepositFlash>().StopAllCoroutines(); //TODO Nasty horrible find another way of disabling the deposit flash script
        //FindObjectOfType<DepositFlash>().enabled = false; //TODO Nast horrible find another way of disabling the deposit flash script
    }
}
