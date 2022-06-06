using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    PauseMenu pauseMenu;
    FinalRoomSequence finalRoomSeq;
    EquipJetpack equipJetpack;
    CameraManager cameraManager;
    AudioManager audioMan;
    ScreenFadeManager screenFadeManager;

    [SerializeField] VideoPlayer video;
    [SerializeField] Animator doorAnimator;

    public float timeBeforeFade;
    public float timeBeforeSceneLoad;

    public bool cursorOn = false;
    public string levelToLoad;

    public bool areSolarPanelsDeployed = false;
    public bool isSatelliteDeployed = false;
    public bool arePlantsHydrated = false;
    public bool areBatteriesCollected = false;

    public TMP_Text task1text, task2text, task3text, task4text;

    private void Awake()
    {
        pauseMenu = GetComponent<PauseMenu>();
        finalRoomSeq = FindObjectOfType<FinalRoomSequence>();
        equipJetpack = FindObjectOfType<EquipJetpack>();
        cameraManager = FindObjectOfType<CameraManager>();
        audioMan = FindObjectOfType<AudioManager>();
        screenFadeManager = FindObjectOfType<ScreenFadeManager>();

        video.Stop();
    }

    void Start()
    {
        if(!cursorOn) Cursor.visible = false;
    }

    public void LoadFinalRoom()
    {
        StartCoroutine(LoadFinalRoomCo());
    }

    public IEnumerator LoadFinalRoomCo()
    {
        equipJetpack.thrustHolder.SetActive(false);

        video.Play();

        pauseMenu.playerStateMachine.FadeScreen();

        yield return new WaitForSeconds(timeBeforeSceneLoad);

        ScreenFadeManager screenFadeManager = FindObjectOfType<ScreenFadeManager>(); //TODO Cache this (Also Above)

        if (screenFadeManager != null) screenFadeManager.FadeIn();

        finalRoomSeq.ActivateFinalRoomSequence();
    }

    public void RespawnFromCheckpoint()
    {
        Time.timeScale = 1f;
        pauseMenu.pauseMenuCanvas.gameObject.SetActive(false);
        pauseMenu.playerStateMachine.Respawn();
        pauseMenu.playerStateMachine.EnterIdleState();
        pauseMenu.playerStateMachine.EnableGameplayControls();
    }

    public void CheckLevelIntroStatus()
    {
        if (arePlantsHydrated)
        {
            task1text.text = "<s>1. Hydrate the plants.</s>";
        }

        if (areBatteriesCollected)
        {
            task2text.text = "<s>2. Retrieve and recharge the batteries.</s>";
        }  

        if (areSolarPanelsDeployed)
        {
            task3text.text = "<s>3. Activate the solar panel array.</s>";
        }
        
        if (isSatelliteDeployed)
        {
            task4text.text = "<s>4. Deploy the satellite scanner.</s>";
        }   
        
        if (areBatteriesCollected && isSatelliteDeployed && arePlantsHydrated && areBatteriesCollected)
        {
            StartCoroutine(CameraCutSequence());
        }
    }    

    private IEnumerator CameraCutSequence()
    {
        yield return new WaitForSeconds(1f);

        if (screenFadeManager != null)
        {
            screenFadeManager.TurnOnAnimatorAndFadeOut(); 
        }

        yield return new WaitForSeconds(2f);

        cameraManager.FocusCamera();

        yield return new WaitForSeconds(0.5f);

        if (screenFadeManager != null)
        {
            screenFadeManager.FadeIn();
        }

        yield return new WaitForSeconds(1f);

        doorAnimator.enabled = true;
        audioMan.gateSFXOpen.Play();

        yield return new WaitForSeconds(2f);

        if (screenFadeManager != null)
        {
            screenFadeManager.TurnOnAnimatorAndFadeOut();
        }

        yield return new WaitForSeconds(1f);

        cameraManager.ResetCamera();

        yield return new WaitForSeconds(2f);

        if (screenFadeManager != null)
        {
            screenFadeManager.FadeIn();
        }
    }
}
