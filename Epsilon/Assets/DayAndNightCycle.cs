using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayAndNightCycle : MonoBehaviour
{
    //TODO maybe make one animator / or timeline? to manage the sequence

    [Header("Sun")]
    [SerializeField] Animator sunAnimator;

    [Header("Sky")]
    [SerializeField] Animator daySkyImageAnimator;
    [SerializeField] SpriteRenderer dayImageSpriteRend;
    [SerializeField] float dayImageOpacity = 0.15f;

    [Header("Lighting")]
    [SerializeField] Animator globalLight1Anim;
    [SerializeField] Light2D globalLight1;
    [SerializeField] Animator globalLight2Anim;
    [SerializeField] Light2D globalLight2;
    [SerializeField] float lightIntensityAtNight = 0.25f;


    [Header("Transforms")]
    [SerializeField] Transform sunStartingPos;

    [Header("Variables")]
    [SerializeField] float cycleLength;
    public bool sunrising, sunsetting;

    private void Awake()
    {
        daySkyImageAnimator.enabled = false;
        globalLight1Anim.enabled = false;
        globalLight2Anim.enabled = false;
        sunAnimator.enabled = false;
    }

    void Start()
    {
        BeginSunrise(); // Sunrise until cave sequence.
    }

    private void BeginSunset()
    {
        TurnOnComponents();

        Debug.Log("BeginSunsetInvoked");
        daySkyImageAnimator.Play("SunSet");
        globalLight1Anim.Play("DimGlobalLight");
        globalLight2Anim.Play("DimGlobalLight1");
        sunAnimator.Play("SetSun");
        Invoke("BeginSunrise", cycleLength);

        sunrising = false;
        sunsetting = true;
    }


    private void BeginSunrise()
    {
        TurnOnComponents();

        Debug.Log("BeginSunriseInvoked");

        //Sun
        sunAnimator.Play("SunriseToNoon");

        //Background
        daySkyImageAnimator.Play("SunriseToNoon");

        //Global Lights
        globalLight1Anim.Play("SunriseToNoon");
        globalLight2Anim.Play("SunriseToNoon(GB)");

        //Invoke("BeginSunset", cycleLength);

        sunrising = true;
        sunsetting = false;
    }

    public void StartNightTime()
    {
        TurnOnComponents();

        Debug.Log("NightTimeInvoked");

        //Sun
        sunAnimator.enabled = false;
        sunAnimator.gameObject.transform.position = sunStartingPos.transform.position;
        //Keep sun at starting position

        //Background
        //daySkyImage.Play("SunriseToNoon");
        daySkyImageAnimator.enabled = false;
        dayImageSpriteRend.color = new Color(1, 1, 1, dayImageOpacity);
        //daySkyImageAnimator.gameObject.SetActive(false);

        //Global Lights
        //globalLight1.Play("SunriseToNoon");
        //globalLight2.Play("SunriseToNoon(GB)");

        globalLight1Anim.enabled = false;
        globalLight2Anim.enabled = false;
        globalLight1.intensity = lightIntensityAtNight;
        globalLight2.intensity = lightIntensityAtNight;

        //set global lights to night time value. 0.2f TBC?

        sunrising = false;
        sunsetting = false;
    }

    private void TurnOnComponents()
    {
        daySkyImageAnimator.enabled = true;
        globalLight1Anim.enabled = true;
        globalLight2Anim.enabled = true;
        sunAnimator.enabled = true;
    }

    private void BeginSunriseAtMidMorning()
    {
        TurnOnComponents();

        //Debug.Log("BeginSunriseInvoked");
        daySkyImageAnimator.Play("Sunrise");
        globalLight1Anim.Play("BrightenGlobalLight1");
        globalLight2Anim.Play("BrightenGlobalLight");

        //sunrise["SunSetAndRise"].time = 10f;
        sunAnimator.Play("SunSetAndRise");

        Invoke("BeginSunset", cycleLength);

        sunrising = true;
        sunsetting = false;
    }
}
