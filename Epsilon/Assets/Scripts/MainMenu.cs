using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public AudioSource menuMusic;

    // Start is called before the first frame update
    void Start()
    {
        menuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            menuMusic.Stop();
            SceneManager.LoadScene(levelToLoad);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuMusic.Stop();
            Application.Quit();
        }
    }

    public void PressEnter()
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
