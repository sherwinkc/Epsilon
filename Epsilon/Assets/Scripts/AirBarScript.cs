using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirBarScript : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxAir(float air)
    {
        slider.maxValue = air;
        slider.value = air;
    }

    public void setAir(float air)
    {
        slider.value = air;
    }
}
