using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Collapse : MonoBehaviour
{
    AudioManager audioManager;
    LevelMusicManager levelMusicManager;

    PlayerStateMachine playerStateMachine;

    [SerializeField] BoxCollider2D boxCollider2D;

    [SerializeField] Rigidbody2D[] rbs;
    [SerializeField] DestroyOverTime[] destroy;

    [SerializeField] GameObject blackScreen;
    [SerializeField] Transform wakeUpLocation;

    [SerializeField] float timeScaleFactor = 0.25f;
    [SerializeField] float blackScreenWaitTime = 2f;
    [SerializeField] float blackScreenTime = 10f;

    [SerializeField] PlayableDirector playableDirector;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        levelMusicManager = FindObjectOfType<LevelMusicManager>();
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();

        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].constraints = RigidbodyConstraints2D.FreezeAll;
        }

        blackScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxCollider2D.enabled = false;

            ReleaseRocks();
            //ActivateDestroyMethod();
            audioManager.rockfallSFX.Play();

            StartCoroutine(FallSequence());
        }
    }

    private void ActivateDestroyMethod()
    {
        for (int i = 0; i < destroy.Length; i++)
        {
            destroy[i].Destroy();
        }
    }

    void ReleaseRocks()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].constraints = RigidbodyConstraints2D.None;
        }
    }

    private IEnumerator FallSequence()
    {
        Time.timeScale = timeScaleFactor;
        levelMusicManager.music4.Stop();
        audioManager.playerBreathingSFX.Stop();
        yield return new WaitForSeconds(blackScreenWaitTime);

        //black screen
        blackScreen.SetActive(true);

        audioManager.crashSFX.Play();

        Time.timeScale = 1f;

        //Destroy falling rocks
        for (int i = 0; i < destroy.Length; i++)
        {
            destroy[i].Destroy();
        }

        FindObjectOfType<PlayerStateMachine>().inCinematic = true;


        //play audio
        //stop music

        //hold player in place

        yield return new WaitForSeconds(blackScreenTime);

        //teleport player
        playerStateMachine.transform.position = wakeUpLocation.position;

        playableDirector.Play();

        blackScreen.SetActive(false);

        ScreenFadeManager screenFadeMan = FindObjectOfType<ScreenFadeManager>(); // TODO Cache ScreenFadeManager
        if (screenFadeMan != null) screenFadeMan.FadeIn();

            audioManager.playerBreathingSFX.Play();
        levelMusicManager.music4.Play();

        //Initate cinematic state
        //start timeline w/ animation of standing up
        //play music
    }
}
