using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStateDebug != null) playerStateDebug.text = "Player State: " + playerStateMachine.CurrentState.ToString();

        //Previous State
        //if (previousStateDebug != null) previousStateDebug.text = "Previous Player State: " + playerStateMachine.CurrentState.ToString();

        if (playerVelocityX != null) playerVelocityX.text = "Player Velocity X: " + playerStateMachine.Rigidbody.velocity.x.ToString("F2");
        if (playerVelocityY != null) playerVelocityY.text = "Player Velocity Y: " + playerStateMachine.Rigidbody.velocity.y.ToString("F2");

        if (currentInputX != null) currentInputX.text = "Current Input X: " + playerStateMachine.CurrentMovement.x.ToString("F2");
        if (currentInputY != null) currentInputY.text = "Current Input Y: " + playerStateMachine.CurrentMovement.y.ToString("F2");

        if (jetpackTime != null) jetpackTime.text = "Jetpack Time: " + playerStateMachine.thrustCounter.ToString("F2");

        //if (collidingWith != null && playerStateMachine.collisionForDebug != null) collidingWith.text = "Colliding With: " + playerStateMachine.collisionForDebug.gameObject.name.ToString();



        //if (jumpBuffer != null) jumpBuffer.text = "Jump Buffer: " + playerStateMachine.jumpBufferCounter.ToString("F4");

        DisplayJumpLogic();

        ReloadScene();

        ExitGame();
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

    private void ShowPlayerState()
    {

    }

    private void SetTimeScale()
    {
        Time.timeScale = timeScaleFactor;
    }
}
