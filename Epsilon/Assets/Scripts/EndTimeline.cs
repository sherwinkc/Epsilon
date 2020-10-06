using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndTimeline : MonoBehaviour
{
    public PlayerMovement playerMov;
    public PlayableDirector cloneTimeline;
    public AudioSource endMusic, levelMusic;

    public bool musicFadeOut;

    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        playerMov = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicFadeOut == true)
        {
            levelMusic.volume -= Time.deltaTime / 3;
        }

        if (levelMusic.volume <= 0)
        {
            levelMusic.volume = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            musicFadeOut = true;
            endMusic.Play();

            playerMov.canMove = false;
            cloneTimeline.Play();
        }
    }

    public void NextRoom()
    {
        endMusic.Stop();
        SceneManager.LoadScene(levelToLoad);
    }
}
