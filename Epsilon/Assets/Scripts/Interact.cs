using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    AudioManager audioManager;
    HelperMovement helper;
    Collider2D colliderToPickUp;
    RoverBehaviour rover;

    public GameObject interactHUD;
    public LiftManager liftManager;

    //lift
    [SerializeField] GameObject lift;
    public bool isCloseEnoughToLiftButton;
    //[SerializeField] bool isLiftOn = false;
    [SerializeField] bool isLiftLoopPlaying = false;

    //battery
    public bool isCloseEnoughToBattery = false;

    //rover
    public bool isCloseEnoughToRover = false;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        helper = FindObjectOfType<HelperMovement>();
        rover = FindObjectOfType<RoverBehaviour>();
    }

    void Start()
    {
        interactHUD.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            //check if close to lift
            if (isCloseEnoughToLiftButton)
            {
                if (liftManager != null) liftManager.moveLift = !liftManager.moveLift;
            }

            //check if close to battery
            else if (isCloseEnoughToBattery)
            {
                helper.objectToPickUp = colliderToPickUp.gameObject;
                //helper.objectToPickUp.GetComponent<Collider2D>().enabled = false;
                helper.isPickingUpItem = true;
                isCloseEnoughToBattery = false;
                interactHUD.SetActive(false);
            }

            //check if close enough to rover && holding a battery
            else if (isCloseEnoughToRover && helper.isCarryingBattery)
            {
                //helper.objectToPickUp = colliderToPickUp.gameObject;
                helper.isDepositingToRover = true;
                isCloseEnoughToRover = false;
                interactHUD.SetActive(false);
            }
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleLiftLogic(collision);

        if (collision.gameObject.CompareTag("BatteryDeposit"))
        {
            FindObjectOfType<HelperMovement>().depositTransform = collision.transform;
        }

        if (collision.gameObject.CompareTag("Battery") && !helper.isCarryingBattery)
        {
            isCloseEnoughToBattery = true;
            colliderToPickUp = collision;
            interactHUD.SetActive(true);
        }

        if (collision.gameObject.CompareTag("BatteryDeposit") && !rover.canMove && helper.isCarryingBattery)
        {
            DepositManager depositManager = collision.gameObject.GetComponent<DepositManager>();

            if (depositManager != null && depositManager.isRoverDockedHere)
            {
                isCloseEnoughToRover= true;
                interactHUD.SetActive(true);
                //colliderToPickUp = collision;
            }
        }
    }

    private void HandleLiftLogic(Collider2D collision)
    {
        Debug.Log("HandleLiftLogic");

        liftManager = collision.GetComponentInParent<LiftManager>();

        if (collision.gameObject.CompareTag("Lift"))
        {
            isCloseEnoughToLiftButton = true;
            interactHUD.SetActive(true);

            PlayLiftAudio();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        isCloseEnoughToLiftButton = false;
        isCloseEnoughToBattery = false;
        isCloseEnoughToRover = false;
        interactHUD.SetActive(false);

        /*if (collision.gameObject.CompareTag("BatteryDeposit"))
        {
            FindObjectOfType<HelperMovement>().depositTransform = null;
        }*/
    }
    private void PlayLiftAudio()
    {
        //if lift is moving
        if (liftManager.moveLift)
        {
            //play audio
            if (!isLiftLoopPlaying)
            {
                audioManager.liftActiveLoop.Play();
                isLiftLoopPlaying = true;
            }
        }
        //if lift is not moving
        else if (!liftManager.moveLift) 
        {
            //stop audio
            if (isLiftLoopPlaying)
            {
                audioManager.liftActiveLoop.Stop();
                isLiftLoopPlaying = false;
            }
        }
    }
}
