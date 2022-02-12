using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    public AudioSource music1, music2, music3, music4;
    public bool playMusic1, playMusic2, playMusic3, playMusic4;

    private void Awake()
    {

    }

    void Start()
    {
        PlaySelectedMusic();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaySelectedMusic()
    {
        if (playMusic1)
        {
            if (music1 != null) music1.gameObject.SetActive(true);
            if (music2 != null) music2.gameObject.SetActive(false);
            if (music3 != null) music3.gameObject.SetActive(false);
            if (music4 != null) music4.gameObject.SetActive(false);
        }
        else if (playMusic2)
        {
            if (music1 != null) music1.gameObject.SetActive(false);
            if (music2 != null) music2.gameObject.SetActive(true);
            if (music3 != null) music3.gameObject.SetActive(false);
            if (music4 != null) music4.gameObject.SetActive(false);
        }
        else if (playMusic3)
        {
            if (music1 != null) music1.gameObject.SetActive(false);
            if (music2 != null) music2.gameObject.SetActive(false);
            if (music3 != null) music3.gameObject.SetActive(true);
            if (music4 != null) music4.gameObject.SetActive(false);
        }
        else if (playMusic4)
        {
            if (music1 != null) music1.gameObject.SetActive(false);
            if (music2 != null) music2.gameObject.SetActive(false);
            if (music3 != null) music3.gameObject.SetActive(false);
            if (music4 != null) music4.gameObject.SetActive(true);
        }
    }
}
