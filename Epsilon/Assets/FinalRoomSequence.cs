using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalRoomSequence : MonoBehaviour
{
    CameraManager camManager;
    PlayerStateMachine player;
    LevelMusicManager levelMusicMan;
    ScreenFadeManager screenFadeMan;
    //FindOrbsToolTipManager orbManager;

    public Animator anim;

    public float transitionTime, transitionTime2;

    public string levelToLoad;

    //[SerializeField] GameObject questionCanvas;

    [SerializeField] Transform teleportTo;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        camManager = FindObjectOfType<CameraManager>();
        levelMusicMan = FindObjectOfType<LevelMusicManager>();
        screenFadeMan = FindObjectOfType<ScreenFadeManager>();
        //orbManager = FindObjectOfType<FindOrbsToolTipManager>();

        //questionCanvas.SetActive(false);
    }    
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateFinalRoomSequence()
    {
        screenFadeMan.FadeIn();

        player.EnterCinematicState();

        player.transform.localScale = new Vector3(player.RotationScaleAmount, player.RotationScaleAmount, player.transform.localScale.z);

        player.transform.position = teleportTo.transform.position;

        anim.Play("Player_LyingDown");

        camManager.endingCamFar.gameObject.SetActive(false);
        camManager.endingCamClose.gameObject.SetActive(true);
        camManager.cam1.enabled = false;

        if (levelMusicMan != null) 
        {
            levelMusicMan.music4.Stop();
            levelMusicMan.music5.Play();
        } 

        StartCoroutine(CameraLogic());        
    }

    public IEnumerator CameraLogic()
    {
        yield return new WaitForSeconds(transitionTime);

        camManager.endingCamClose.gameObject.SetActive(false);
        camManager.endingCamFar.gameObject.SetActive(true);

        yield return new WaitForSeconds(transitionTime2);
        
        screenFadeMan.TurnOnAnimatorAndFadeOut();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(levelToLoad);
    }
}
