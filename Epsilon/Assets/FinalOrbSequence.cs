using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class FinalOrbSequence : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerStateMachine player;

    [SerializeField] GameObject gunk1;
    [SerializeField] GameObject helperLegs;

    [Header("Screenshake")]
    CinemachineImpulseSource impulse;
    [SerializeField] float force;
    public bool isScreenshaking = false;

    public float waitTime = 5f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        impulse = FindObjectOfType<CinemachineImpulseSource>();
    }

    public void StartCoroutineSequence()
    {
        StartCoroutine(FinalOrbSequenceCo());
    }

    private void Update()
    {
        if (isScreenshaking) impulse.GenerateImpulse(force);
    }

    public IEnumerator FinalOrbSequenceCo()
    {
        yield return new WaitForSeconds(7.25f);

        player.inCinematic = true;
        player.DisableGameplayControls();

        LevelMusicManager levelMusicManager = FindObjectOfType<LevelMusicManager>();
        levelMusicManager.isFadingMusicOut = true;

        levelMusicManager.ominousMusic.volume = 0f;
        levelMusicManager.ominousMusic.Play();
        levelMusicManager.isFadingOminousMusicIn = true;

        FindObjectOfType<Letterbox>().MoveIn();

        yield return new WaitForSeconds(5f);

        //focus camera 1
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        cameraManager.FocusClonesCam();

        yield return new WaitForSeconds(4f);

        // focus cam 2
        cameraManager.FocusClonesCam2();

        yield return new WaitForSeconds(10f);

        // reset clone cams
        cameraManager.ResetClonesCam();
        cameraManager.ResetClonesCam2();

        yield return new WaitForSeconds(3f);

        //Die
        anim.Play("Player_ScriptedDeath");
        isScreenshaking = true;

        AudioManager audioManager = FindObjectOfType<AudioManager>();

        audioManager.playerBreathingSFX.Stop();
        audioManager.playerPainBreathingSFX.Play();

        yield return new WaitForSeconds(6.5f);

        isScreenshaking = false;
        levelMusicManager.ominousMusic.Stop();
        audioManager.playerPainBreathingSFX.Stop();
        player.GetComponent<UnityEngine.U2D.Animation.SpriteResolver>().SetCategoryAndLabel("PlayerV3", "PlayerCrackedHelm");

        yield return new WaitForSeconds(0.5f);

        //gunk1.SetActive(true);
        //helperLegs.SetActive(true);

        yield return new WaitForSeconds(5f);

        FindObjectOfType<ScreenFadeManager>().TurnOnAnimatorAndFadeOut();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MainMenu");
    }
}
