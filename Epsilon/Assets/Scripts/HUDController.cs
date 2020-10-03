using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject repairSuit;
    public GameObject airBar;
    public GameObject jetpackOffline;
    public GameObject jetpackBar;
    public GameObject airCanister;
    public GameObject repairJetpack;
    public GameObject findEpsilon;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstUI());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FirstUI()
    {
        yield return new WaitForSeconds(2f);

        repairSuit.SetActive(enabled);
        airBar.SetActive(enabled);
        jetpackOffline.SetActive(enabled);

        yield return null;
    }

    public void SuitRepaired()
    {
        repairSuit.SetActive(false);
        airBar.SetActive(false);
        //jetpackOffline.SetActive(false);
        StartCoroutine(RefillCo());
    }

    public IEnumerator RefillCo()
    {
        Debug.Log("Co Called");
        yield return new WaitForSeconds(5f);

        airCanister.SetActive(enabled);
        airBar.SetActive(enabled);
        jetpackOffline.SetActive(enabled);

        yield return null;
    }
    public void AirFound()
    {
        airCanister.SetActive(false);
        repairJetpack.SetActive(enabled);

    }

}
