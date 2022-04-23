using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    ScreenFadeManager screenFadeManager;
    [SerializeField] float timeToLoadGame = 0.25f;
    [SerializeField] GameObject button;
    [SerializeField] Button defaultButton;

    public string levelToLoad;

    public AudioSource menuMusic;
    public AudioSource highlightUI;
    public AudioSource selectUI;

    GameObject currentlySelected, previouslySelected;

    private void Awake()
    {
        screenFadeManager = FindObjectOfType<ScreenFadeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        menuMusic.Play();

        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //HandleButtonScalingWhenSelected();

        /*if (Input.GetKeyDown(KeyCode.Return))
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
        }*/
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        //Debug.Log(button);

        if (EventSystem.current.currentSelectedGameObject != button)
        {
            highlightUI.Play();
            button = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            defaultButton.Select();
        }
    }

    private void HandleButtonScalingWhenSelected()
    {
        currentlySelected = EventSystem.current.currentSelectedGameObject;

        if (currentlySelected != null)
        {
            currentlySelected.gameObject.transform.localScale = new Vector3(1.05f, 1.2f, 1f);

        }

        if (previouslySelected != null)
        {
            if (currentlySelected != previouslySelected)
            {
                previouslySelected.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

                highlightUI.pitch = Random.Range(0.9f, 1f);
                highlightUI.Play();
            }
        }

        previouslySelected = currentlySelected;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void SelectedOptions()
    {

    }

    public void SelectedBack()
    {

    }

    public void SelectedControls()
    {
        SceneManager.LoadScene("ThisGameSupports");
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
