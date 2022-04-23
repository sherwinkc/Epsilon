using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    LevelManager levelManager;

    public PlayerStateMachine playerStateMachine;
    public Canvas pauseMenuCanvas;  


    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        levelManager = FindObjectOfType<LevelManager>();
    }


    void Start()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        EnableDisablePauseMenu();
    }

    private void EnableDisablePauseMenu()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && pauseMenuCanvas.gameObject.activeSelf == true)
        {
            playerStateMachine.EnableGameplayControls();
            pauseMenuCanvas.gameObject.SetActive(false);

            if (!levelManager.cursorOn) Cursor.visible = false;

            Time.timeScale = 1f; //TODO get rid
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            playerStateMachine.DisableGameplayControls();
            pauseMenuCanvas.gameObject.SetActive(true);

            if (!levelManager.cursorOn) Cursor.visible = true;

            Time.timeScale = 0f; //TODO get rid
        }
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f; //TODO get rid
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f; //TODO get rid
        SceneManager.LoadScene("MainLevel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
