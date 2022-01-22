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
        if (playerVelocityX != null) playerVelocityX.text = "Player Velocity X: " + playerStateMachine.Rigidbody.velocity.x.ToString("F2");
        if (playerVelocityY != null) playerVelocityY.text = "Player Velocity Y: " + playerStateMachine.Rigidbody.velocity.y.ToString("F2");

        if (currentInputX != null) currentInputX.text = "Current Input X: " + playerStateMachine.CurrentMovement.x.ToString("F2");
        if (currentInputY != null) currentInputY.text = "Current Input Y: " + playerStateMachine.CurrentMovement.y.ToString("F2");

        //if (jumpBuffer != null) jumpBuffer.text = "Jump Buffer: " + playerStateMachine.jumpBufferCounter.ToString("F4");

        DisplaJumpLogic();

        ExitGame();
    }

    private void DisplaJumpLogic()
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
            if (playerStateMachine.coyoteTime >= 0 && playerStateMachine.coyoteTime <= playerStateMachine.coyoteTimeCounter)
            {
                coyoteTime.text = "Coyote Time: " + playerStateMachine.coyoteTimeCounter.ToString("F2");
            }
        }
    }

    private static void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
