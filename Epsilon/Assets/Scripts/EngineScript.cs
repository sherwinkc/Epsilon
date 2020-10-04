using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineScript : MonoBehaviour
{
    public bool isEngineOn;
    public GameObject blast1;
    public GameObject blast2;

    public AudioSource powerdown;
    public AudioSource engineBlast;
    public AudioSource engineIdle;

    // Start is called before the first frame update
    void Start()
    {
        isEngineOn = false;
        engineIdle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEngineOn)
        {
            StartCoroutine(EngineLoopCo());            
        }
    }

    public IEnumerator EngineLoopCo()
    {
        isEngineOn = true;
        engineBlast.Play();

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        powerdown.Play();

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(enabled);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(enabled);
        blast2.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        blast1.SetActive(false);
        blast2.SetActive(false);

        yield return new WaitForSeconds(5f);

        isEngineOn = false;

        yield return null;
    }
}
