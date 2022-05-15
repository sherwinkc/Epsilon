using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DebugManager : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;
    Collector collector;
    PlayerVFXManager playerVFXMan;

    [Header("Toggle Debug")]
    public bool displayPlayerState;
    public bool displayerPlayerVelocity;
    public bool displayMovementInput;
    public bool displayJumpLogic;
    public bool displayJetpackValues;    

    [Header("Change Game Time")]
    public bool timeScaleOn = false;
    public float timeScaleFactor = 1f;

    [Header("Player State")]
    public TMP_Text playerStateDebug;
    //public TMP_Text previousStateDebug;
    public TMP_Text standingOnWhatMaterial;
    public TMP_Text isGroundedDebug;

    [Header("Player Velocity")]
    public TMP_Text playerVelocityX;
    public TMP_Text playerVelocityY;

    [Header("Player Input")]
    public TMP_Text currentInputX;
    public TMP_Text currentInputY;

    [Header("Jump Variables")]
    public TMP_Text jumpBuffer;
    public TMP_Text coyoteTime;

    [Header("Jetpack")]
    public TMP_Text jetpackTime;

    [Header("Collisions")]
    //public TMP_Text collidingWith;
    public TMP_Text standingOn;

    [Header("Timeline / Cinematics")]
    public PlayableDirector[] playableDirectors;
    public bool turnOffAllTimelines = false;
    [SerializeField] bool timeline1Active;
    [SerializeField] bool timeline2Active;
    [SerializeField] bool timeline3Active;
    [SerializeField] bool timeline4Active;


    [Header("Collectibles")]
    public TMP_Text orbCount;

    [Header("Ledge Climb Checks")]
    public Animator animator;
    public TMP_Text ledgeDetectedAnimator;
    public TMP_Text isTouchingWall;
    public TMP_Text isTouchingLedge;
    public TMP_Text isNearClimbableLedge;
    public TMP_Text isMountDetected;
    public TMP_Text isLettingGoOfLedgeAnimator;


    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        collector = FindObjectOfType<Collector>();
        playerVFXMan = FindObjectOfType<PlayerVFXManager>();
    }

    void Start()
    {
        if (timeScaleOn)
        {
            SetTimeScale();
        } 
        else
        {
            Time.timeScale = 1f;
        }

        //playableDirectors = FindObjectsOfType<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayPlayerState();
        //DisplayPreviousPlayerState();

        DisplayIsGrounded();

        if (displayerPlayerVelocity) DisplayPlayerVelocity();

        if (displayMovementInput) DisplayCurrentMovementInputValues();

        if (displayJumpLogic) DisplayJumpLogic();

        if (displayJetpackValues) DisplayJetpackValues();

        CheckPlayableDirectors(); //Turn this on or off to activate timelines

        DisplayOrbCount();

        ReloadScene();

        DisplayIsStandingOn();

        DisplayLedgeAndClimbingChecks();

        //if (collidingWith != null && playerStateMachine.collisionForDebug != null) collidingWith.text = "Colliding With: " + playerStateMachine.collisionForDebug.gameObject.name.ToString();
        //if (jumpBuffer != null) jumpBuffer.text = "Jump Buffer: " + playerStateMachine.jumpBufferCounter.ToString("F4");
        //isMountDetected.text = "Is Mount Detected: " + animator.GetBool("mountDetected").ToString();


    }

    private void DisplayLedgeAndClimbingChecks()
    {
        ledgeDetectedAnimator.text = "Ledge Detected: " + animator.GetBool("ledgeDetected").ToString();
        isTouchingWall.text = "isTouchingWall: " + playerStateMachine.isTouchingWall.ToString();
        isTouchingLedge.text = "isTouchingLedge: " + playerStateMachine.isTouchingLedge.ToString();

        isNearClimbableLedge.text = "isNearClimbabeLedge: " + playerStateMachine.ledgeInfo.isNearClimbableMesh.ToString();
        isLettingGoOfLedgeAnimator.text = "Is Letting Go Of Ledge: " + animator.GetBool("isLettingGoLedge").ToString();
    }

    private void DisplayIsStandingOn()
    {
        standingOnWhatMaterial.text = "Standing On: " + playerVFXMan.material.ToString();
    }

    private void DisplayIsGrounded()
    {
        isGroundedDebug.text = "is Grounded: " + playerStateMachine.IsGrounded.ToString();
    }

    private void DisplayOrbCount()
    {
        if (orbCount != null) orbCount.text = "Orb Count: " + collector.orbs.ToString();
    }
    
    private void CheckPlayableDirectors()
    {
        if (turnOffAllTimelines)
        {
            for (int i = 0; i < playableDirectors.Length; i++)
            {
                playableDirectors[i].enabled = false;
            }

            return;
        }

        if (timeline1Active)
        {
            playableDirectors[0].enabled = true;
        }
        else if(!timeline1Active)
        {
            playableDirectors[0].enabled = false;
        }

        if (timeline2Active)
        {
            playableDirectors[1].enabled = true;
        }
        else
        {
            playableDirectors[1].enabled = false;
        }

        if (timeline3Active)
        {
            playableDirectors[2].enabled = true;
        }
        else
        {
            playableDirectors[2].enabled = false;
        }

        /*if (timeline4Active)
        {
            playableDirectors[3].enabled = true;
        }
        else
        {
            playableDirectors[3].enabled = false;
        }*/

        /*if (turnOffAllTimelines)
        {
            for (int i = 0; i < playableDirectors.Length; i++)
            {
                playableDirectors[i].enabled = true;
            }
        }*/

        /*if (!turnOffAllTimelines)
        {
            for (int i = 0; i < playableDirectors.Length; i++)
            {
                playableDirectors[i].enabled = false;
            }
        }*/
    }

    private void DisplayJetpackValues()
    {
        if (jetpackTime != null) jetpackTime.text = "Jetpack Time: " + playerStateMachine.thrustCounter.ToString("F2");
    }

    private void DisplayCurrentMovementInputValues()
    {
        if (currentInputX != null) currentInputX.text = "Current Input X: " + playerStateMachine.CurrentMovement.x.ToString("F2");
        if (currentInputY != null) currentInputY.text = "Current Input Y: " + playerStateMachine.CurrentMovement.y.ToString("F2");
    }

    private void DisplayPlayerVelocity()
    {
        if (playerVelocityX != null) playerVelocityX.text = "Player Velocity X: " + playerStateMachine.Rigidbody.velocity.x.ToString("F2");
        if (playerVelocityY != null) playerVelocityY.text = "Player Velocity Y: " + playerStateMachine.Rigidbody.velocity.y.ToString("F2");
    }

    private void DisplayPlayerState()
    {
        playerStateDebug.text = "player state: " + playerStateMachine.CurrentState.ToString();
    }

    private void DisplayPreviousPlayerState()
    {
        //previousStateDebug.text = "Previous Player State: " + playerStateMachine.CurrentState.ToString();
    }

    private static void ReloadScene()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainLevel");
        }
    }

    private void DisplayJumpLogic()
    {
        if (jumpBuffer != null)
        {
            if (playerStateMachine.jumpBufferCounter >= 0 && playerStateMachine.jumpBufferCounter <= playerStateMachine.jumpBufferTime)
            {
                jumpBuffer.text = "Jump Buffer: " + playerStateMachine.jumpBufferCounter.ToString("F4");
            }
        }

        if (coyoteTime != null)
        {
            if (playerStateMachine.coyoteTimeCounter >= 0 && playerStateMachine.coyoteTimeCounter <= playerStateMachine.coyoteTime)
            {
                coyoteTime.text = "Coyote Time: " + playerStateMachine.coyoteTimeCounter.ToString("F2");
            }
        }
    }

    private void SetTimeScale()
    {
        Time.timeScale = timeScaleFactor;
    }
}
