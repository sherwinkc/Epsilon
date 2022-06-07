using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryRecharger : MonoBehaviour
{    
    [SerializeField] Animator door;
    [SerializeField] GameObject battery1, battery2, battery3;

    [SerializeField] GameObject battery1LightRed, battery2LightRed, battery3LightRed;
    [SerializeField] GameObject battery1LightAmber, battery2LightAmber, battery3LightAmber;
    [SerializeField] GameObject battery1LightGreen;

    [SerializeField] GameObject realBattery;

    Animator anim;

    [SerializeField] BoxCollider2D boxCollider;


    public int batteriesDocked = 0;

    public bool collectKeys = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;

        boxCollider = GetComponent<BoxCollider2D>();

        ActivateBatterySprites();
        realBattery.SetActive(false);

        SetAllLightsToRed();
    }

    private void Update()
    {
        if (collectKeys) ActivateBatterySprites();
    }

    public void ActivateBatterySprites()
    {
        if (batteriesDocked == 0)
        {
            battery1.SetActive(false);
            battery2.SetActive(false);
            battery3.SetActive(false);

            SetAllLightsToRed();
        }
        else if (batteriesDocked == 1)
        {
            battery1.SetActive(true);
            battery2.SetActive(false);
            battery3.SetActive(false);

            battery1LightRed.SetActive(false);
            battery1LightAmber.SetActive(true);
        }
        else if (batteriesDocked == 2)
        {
            battery1.SetActive(true);
            battery2.SetActive(true);
            battery3.SetActive(false);

            battery2LightRed.SetActive(false);
            battery2LightAmber.SetActive(true);
        }
        else if (batteriesDocked == 3)
        {
            battery1.SetActive(true);
            battery2.SetActive(true);
            battery3.SetActive(true);

            battery3LightRed.SetActive(false);
            battery3LightAmber.SetActive(true);

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.areBatteriesCollected = true;

            DisableTrigger();

            levelManager.CheckLevelIntroStatus();
        }
    }

    public void DischargeBatteryAnim()
    {
        anim.enabled = true;
    }

    public void SwapBatteryToRealOne()
    {
        battery1.SetActive(false);
        realBattery.SetActive(true);
    }

    public void DisableTrigger()
    {
        boxCollider.enabled = false;
    }

    public void SetAllLightsToRed()
    {
        battery1LightRed.SetActive(true);
        battery2LightRed.SetActive(true);
        battery3LightRed.SetActive(true);
    }

    public void SetGreenLight()
    {
        battery1LightAmber.SetActive(false);
        battery1LightGreen.SetActive(true);
    }
}
