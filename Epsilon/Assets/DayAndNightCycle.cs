using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    //TODO maybe make one animator / or timeline? to manage the sequence

    [SerializeField] Animator dayAnim;
    [SerializeField] Animator globalLight1, globalLight2;
    [SerializeField] Animator sun;

    [SerializeField] Animator nightAnim;

    [SerializeField] float cycleLength;
    [SerializeField] bool isNight;

    private void Awake()
    {
        dayAnim.enabled = false;
        globalLight1.enabled = false;
        globalLight2.enabled = false;
        sun.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeginSunset", cycleLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BeginSunset()
    {
        dayAnim.enabled = true;
        globalLight1.enabled = true;
        globalLight2.enabled = true;
        sun.enabled = true;

        Debug.Log("BeginSunsetInvoked");
        dayAnim.Play("SunSet");
        globalLight1.Play("DimGlobalLight");
        globalLight2.Play("DimGlobalLight1");
        sun.Play("SetSun");
        Invoke("BeginSunrise", cycleLength);
        isNight = true;
    }

    private void BeginSunrise()
    {
        Debug.Log("BeginSunriseInvoked");
        dayAnim.Play("Sunrise");
        globalLight1.Play("BrightenGlobalLight1");
        globalLight2.Play("BrightenGlobalLight");
        sun.Play("RiseSun");
        Invoke("BeginSunset", cycleLength);
        isNight = false;
    }

}
