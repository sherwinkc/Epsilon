using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    AudioManager audioManager;
    public Transform helperTransform;
    public HelperMovement helper;

    [SerializeField] bool isMovingWithHelper = false;

    //public RoverBehaviour roverBehaviour;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        helper = FindObjectOfType<HelperMovement>();

        if (helper != null) helperTransform = helper.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovingWithHelper && helperTransform != null) transform.position = helperTransform.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BatteryDeposit"))
        {
            helper.isDepositingToRover = false;
            helper.isCarryingBattery = false;
            audioManager.helperCollectSFX.Play();
            audioManager.roverGreenLightSFX.Play();

            //Turn on rover and other things
            DepositManager depManager = collision.gameObject.GetComponent<DepositManager>();

            depManager.ChangeDepositState();
            depManager.rover.MoveRover();
            depManager.isRoverDockedHere = false;

            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Helper"))
        {
            collision.transform.position = helperTransform.transform.position;
            isMovingWithHelper = true;
            helper.isCarryingBattery = true;
            helper.isPickingUpItem = false;
            helper.isDepositingToRover = false;

            audioManager.helperCollectSFX.Play();
        }
        else if (collision.gameObject.CompareTag("BatteryRecharger"))
        {
            Debug.Log("Battery Collided with :" + collision.gameObject.name);
            isMovingWithHelper = false;
            helper.isPickingUpItem = false;
            helper.isCarryingBattery = false;
            helper.isDepositingToBatteryRecharger = false;

            BatteryRecharger batRecharger = FindObjectOfType<BatteryRecharger>();
            batRecharger.batteriesDocked++;
            batRecharger.ActivateBatterySprites();

            audioManager.helperCollectSFX.Play();

            Destroy(this.gameObject);
        }
    }
}
