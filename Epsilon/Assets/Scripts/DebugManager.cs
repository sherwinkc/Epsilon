using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    //PlayerStateMachine playerStateMachine;
    
    //Player State
    public TMP_Text playerStateDebug;
    //public TMP_Text standingOnWhatMaterial;

    //Player Movement
    public TMP_Text playerVelocityX;
    public TMP_Text playerVelocityY;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerStateDebug != null) playerStateDebug.text = "Player State: " + FindObjectOfType<PlayerStateMachine>().CurrentState.ToString();
        if(playerVelocityX != null) playerVelocityX.text = "Player Velocity X: " + FindObjectOfType<PlayerStateMachine>().Rigidbody.velocity.x.ToString("F2");
        if (playerVelocityY != null) playerVelocityY.text = "Player Velocity Y: " + FindObjectOfType<PlayerStateMachine>().Rigidbody.velocity.y.ToString("F2");

        ExitGame();
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
}
