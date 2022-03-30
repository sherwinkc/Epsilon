using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositManager : MonoBehaviour
{
    public GameObject redLines, greenLines, batterySprite;

    public BoxCollider2D boxCollider;

    private void Awake()
    {
        redLines.SetActive(true);
        greenLines.SetActive(false);
        batterySprite.SetActive(false);

        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            boxCollider.enabled = false;
            redLines.SetActive(false);
            greenLines.SetActive(true);
            batterySprite.SetActive(true);
        }
    }

    public void ChangeDepositState()
    {
        boxCollider.enabled = false;
        redLines.SetActive(false);
        greenLines.SetActive(true);
        batterySprite.SetActive(true);
        //FindObjectOfType<DepositFlash>().StopAllCoroutines(); //TODO Nast horrible find another way of disabling the deposit flash script
        //FindObjectOfType<DepositFlash>().enabled = false; //TODO Nast horrible find another way of disabling the deposit flash script
    }
}
