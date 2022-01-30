using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    [SerializeField] string levelToLoad;
    [SerializeField] float waitTimeToLoadLevel = 25f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene", waitTimeToLoadLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
