using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalOrbSequence : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerStateMachine player;

    [SerializeField] GameObject gunk1;
    [SerializeField] GameObject helperLegs;
    
    public float waitTime = 5f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>();
    }

    public void StartCoroutineSequence()
    {
        StartCoroutine(FinalOrbSequenceCo());
    }

    public IEnumerator FinalOrbSequenceCo()
    {
        yield return new WaitForSeconds(7.25f);

        player.inCinematic = true;
        player.DisableGameplayControls();

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

        yield return new WaitForSeconds(6.5f);

        //gunk1.SetActive(true);
        //helperLegs.SetActive(true);

        yield return new WaitForSeconds(5f);

        FindObjectOfType<ScreenFadeManager>().TurnOnAnimatorAndFadeOut();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MainMenu");
    }
}
