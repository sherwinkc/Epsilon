using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public AudioSource menuMusic;

    GameObject currentSelected, previouslySelected;

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
            menuMusic.Stop();
            SceneManager.LoadScene(levelToLoad);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuMusic.Stop();
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
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
            }
        }

        previouslySelected = currentSelected;
    }

    public void StartGame()
    {
        menuMusic.Stop();
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitButton()
    {
        menuMusic.Stop();
        Application.Quit();
    }
}
