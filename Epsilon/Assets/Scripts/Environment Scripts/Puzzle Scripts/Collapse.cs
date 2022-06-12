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
    DayAndNightCycle dayNightCycle;
    ScreenFadeManager screenFadeManager;

    [SerializeField] BoxCollider2D boxCollider2D;

    [SerializeField] Rigidbody2D[] rbs;
    [SerializeField] DestroyOverTime[] destroy;

    [SerializeField] GameObject blackScreen;
    [SerializeField] Transform wakeUpLocation;

    [SerializeField] float timeScaleFactor = 0.25f;
    [SerializeField] float blackScreenWaitTime = 2f;
    [SerializeField] float blackScreenTime = 10f;

    [SerializeField] PlayableDirector playableDirector;

    //Player
    [SerializeField] GameObject helmetLight;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        levelMusicManager = FindObjectOfType<LevelMusicManager>();
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        dayNightCycle = FindObjectOfType<DayAndNightCycle>();
        screenFadeManager = FindObjectOfType<ScreenFadeManager>();

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

        //TODO Stop Current Music
        levelMusicManager.music1.Stop();
        levelMusicManager.music2.Stop();
        levelMusicManager.music3.Stop();
        levelMusicManager.music4.Stop();

        audioManager.playerBreathingSFX.Stop(); //TODO Not stopping breathing

        yield return new WaitForSeconds(blackScreenWaitTime);

        //black screen
        blackScreen.SetActive(true);

        Time.timeScale = 1f;

        FindObjectOfType<PlayerStateMachine>().inCinematic = true;

        playerStateMachine.Animator.SetBool("isFalling", false);   

        dayNightCycle.StartNightTime();

        //Destroy falling rocks
        for (int i = 0; i < destroy.Length; i++)
        {
            destroy[i].Destroy();
        }

        //audio
        audioManager.crashSFX.Play();
        audioManager.rockfallSFX.Stop();

        yield return new WaitForSeconds(blackScreenTime);

        //teleport player
        playerStateMachine.transform.position = wakeUpLocation.position;

        playableDirector.Play();

        blackScreen.SetActive(false);

        if (screenFadeManager != null) screenFadeManager.FadeIn();

        //switch on helmet light
        if (helmetLight != null) helmetLight.SetActive(true);

        //audio
        levelMusicManager.music1.Play(); //TODO Choos a nice intense piece of music here
        audioManager.playerBreathingSFX.Play();
    }
}
