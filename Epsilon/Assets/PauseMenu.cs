using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;

    [SerializeField] Canvas pauseMenuCanvas;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
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

            Time.timeScale = 1f; //TODO get rid
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            playerStateMachine.DisableGameplayControls();
            pauseMenuCanvas.gameObject.SetActive(true);

            Time.timeScale = 0f; //TODO get rid
        }
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
