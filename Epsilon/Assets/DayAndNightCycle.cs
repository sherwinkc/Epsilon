using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    //TODO maybe make one animator / or timeline? to manage the sequence

    [SerializeField] Animator daySkyImage;
    [SerializeField] Animator globalLight1, globalLight2;
    [SerializeField] Animator sun;
    [SerializeField] Animation sunrise;

    [SerializeField] Animator nightAnim;

    [SerializeField] float cycleLength;
    public bool sunrising, sunsetting;

    private void Awake()
    {
        daySkyImage.enabled = false;
        globalLight1.enabled = false;
        globalLight2.enabled = false;
        sun.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        BeginSunrise(); // Sunrise until cave sequence.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BeginSunset()
    {
        TurnOnComponents();

        Debug.Log("BeginSunsetInvoked");
        daySkyImage.Play("SunSet");
        globalLight1.Play("DimGlobalLight");
        globalLight2.Play("DimGlobalLight1");
        sun.Play("SetSun");
        Invoke("BeginSunrise", cycleLength);

        sunrising = false;
        sunsetting = true;
    }


    private void BeginSunrise()
    {
        TurnOnComponents();

        Debug.Log("BeginSunriseInvoked");

        //Sun
        sun.Play("SunriseToNoon");

        //Background
        daySkyImage.Play("SunriseToNoon");

        //Global Lights
        globalLight1.Play("SunriseToNoon");
        globalLight2.Play("SunriseToNoon(GB)");

        //Invoke("BeginSunset", cycleLength);

        sunrising = true;
        sunsetting = false;
    }

    private void TurnOnComponents()
    {
        daySkyImage.enabled = true;
        globalLight1.enabled = true;
        globalLight2.enabled = true;
        sun.enabled = true;
    }

    private void BeginSunriseAtMidMorning()
    {
        TurnOnComponents();

        //Debug.Log("BeginSunriseInvoked");
        daySkyImage.Play("Sunrise");
        globalLight1.Play("BrightenGlobalLight1");
        globalLight2.Play("BrightenGlobalLight");

        //sunrise["SunSetAndRise"].time = 10f;
        sun.Play("SunSetAndRise");

        Invoke("BeginSunset", cycleLength);

        sunrising = true;
        sunsetting = false;
    }
}
