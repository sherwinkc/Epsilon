using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneFlow : MonoBehaviour
{
    public string levelToLoad;

    public AudioSource ominous, chatter, fly1, fly2, hugeExplosion;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AudioManager());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AudioManager()
    {
        ominous.Play();

        yield return new WaitForSeconds(5);

        fly1.Play();
        fly2.Play();

        yield return new WaitForSeconds(7);

        ominous.Stop();
        chatter.Stop();
        fly1.Stop();
        fly2.Stop();

        hugeExplosion.Play();

        yield return new WaitForSeconds(7);

        SceneManager.LoadScene(levelToLoad);

        yield return null;
    }
}
