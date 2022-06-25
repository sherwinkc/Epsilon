using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraSequence : MonoBehaviour
{
    CameraManager cameraManager;
    PlayerStateMachine playerStateMachine;

    [SerializeField] BoxCollider2D trigger;
    [SerializeField] float holdTime = 16f;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();

        trigger = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with player");
            trigger.enabled = false;

            StartCoroutine(StartMiraSequence());            
        }
    }

    public IEnumerator StartMiraSequence()
    {
        LevelMusicManager levelMusicManager = FindObjectOfType<LevelMusicManager>();

        levelMusicManager.ominousMusic.volume = 0f;
        levelMusicManager.ominousMusic.Play();
        levelMusicManager.isFadingOminousMusicIn = true;

        FindObjectOfType<Letterbox>().MoveIn();

        cameraManager.FocusMiraCam();
        playerStateMachine.inCinematic = true;

        yield return new WaitForSeconds(holdTime);

        cameraManager.ResetMiraCam();
        playerStateMachine.inCinematic = false;

        levelMusicManager.isFadingOminousMusicIn = false;
        levelMusicManager.isFadingOminousMusicOut = true;

        levelMusicManager.music.volume = 0f;
        levelMusicManager.music.Play();
        levelMusicManager.isFadingMusicIn = true;

        FindObjectOfType<Letterbox>().MoveOut();
    }
}
