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

    //Computer
    [SerializeField] GameObject computerSignal;

    [SerializeField] GameObject taskList;

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
        
        if (areBatteriesCollected && isSatelliteDeployed && arePlantsHydrated && areSolarPanelsDeployed)
        {
            StartCoroutine(SignalAndDoorSequence());
        }
    }    

    private IEnumerator SignalAndDoorSequence()
    {
        FindObjectOfType<Letterbox>().MoveIn();
        //Computer Signal Sequence
        yield return new WaitForSeconds(5f);

        PlayerStateMachine playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        playerStateMachine.EnterCinematicState();

        taskList.SetActive(false);

        //Computer Signal Sequence
        yield return new WaitForSeconds(1f);

        FadeToBlack();

        yield return new WaitForSeconds(1f);

        //change camera
        cameraManager.FocusComputerCamera();

        yield return new WaitForSeconds(1f);

        FadeFromBlack();

        yield return new WaitForSeconds(1f);

        computerSignal.SetActive(true);

        yield return new WaitForSeconds(2f);

        Computer computer = FindObjectOfType<Computer>();
        computer.ActivateSidePanel();
        audioMan.extendSFX.Play();

        yield return new WaitForSeconds(7f); //watch computer for 8 secs

        FadeToBlack();

        yield return new WaitForSeconds(1f);

        //change cameras - computer > gate
        cameraManager.ResetComputerCamera();
        cameraManager.FocusGateCamera();

        yield return new WaitForSeconds(1f);

        FadeFromBlack();

        yield return new WaitForSeconds(1f);

        doorAnimator.enabled = true;
        audioMan.gateSFXOpen.Play();

        yield return new WaitForSeconds(3f);

        FadeToBlack();

        yield return new WaitForSeconds(1f);

        // change cameras - Gate > Battery Charger
        cameraManager.ResetGateCamera();
        cameraManager.FocusBatteryChargerCamera();

        yield return new WaitForSeconds(1f);

        FadeFromBlack();

        yield return new WaitForSeconds(3f); // focus on battery charger for 10 secs

        BatteryRecharger batteryRecharger = FindObjectOfType<BatteryRecharger>();

        batteryRecharger.SetGreenLight();
        batteryRecharger.DischargeBatteryAnim();
        audioMan.dischargeSFXOpen.Play();
        audioMan.finishSFXOpen.Play();

        yield return new WaitForSeconds(6f); // focus on battery charger for 10 secs

        FadeToBlack();

        yield return new WaitForSeconds(1f);

        //change cameras
        cameraManager.ResetBatteryChargerCamera();

        yield return new WaitForSeconds(1f);

        FadeFromBlack();

        playerStateMachine.inCinematic = false;

        FindObjectOfType<Letterbox>().MoveOut();
    }

    private void FadeFromBlack()
    {
        if (screenFadeManager != null)
        {
            screenFadeManager.FadeIn();
        }
    }

    private void FadeToBlack()
    {
        if (screenFadeManager != null)
        {
            screenFadeManager.TurnOnAnimatorAndFadeOut();
        }
    }
}
