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
    Animator animator;

    public GameObject interactHUD;
    public GameObject interactUnableToHUD;
    public LiftManager liftManager;

    public Transform buttonTransform;
    public float xTeleportOffset;
    public float yTeleportOffset;

    //lift
    [SerializeField] GameObject lift;
    public bool isCloseEnoughToLiftButton;
    //[SerializeField] bool isLiftOn = false;

    public bool interactHasSFXPlayed = false;

    //battery
    public bool isCloseEnoughToBattery = false;

    //rover
    public bool isCloseEnoughToRover = false;

    //crop button
    public bool isCloseEnoughToCropButton = false;
    bool isCropsOn = false;

    //solar Panels
    public bool isCloseEnoughToSolarPanelButton = false;
    bool isSolarPanelsOn = false;

    //Comms Tower
    public bool isCloseEnoughToCommsTowerButton = false;
    bool isCommsTowerOn = false;

    //Battery Recharger
    public bool isCloseEnoughToBatteryRecharger = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        helper = FindObjectOfType<HelperMovement>();
        rover = FindObjectOfType<RoverBehaviour>();
        animator = GetComponent<Animator>();
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
                TeleportPlayerAndAnimate();
                Invoke("ActivateLift", 1.2f);
            }

            //check if close to battery
            else if (isCloseEnoughToBattery)
            {
                helper.objectToPickUp = colliderToPickUp.gameObject;
                //helper.objectToPickUp.GetComponent<Collider2D>().enabled = false;
                helper.isPickingUpItem = true;
                isCloseEnoughToBattery = false;
                interactHUD.SetActive(false);

                animator.Play("Player_HandGesture");
                //audioManager.buttonPressSFX.Play(); //TODO Another sound for hand gesture
            }

            //check if close enough to rover && holding a battery
            else if (isCloseEnoughToRover && helper.isCarryingBattery)
            {
                //helper.objectToPickUp = colliderToPickUp.gameObject;
                helper.isDepositingToRover = true;
                isCloseEnoughToRover = false;
                interactHUD.SetActive(false);

                animator.Play("Player_HandGesture");
                //audioManager.buttonPressSFX.Play(); //TODO Another sound for hand gesture
            }

            //check if close enough to battery recharger && holding a battery
            else if (isCloseEnoughToBatteryRecharger && helper.isCarryingBattery)
            {
                //helper.objectToPickUp = colliderToPickUp.gameObject;
                helper.isDepositingToBatteryRecharger = true;
                interactHUD.SetActive(false);

                animator.Play("Player_HandGesture");
                //audioManager.buttonPressSFX.Play(); //TODO Another sound for hand gesture
            }

            else if (isCloseEnoughToCropButton && !isCropsOn)
            {
                TeleportPlayerAndAnimate();
                Invoke("ActivateCrops", 1f);
            }

            else if (isCloseEnoughToSolarPanelButton && !isSolarPanelsOn)
            {
                TeleportPlayerAndAnimate();
                Invoke("ActivateSolarPanels", 1f);
            }

            else if (isCloseEnoughToCommsTowerButton && !isCommsTowerOn)
            {
                TeleportPlayerAndAnimate();
                Invoke("ActivateCommsTower", 1f);
            }
        } 
    }

    private void ActivateLift()
    {
        Invoke("MoveLiftAfterADelay", 0.2f); //had to invoke at a later time - this was causing issues when the animation was still playing but th lift was still moving

        audioManager.buttonPressSFX.Play();
    }

    private void MoveLiftAfterADelay()
    {
        if (liftManager != null) liftManager.moveLift = !liftManager.moveLift;
    }


    private void ActivateCommsTower()
    {
        FindObjectOfType<CommsTower>().GetComponent<Animator>().enabled = true;
        isCommsTowerOn = true;

        audioManager.buttonPressSFX.Play();
        audioManager.commsTowerSFXOpen.Play();
    }

    private void ActivateSolarPanels()
    {
        SolarPanel[] solarPanels = FindObjectsOfType<SolarPanel>();

        for (int i = 0; i < solarPanels.Length; i++)
        {
            solarPanels[i].GetComponent<Animator>().enabled = true;
        }

        audioManager.solarPanelSFXOpen.Play();
        isSolarPanelsOn = true;

        audioManager.buttonPressSFX.Play();
        audioManager.buttonPressSFX.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleLiftLogic(collision);

        //Battery
        if (collision.gameObject.CompareTag("Battery") && !helper.isCarryingBattery)
        {
            isCloseEnoughToBattery = true;
            colliderToPickUp = collision;
            interactHUD.SetActive(true);
            //PlayInteractSound();
        }

        //Battery Deposit
        if (collision.gameObject.CompareTag("BatteryDeposit"))
        {
            FindObjectOfType<HelperMovement>().depositTransform = collision.transform;
        }

        //Battery Deposit
        if (collision.gameObject.CompareTag("BatteryDeposit") && !rover.canMove && helper.isCarryingBattery)
        {
            DepositManager depositManager = collision.gameObject.GetComponent<DepositManager>();

            if (depositManager != null && depositManager.isRoverDockedHere)
            {
                isCloseEnoughToRover= true;
                interactHUD.SetActive(true);
                //PlayInteractSound();
                //colliderToPickUp = collision;
            }
        }
        else if (collision.gameObject.CompareTag("BatteryDeposit") && !helper.isCarryingBattery)
        {
            interactUnableToHUD.SetActive(true);
        }

        //crop Button
        if (collision.gameObject.CompareTag("CropButton") && !isCropsOn)
        {
            buttonTransform = collision.transform;
            isCloseEnoughToCropButton = true;
            interactHUD.SetActive(true);
        }

        //solar panels Button
        if (collision.gameObject.CompareTag("SolarPanelsButton") && !isSolarPanelsOn)
        {
            buttonTransform = collision.transform;
            isCloseEnoughToSolarPanelButton = true;
            interactHUD.SetActive(true);
        }

        if (collision.gameObject.CompareTag("CommsTowerButton") && !isCommsTowerOn)
        {
            buttonTransform = collision.transform;
            isCloseEnoughToCommsTowerButton = true;
            interactHUD.SetActive(true);
        }

        if (collision.gameObject.CompareTag("BatteryRecharger") && helper.isCarryingBattery)
        {
            isCloseEnoughToBatteryRecharger = true;
            interactHUD.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("BatteryRecharger") && !helper.isCarryingBattery)
        {
            interactUnableToHUD.SetActive(true);
        }
    }

    public void PlayInteractSound()
    {
        if (audioManager.interactSFX != null)
        {
            if(!interactHasSFXPlayed) audioManager.interactSFX.Play();
            interactHasSFXPlayed = true;
        }
    }

    private void HandleLiftLogic(Collider2D collision)
    {
        //Debug.Log("HandleLiftLogic");

        liftManager = collision.GetComponentInParent<LiftManager>();

        if (collision.gameObject.CompareTag("Lift") && !liftManager.moveLift)
        {
            buttonTransform = collision.transform;
            isCloseEnoughToLiftButton = true;
            interactHUD.SetActive(true);
            //PlayInteractSound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCloseEnoughToLiftButton = false;
        isCloseEnoughToBattery = false;
        isCloseEnoughToRover = false;
        interactHUD.SetActive(false);
        interactUnableToHUD.SetActive(false);
        isCloseEnoughToCropButton = false;
        isCloseEnoughToSolarPanelButton = false;
        isCloseEnoughToCommsTowerButton = false;
        isCloseEnoughToBatteryRecharger = false;

        buttonTransform = null;

        /*if (collision.gameObject.CompareTag("BatteryDeposit") || collision.gameObject.CompareTag("Battery") || collision.gameObject.CompareTag("Lift"))
        {
            interactHasSFXPlayed = false;
        }*/
    }

    private void TeleportPlayerAndAnimate()
    {
        //teleport player
        PlayerStateMachine player = FindObjectOfType<PlayerStateMachine>();
        player.transform.position = new Vector2(buttonTransform.position.x + xTeleportOffset, buttonTransform.position.y + yTeleportOffset);

        //animate button press
        animator.Play("Player_PressButton");
    }

    private void ActivateCrops()
    {
        FindObjectOfType<CropsManager>().ActivateSprinklers();
        interactHUD.SetActive(false);
        isCropsOn = true;
        audioManager.buttonPressSFX.Play();
    }
}
