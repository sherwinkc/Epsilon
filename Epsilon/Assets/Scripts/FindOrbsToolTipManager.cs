using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOrbsToolTipManager : MonoBehaviour
{
    public GameObject findOrbsToolTip;

    private void Awake()
    {
        if (findOrbsToolTip != null) findOrbsToolTip.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (findOrbsToolTip != null) findOrbsToolTip.SetActive(true);
        }
    }
}
