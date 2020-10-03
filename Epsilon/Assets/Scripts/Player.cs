using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxAir = 100f;
    public float startingAir = 75f;
    public float currentAir;

    public AirBarScript airBar;

    // Start is called before the first frame update
    void Start()
    {
        currentAir = startingAir;
        airBar.SetMaxAir(maxAir);
    }

    // Update is called once per frame
    void Update()
    {
        DepleteAir(0.005f);
    }

    void DepleteAir(float airBreathed)
    {
        currentAir -= airBreathed;
        airBar.setAir(currentAir);
    }
}
