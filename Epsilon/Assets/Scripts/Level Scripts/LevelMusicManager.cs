using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource ominousMusic;

    public float maxVolume;
    [Tooltip("Higher values result in faster volume fade")]
    public float decreaseVolumeRate = 0.05f;
    [Tooltip("Higher values result in faster volume fade")]
    public float increaseVolumeRate = 0.1f;

    public bool randomiseMusic, playMusic1, playMusic2, playMusic3, playMusic4;

    public bool isFadingMusicIn = false;
    public bool isFadingMusicOut = false;
    public bool isFadingOminousMusicIn = false;
    public bool isFadingOminousMusicOut = false;

    void Start()
    {
        music.volume = 0f;
        music.Play();
        isFadingMusicIn = true;
    }

    private void Update()
    {
        if (isFadingMusicIn) FadeMusicIn(music, 0.25f);
        if (isFadingMusicOut) FadeMusicOut(music, 0.25f);
        if (isFadingOminousMusicIn) FadeMusicIn(ominousMusic, 0.4f);
        if (isFadingOminousMusicOut) FadeMusicOut(ominousMusic, 0.4f);
    }

    private void PlaySelectedMusic()
    {
        /*if (playMusic1)
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
        else if (randomiseMusic)
        {
            int var = Random.Range(1, 5);
            //Debug.Log(var);

            if (var == 1) music1.gameObject.SetActive(true);
            if (var == 2) music2.gameObject.SetActive(true);
            if (var == 3) music3.gameObject.SetActive(true);
            if (var == 4) music4.gameObject.SetActive(true);
        }*/
    }

    public void FadeMusicIn(AudioSource audioSource, float maxVolume)
    {
        audioSource.volume += Time.deltaTime * increaseVolumeRate;

        if (audioSource.volume >= maxVolume)
        {
            audioSource.volume = maxVolume;
            isFadingMusicIn = false;
            isFadingOminousMusicIn = false;
        }
    }

    public void FadeMusicOut(AudioSource audioSource, float maxVolume)
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime * decreaseVolumeRate;
        }

        if (audioSource.volume <= 0)
        {
            audioSource.volume = 0f;
            isFadingMusicOut = false;
            isFadingOminousMusicOut = false;
        }
    }
}
