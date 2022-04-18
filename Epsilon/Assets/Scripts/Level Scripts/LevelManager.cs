using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    PauseMenu pauseMenu;

    public float timeBeforeFade;
    public float timeBeforeSceneLoad;

    public bool cursorOn = false;
    public string levelToLoad;

    private void Awake()
    {
        pauseMenu = GetComponent<PauseMenu>();
    }

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

        pauseMenu.playerStateMachine.FadeScreen();

        yield return new WaitForSeconds(timeBeforeSceneLoad);

        SceneManager.LoadScene(levelToLoad);
    }

    public void RespawnFromCheckpoint()
    {
        Time.timeScale = 1f;
        pauseMenu.pauseMenuCanvas.gameObject.SetActive(false);
        pauseMenu.playerStateMachine.Respawn();
        pauseMenu.playerStateMachine.EnterIdleState();
        pauseMenu.playerStateMachine.EnableGameplayControls();
    }
}
