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

    //lift
    [SerializeField] GameObject lift;
    [SerializeField] bool isCloseEnoughToLiftButton;
    [SerializeField] bool isLiftOn = false;
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
                isLiftOn = !isLiftOn;
                audioManager.helperInteractSFX.Play();
            }
            //check if close to battery
            else if (isCloseEnoughToBattery)
            {
                helper.objectToPickUp = colliderToPickUp.gameObject;
                //helper.objectToPickUp.GetComponent<Collider2D>().enabled = false;
                helper.isPickingUpItem = true;
                isCloseEnoughToBattery = false;
                interactHUD.SetActive(false);
                audioManager.helperInteractSFX.Play();
            }
            //check if close enough to rover && holding a battery
            else if (isCloseEnoughToRover && helper.isCarryingBattery)
            {
                //helper.objectToPickUp = colliderToPickUp.gameObject;
                helper.isDepositingToRover = true;
                isCloseEnoughToRover = false;
                interactHUD.SetActive(false);
                audioManager.helperInteractSFX.Play();
            }
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleLiftLogic(collision);

        if (collision.gameObject.CompareTag("Battery") && !helper.isCarryingBattery)
        {
            isCloseEnoughToBattery = true;
            colliderToPickUp = collision;
            interactHUD.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Rover") && !rover.canMove && helper.isCarryingBattery)
        {
            isCloseEnoughToRover= true;
            interactHUD.SetActive(true);
            //colliderToPickUp = collision;
        }
    }

    private void HandleLiftLogic(Collider2D collision)
    {
        LiftManager liftManager = collision.GetComponentInParent<LiftManager>();

        if (collision.gameObject.CompareTag("Lift"))
        {
            isCloseEnoughToLiftButton = true;
            interactHUD.SetActive(true);

            if (isLiftOn)
            {
                //move lift
                if (liftManager != null) liftManager.isMoving = true;

                //play audio
                if (!isLiftLoopPlaying)
                {
                    audioManager.liftActiveLoop.Play();
                    isLiftLoopPlaying = true;
                }
            }
            else if (!isLiftOn)
            {
                //stop lift
                if (liftManager != null) liftManager.isMoving = false;

                //stop audio
                if (isLiftLoopPlaying)
                {
                    audioManager.liftActiveLoop.Stop();
                    isLiftLoopPlaying = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCloseEnoughToLiftButton = false;
        isCloseEnoughToBattery = false;
        isCloseEnoughToRover = false;
        interactHUD.SetActive(false);
        interactHUD.SetActive(false);
    }
}
