using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    LevelManager levelManager;
    AudioManager audioManager;

    [SerializeField] GameObject button;
    [SerializeField] Button defaultButton;
    [SerializeField] Slider volumeSlider;

    public PlayerStateMachine playerStateMachine;
    public Canvas pauseMenuCanvas;


    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        levelManager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();

        //Set60FPS();
    }


    void Start()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        EnableDisablePauseMenu();

        PlaySoundWhenButtonChanges();
    }

    private void PlaySoundWhenButtonChanges()
    {
        if (EventSystem.current.currentSelectedGameObject != button && pauseMenuCanvas.gameObject.activeSelf)
        {
            audioManager.highlightUISFX.Play();
            button = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            defaultButton.Select();
        }
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

        ChangeVolume();
    }

    private void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
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

    public void SetResolution720()
    {
        Screen.SetResolution(1280, 720, true);
    }

    public void SetResolution1080()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    public void SetResolution4K()
    {
        Screen.SetResolution(3840, 2160, true);
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = true;
    }

    public void SetWindowedMode()
    {
        Screen.fullScreen = false;
    }

    public void Set30FPS()
    {
        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    public void Set60FPS()
    {
        //QualitySettings.vSyncCount = 60;
        Application.targetFrameRate = 60;
    }
}

