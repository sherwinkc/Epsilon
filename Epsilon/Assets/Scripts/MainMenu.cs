using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ScreenFadeManager screenFadeManager;
    [SerializeField] float timeToLoadGame = 0.25f;

    public string levelToLoad;

    public AudioSource menuMusic;
    public AudioSource highlightUI;
    public AudioSource selectUI;

    GameObject currentSelected, previouslySelected;

    private void Awake()
    {
        screenFadeManager = FindObjectOfType<ScreenFadeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        menuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        HandleButtonScalingWhenSelected();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectUI.Play();
            StartCoroutine(StartGameCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectUI.Play();
            menuMusic.Stop();
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            selectUI.Play();
            menuMusic.Stop();
            SceneManager.LoadScene(levelToLoad);
        }
    }

    private void HandleButtonScalingWhenSelected()
    {
        currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected != null)
        {
            currentSelected.gameObject.transform.localScale = new Vector3(1.05f, 1.2f, 1f);

        }

        if (previouslySelected != null)
        {
            if (currentSelected != previouslySelected)
            {
                previouslySelected.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

                highlightUI.pitch = Random.Range(0.9f, 1f);
                highlightUI.Play();
            }
        }

        previouslySelected = currentSelected;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void QuitButton()
    {
        selectUI.Play();
        menuMusic.Stop();
        Application.Quit();
    }

    private IEnumerator StartGameCoroutine()
    {
        screenFadeManager.TurnOnAnimatorAndFadeOut();
        selectUI.Play();

        yield return new WaitForSeconds(timeToLoadGame);

        menuMusic.Stop();
        SceneManager.LoadScene(levelToLoad);
    }
}
