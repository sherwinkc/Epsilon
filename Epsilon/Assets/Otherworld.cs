using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otherworld : MonoBehaviour
{
    [SerializeField] GameObject otherworld;
    [SerializeField] GameObject helperLegs;
    [SerializeField] float displayTime = 0.01f;
    [SerializeField] float repeatTime = 5f;

    private void Awake()
    {
        otherworld.SetActive(false);
        helperLegs.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("StartCo", repeatTime, repeatTime);    
    }

    public void StartCo()
    {
        otherworld.SetActive(true);
        helperLegs.SetActive(true);
        StartCoroutine(DisplayOtherworld());
    }

    public IEnumerator DisplayOtherworld()
    {
        yield return new WaitForSeconds(displayTime);

        otherworld.SetActive(false);
        helperLegs.SetActive(false);
    }
}
