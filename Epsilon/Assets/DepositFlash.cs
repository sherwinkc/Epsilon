using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositFlash : MonoBehaviour
{
    [SerializeField] GameObject barHolder;

    // Start is called before the first frame update
    void Start()
    {
        barHolder.SetActive(true);
        InvokeRepeating("TurnOnBarHolder", 1f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnOnBarHolder()
    {
        if (barHolder != null)
        {
            barHolder.SetActive(true);
            StartCoroutine(SwitchOffBarHolder());
        }
    }

    public IEnumerator SwitchOffBarHolder()
    {
        yield return new WaitForSeconds(1f);

        if (barHolder != null) barHolder.SetActive(false);
    }
}
