using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    ScreenFadeManager screenFadeManager;
    Animator anim;
    [SerializeField] float timeToLoadGame = 0.25f;
    [SerializeField] GameObject button;
    [SerializeField] Button defaultButton;

    public string levelToLoad;

    public AudioSource menuMusic;
    public AudioSource highlightUI;
    public AudioSource selectUI;

    GameObject currentlySelected, previouslySelected;

    [SerializeField] GameObject selectedIcon1;
    [SerializeField] GameObject selectedIcon2;
    [SerializeField] GameObject selectedIcon3;

    [SerializeField] GameObject selectedline1;
    [SerializeField] GameObject selectedline2;
    [SerializeField] GameObject selectedline3;

    public bool hasAnim1played = false;

    private void Awake()
    {
        screenFadeManager = FindObjectOfType<ScreenFadeManager>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        menuMusic.Play();

        Cursor.visible = true;

        selectedIcon1.SetActive(true);
        selectedline1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);

        if (EventSystem.current.currentSelectedGameObject.name == "Button Start Game")
        {
            selectedIcon1.SetActive(true);
            selectedline1.SetActive(true);
        }
        else
        {
            selectedIcon1.SetActive(false);
            selectedline1.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Button Controls")
        {
            selectedIcon2.SetActive(true);
            selectedline2.SetActive(true);
        }
        else
        {
            selectedIcon2.SetActive(false);
            selectedline2.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Button Quit")
        {
            selectedIcon3.SetActive(true);
            selectedline3.SetActive(true);
        }
        else
        {
            selectedIcon3.SetActive(false);
            selectedline3.SetActive(false);
        }

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
        if(screenFadeManager != null) screenFadeManager.TurnOnAnimatorAndFadeOut();
        selectUI.Play();

        yield return new WaitForSeconds(timeToLoadGame);

        menuMusic.Stop();
        SceneManager.LoadScene(levelToLoad);
    }
}
