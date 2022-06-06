using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsTower : MonoBehaviour
{
    [SerializeField] GameObject lightAndSFX, light2;

    public void TurnOnLight()
    {
        if (lightAndSFX != null || light2 != null)
        {
            lightAndSFX.SetActive(true);
            light2.SetActive(true);
        }
    }
}
