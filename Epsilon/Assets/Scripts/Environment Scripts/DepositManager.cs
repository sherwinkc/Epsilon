using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositManager : MonoBehaviour
{
    AudioManager audioMan;

    public RoverBehaviour rover;
    public GameObject batterySprite;
    public BoxCollider2D boxCollider;

    //Red & Green Lights
    public GameObject greenLight1, greenLight2, greenLight3, redLight1, redLight2, redLight3;

    public bool isRoverDockedHere;

    private void Awake()
    {
        batterySprite.SetActive(false);

        boxCollider = GetComponent<BoxCollider2D>();
        audioMan = FindObjectOfType<AudioManager>();

        DisableAllGreenLights();
        EnableAllRedLights();
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

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            Debug.Log("Deposit Hit Battery");
            boxCollider.enabled = false;
            batterySprite.SetActive(true);
            StartCoroutine(TransferPower());
        }
    }*/

    public void StartCo()
    {
        StartCoroutine(TransferPower());
    }

    private IEnumerator TransferPower()
    {
        boxCollider.enabled = false;
        batterySprite.SetActive(true);

        EnableAllGreenLights();
        DisableAllRedLights();

        audioMan.roverGreenLightSFX.pitch = 0.75f;
        audioMan.roverGreenLightSFX.Play();

        yield return new WaitForSeconds(1f);

        greenLight1.SetActive(false);
        redLight1.SetActive(true);

        //rover
        rover.greenLight3.SetActive(true);
        rover.redLight3.SetActive(false);

        audioMan.roverGreenLightSFX.pitch = 0.75f;
        audioMan.roverGreenLightSFX.Play();

        yield return new WaitForSeconds(1f);

        greenLight2.SetActive(false);
        redLight2.SetActive(true);

        //rover
        rover.greenLight2.SetActive(true);
        rover.redLight2.SetActive(false);

        audioMan.roverGreenLightSFX.pitch = 0.75f;
        audioMan.roverGreenLightSFX.Play();

        yield return new WaitForSeconds(1f);

        greenLight3.SetActive(false);
        redLight3.SetActive(true);

        //rover
        rover.greenLight1.SetActive(true);
        rover.redLight1.SetActive(false);

        audioMan.roverGreenLightSFX.pitch = 0.75f;
        audioMan.roverGreenLightSFX.Play();

        yield return new WaitForSeconds(1f);

        audioMan.roverGreenLightSFX.pitch = 1f;
        audioMan.roverGreenLightSFX.Play();
        rover.MoveRover();
    }
}
