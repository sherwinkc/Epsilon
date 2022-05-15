using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    public string levelToLoad;
    public float loadTime = 2f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateBlackScreen", loadTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ActivateBlackScreen()
    {
        ScreenFadeManager screenFadeManager = FindObjectOfType<ScreenFadeManager>();
        if (screenFadeManager != null) screenFadeManager.TurnOnAnimatorAndFadeOut();

        Invoke("LoadNextScene", 0.26f);
    }


}
