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
    public TMP_Text previousStateDebug;    
    //public TMP_Text standingOnWhatMaterial;

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
    public TMP_Text collidingWith;
    public TMP_Text standingOn;

    [Header("Timeline / Cinematics")]
    public PlayableDirector[] playableDirectors;
    public bool timelinesAreActive = true;

    //TODO Move this
    [SerializeField] Image constellationImage;


    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
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

        constellationImage.gameObject.SetActive(false);

        playableDirectors = FindObjectsOfType<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayPlayerState) DisplayPlayerState();

        if (displayerPlayerVelocity) DisplayPlayerVelocity();

        if (displayMovementInput) DisplayCurrentMovementInputValues();

        if (displayJumpLogic) DisplayJumpLogic();

        if (displayJetpackValues) DisplayJetpackValues();

        CheckPlayableDirectorsEnabled();

        ReloadScene();
        ExitGame();


        //Turn off Constellation Image //TODO move to pause menu
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!constellationImage.isActiveAndEnabled)
            {
                constellationImage.gameObject.SetActive(true);
            }
            else
            {
                constellationImage.gameObject.SetActive(false);
            }
        }

        //Previous State
        //if (previousStateDebug != null) previousStateDebug.text = "Previous Player State: " + playerStateMachine.CurrentState.ToString();

        //if (collidingWith != null && playerStateMachine.collisionForDebug != null) collidingWith.text = "Colliding With: " + playerStateMachine.collisionForDebug.gameObject.name.ToString();


        //if (jumpBuffer != null) jumpBuffer.text = "Jump Buffer: " + playerStateMachine.jumpBufferCounter.ToString("F4");
    }

    private void CheckPlayableDirectorsEnabled()
    {
        if (timelinesAreActive)
        {
            for (int i = 0; i < playableDirectors.Length; i++)
            {
                playableDirectors[i].enabled = true;
            }
        }
        else if (!timelinesAreActive)
        {
            for (int i = 0; i < playableDirectors.Length; i++)
            {
                playableDirectors[i].enabled = false;
            }
        }
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
        if (playerStateDebug != null) playerStateDebug.text = "Player State: " + playerStateMachine.CurrentState.ToString();
    }

    private static void ReloadScene()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton7))
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

    private static void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            Application.Quit();
        }
    }

    private void SetTimeScale()
    {
        Time.timeScale = timeScaleFactor;
    }
}
