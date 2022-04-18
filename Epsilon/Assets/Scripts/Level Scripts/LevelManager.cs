using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float timeBeforeFade;
    public float timeBeforeSceneLoad;

    public bool cursorOn = false;
    public string levelToLoad;

    void Start()
    {
        if(!cursorOn) Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        StartCoroutine(EndGameCo());
    }

    public IEnumerator EndGameCo()
    {
        yield return new WaitForSeconds(timeBeforeFade);

        FindObjectOfType<PlayerStateMachine>().FadeScreen();

        yield return new WaitForSeconds(timeBeforeSceneLoad);

        SceneManager.LoadScene(levelToLoad);
    }
}
