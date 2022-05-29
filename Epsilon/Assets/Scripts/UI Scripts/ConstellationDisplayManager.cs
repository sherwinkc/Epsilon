using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationDisplayManager : MonoBehaviour
{
    Collector collector;

    public GameObject blackBackground;
    public GameObject constellation0, constellation1, constellation2, constellation3;

    public float displayWaitTime, turnOffDisplayTime;

    private void Awake()
    {
        collector = FindObjectOfType<Collector>();

        blackBackground.SetActive(false);
        constellation0.SetActive(false);
        constellation1.SetActive(false);
        constellation2.SetActive(false);
        constellation3.SetActive(false);
    }

    private void OnEnable()
    {
        if (collector.orbs == 1)
        {
            SwitchOnConstellation1();
        }
        else if (collector.orbs == 2)
        {
            SwitchOnConstellation2();
        }
        else if (collector.orbs == 3)
        {
            SwitchOnConstellation3();
        }
    }

    public void SwitchOnConstellation1()
    {
        blackBackground.SetActive(true);
        constellation0.SetActive(true);
        
        StartCoroutine(SwitchOutConstellation1());
    }

    public IEnumerator SwitchOutConstellation1()
    {
        yield return new WaitForSeconds(displayWaitTime);

        constellation0.SetActive(false);
        constellation1.SetActive(true);

        FindObjectOfType<AudioManager>().PlayCollectSFX();

        yield return new WaitForSeconds(turnOffDisplayTime);

        blackBackground.SetActive(false);
        constellation1.SetActive(false);
    }

    private void SwitchOnConstellation2()
    {
        blackBackground.SetActive(true);
        constellation1.SetActive(true);

        StartCoroutine(SwitchOutConstellation2());
    }

    public IEnumerator SwitchOutConstellation2()
    {
        yield return new WaitForSeconds(displayWaitTime);

        constellation1.SetActive(false);
        constellation2.SetActive(true);

        FindObjectOfType<AudioManager>().PlayCollectSFX();

        yield return new WaitForSeconds(turnOffDisplayTime);

        blackBackground.SetActive(false);
        constellation2.SetActive(false);
    }

    private void SwitchOnConstellation3()
    {
        blackBackground.SetActive(true);
        constellation2.SetActive(true);

        StartCoroutine(SwitchOutConstellation3());
    }

    public IEnumerator SwitchOutConstellation3()
    {
        yield return new WaitForSeconds(displayWaitTime);

        constellation2.SetActive(false);
        constellation3.SetActive(true);

        FindObjectOfType<AudioManager>().PlayCollectSFX();

        yield return new WaitForSeconds(turnOffDisplayTime);

        blackBackground.SetActive(false);
        constellation3.SetActive(false);
    }
}
