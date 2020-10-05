using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FactoryTimeline : MonoBehaviour
{
    public PlayerMovement playerMov;
    //public Player player;
    //public JetPack jetPack;

    //public LevelManager levelManager;

    public PlayableDirector cloneTimeline;

    public AudioSource endMusic, levelMusic;

    public bool musicFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        playerMov = FindObjectOfType<PlayerMovement>();
        //player = FindObjectOfType<Player>();
        //jetPack = FindObjectOfType<JetPack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(musicFadeOut == true)
        {
            levelMusic.volume -= Time.deltaTime / 3;
        }

        if(levelMusic.volume <= 0)
        {
            levelMusic.volume = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
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
        SceneManager.LoadScene("Testbed");
    }
}
