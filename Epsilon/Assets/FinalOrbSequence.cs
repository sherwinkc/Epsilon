using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalOrbSequence : MonoBehaviour
{
    [SerializeField] GameObject gunk1;
    [SerializeField] GameObject helperLegs;
    
    public float waitTime = 5f;

    public void StartCoroutineSequence()
    {
        StartCoroutine(FinalOrbSequenceCo());
    }

    public IEnumerator FinalOrbSequenceCo()
    {
        yield return new WaitForSeconds(waitTime);

        gunk1.SetActive(true);
        helperLegs.SetActive(true);        
    }
}
