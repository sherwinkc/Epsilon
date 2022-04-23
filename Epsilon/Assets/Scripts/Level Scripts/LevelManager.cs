﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    PauseMenu pauseMenu;
    FinalRoomSequence finalRoomSeq;

    public float timeBeforeFade;
    public float timeBeforeSceneLoad;

    public bool cursorOn = false;
    public string levelToLoad;

    private void Awake()
    {
        pauseMenu = GetComponent<PauseMenu>();
        finalRoomSeq = FindObjectOfType<FinalRoomSequence>();
    }

    void Start()
    {
        if(!cursorOn) Cursor.visible = false;
    }

    public void LoadFinalRoom()
    {
        StartCoroutine(LoadFinalRoomCo());

        Debug.Log("Loaded Final Room");
    }

    public IEnumerator LoadFinalRoomCo()
    {
        //yield return new WaitForSeconds(timeBeforeFade);

        pauseMenu.playerStateMachine.FadeScreen();

        yield return new WaitForSeconds(timeBeforeSceneLoad);

        ScreenFadeManager screenFadeManager = FindObjectOfType<ScreenFadeManager>(); //TODO Cache this (Also Above)

        if (screenFadeManager != null)
        {
            screenFadeManager.FadeIn();
        }

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
}
