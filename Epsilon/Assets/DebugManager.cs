using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    //PlayerStateMachine playerStateMachine;
    
    public TMP_Text playerStateDebug;
    //public TMP_Text playerSubstateDebug;
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShowPlayerState();

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
        playerStateDebug.text = "Player State: " + FindObjectOfType<PlayerStateMachine>().CurrentState.ToString();
        //yerStateDebug.text = "Player State: " + FindObjectOfType<PlayerBaseState>().CurrentState.ToString();
    }
}
