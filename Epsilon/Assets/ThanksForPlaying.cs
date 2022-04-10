using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThanksForPlaying : MonoBehaviour
{
    public string levelToLoad;
    public float timeToLoad;

    void Start()
    {
        Invoke("LoadNextScene", timeToLoad);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
