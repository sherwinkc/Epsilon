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
    DayAndNightCycle dayAndNight;
    LevelManager levelManager;
    BatteryRecharger batteryRecharger;

    [SerializeField] GameObject battery;

    [Header("Gate Sequence")]
    [Tooltip("Shortcut: F2")]
    public bool ActivateGateSequence;
    private bool sequenceInitiated = false;

    [Header("Toggle Debug")]
    public bool displayPlayerState;
    public bool displayerPlayerVelocity;
    public bool displayMovementInput;
    public bool displayJumpLogic;
    public bool displayJetpackValues;
    public bool logAllAudioPlaying = false;

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

    [Header("Gravity")]
    public TMP_Text playerGravity;

    [Header("Player Input")]
    public TMP_Text currentInputX;
    public TMP_Text currentInputY;

    [Header("Jump Variables")]
    public TMP_Text jumpBuffer;
    public TMP_Text coyoteTime;

    [Header("Jetpack")]
    public TMP_Text jetpackTime;
    public TMP_Text isJetpackOn;

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
    //public TMP_Text ledgeDetectedAnimator;
    //public TMP_Text isTouchingWall;
    public TMP_Text isTouchingClimbPointText;
    public TMP_Text isNearClimbableLedge;
    public TMP_Text isMountDetected;
    public TMP_Text isLettingGoOfLedgeAnimator;

    [Header("Datapad")]
    public GameObject datapad;

    [Header("Audio")]
    AudioSource[] sources;

    [Header("Day & Night Cycle")]
    public TMP_Text sunrising;
    public TMP_Text sunsetting;

    [Header("Session Play Time")]
    //public TMP_Text playTIme;
    public TMP_Text mins;
    public TMP_Text secs;
    float playTime = 0f;
    int minsCounter = 0;


    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        collector = FindObjectOfType<Collector>();
        playerVFXMan = FindObjectOfType<PlayerVFXManager>();
        dayAndNight = FindObjectOfType<DayAndNightCycle>();
        levelManager = FindObjectOfType<LevelManager>();
        batteryRecharger = FindObjectOfType<BatteryRecharger>();
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

        if (logAllAudioPlaying) sources = GameObject.FindObjectsOfType<AudioSource>();
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

        playerGravity.text = "Player Gravity: " + playerStateMachine.Rigidbody.gravityScale;

        isJetpackOn.text = "Is Jetpack On: " + playerStateMachine.isJetpackOn.ToString();

        LogAllAudioPlayingToConsole();

        CheckDayNightCycleAndDisplay();

        DisplayPlayTime();

        GateSequenceDebugLogic();

        if (Input.GetKeyDown(KeyCode.F3))
        {
            CreateABattery();
        }

        //if (collidingWith != null && playerStateMachine.collisionForDebug != null) collidingWith.text = "Colliding With: " + playerStateMachine.collisionForDebug.gameObject.name.ToString();
        //if (jumpBuffer != null) jumpBuffer.text = "Jump Buffer: " + playerStateMachine.jumpBufferCounter.ToString("F4");
        //isMountDetected.text = "Is Mount Detected: " + animator.GetBool("mountDetected").ToString();

    }

    private void CreateABattery()
    {
        if (battery != null) Instantiate(battery, playerStateMachine.transform.position, Quaternion.identity);
    }

    private void GateSequenceDebugLogic()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ActivateGateSequence = true;
        }

        if (ActivateGateSequence && !sequenceInitiated)
        {
            sequenceInitiated = true;

            levelManager.areBatteriesCollected = true;
            levelManager.isSatelliteDeployed = true;
            levelManager.arePlantsHydrated = true;

            batteryRecharger.batteriesDocked = 3;
            batteryRecharger.ActivateBatterySprites();
        }
    }

    private void DisplayPlayTime()
    {
        secs.text = (playTime += Time.deltaTime).ToString("F0") + " secs";

        if (playTime >= 59.999)
        {
            playTime = 0f;
            minsCounter++;
        }

        mins.text = minsCounter.ToString("F0") + " mins";
    }

    private void CheckDayNightCycleAndDisplay()
    {
        if (dayAndNight != null && dayAndNight.enabled)
        {
            sunrising.text = "Sunrising: " + dayAndNight.sunrising.ToString();
            sunsetting.text = "Sunsetting: " + dayAndNight.sunsetting.ToString();

        }
        else
        {
            sunrising.text = "Day & Night Cycle Off";
            sunsetting.text = "Day & Night Cycle Off";
        }
    }

    private void LogAllAudioPlayingToConsole()
    {
        if (logAllAudioPlaying)
        {
            foreach (AudioSource audioSource in sources)
            {
                if (audioSource.isPlaying) Debug.Log(audioSource.name + " is playing " + audioSource.clip.name);
            }
        }
    }

    private void DisplayLedgeAndClimbingChecks()
    {
        isTouchingClimbPointText.text = "Is Touching Climbable Point: " + playerStateMachine.isTouchingClimbingPoint.ToString();
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
