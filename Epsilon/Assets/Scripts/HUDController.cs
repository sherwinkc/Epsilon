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

    public JetPack jetpackScript;

    // Start is called before the first frame update
    void Start()
    {
        jetpackScript = GetComponent<JetPack>();
        jetpackScript.enabled = true; // change this to false if you want to play the level
        //StartCoroutine(FirstUI());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FirstUI()
    {
        yield return new WaitForSeconds(0f);//change this to 18f

        repairSuit.SetActive(enabled);
        airBar.SetActive(enabled);
        //jetpackOffline.SetActive(enabled); // this need to be on to play jetpack offline
        jetpackBar.SetActive(enabled);// turn this off see above

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
        //Debug.Log("Co Called");
        yield return new WaitForSeconds(15f);

        airCanister.SetActive(enabled);
        airBar.SetActive(enabled);
        jetpackOffline.SetActive(enabled);

        yield return null;
    }
    public void AirFound()
    {
        if(airCanister.activeSelf == true)
        { 
            airCanister.SetActive(false);
            repairJetpack.SetActive(enabled);
        }
    }

    public void JetpackRepaired()
    {
        jetpackScript.enabled = true;

        jetpackOffline.SetActive(false);
        repairJetpack.SetActive(false);

        jetpackBar.SetActive(enabled);
        findEpsilon.SetActive(enabled);
    }

}
