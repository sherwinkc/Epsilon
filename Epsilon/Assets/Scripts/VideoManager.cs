using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    [SerializeField] string levelToLoad;
    [SerializeField] float waitTimeToLoadLevel = 25f;

    ScreenFadeManager screenFadeManager;
    [SerializeField] float timeToLoadGame = 0.25f;

    private void Awake()
    {
        screenFadeManager = FindObjectOfType<ScreenFadeManager>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene", waitTimeToLoadLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(StartGameCoroutine());
        }
    }

    void LoadNextScene()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        screenFadeManager.TurnOnAnimatorAndFadeOut();

        yield return new WaitForSeconds(timeToLoadGame);

        AudioSource levelMusic = FindObjectOfType<AudioSource>();
        if (levelMusic != null) levelMusic.Stop();
        SceneManager.LoadScene(levelToLoad);
    }
}
