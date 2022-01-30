using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Interact : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] GameObject lift;

    [SerializeField] bool isCloseEnoughToLiftButton;
    [SerializeField] bool isMovingTowardButton, isMovingAwayFromButton;
    [SerializeField] float moveSpeedMultiplier = 10f;
    public bool isLiftOn = false;   

    //IK
    [SerializeField] Solver2D solver;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        solver.weight = 0f;
    }


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2)) && isCloseEnoughToLiftButton)
        {
            isLiftOn = !isLiftOn;
        }

        HandleIKMoveSpeed();
    }

    private void HandleIKMoveSpeed()
    {
        if (isMovingTowardButton && solver.weight <= 1f)
        {
            solver.weight += Time.deltaTime * moveSpeedMultiplier;
        }
        else if (isMovingAwayFromButton && solver.weight >= 0f)
        {
            solver.weight -= Time.deltaTime * moveSpeedMultiplier;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        LiftManager liftManager = collision.GetComponentInParent<LiftManager>();
        Transform buttonTarget = collision.GetComponentInChildren<Transform>();

        if (collision.gameObject.CompareTag("Lift"))
        {
            isCloseEnoughToLiftButton = true;

            if (isLiftOn)
            {
                if (liftManager != null) liftManager.isMoving = true;
                isMovingTowardButton = true;
                isMovingAwayFromButton = false;
            }
            else if (!isLiftOn)
            {
                if (liftManager != null) liftManager.isMoving = false;
                isMovingTowardButton = false;
                isMovingAwayFromButton = true;
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCloseEnoughToLiftButton = false;
        solver.weight = 0f;
        isMovingTowardButton = false;
        isMovingAwayFromButton = false;
    }
}
