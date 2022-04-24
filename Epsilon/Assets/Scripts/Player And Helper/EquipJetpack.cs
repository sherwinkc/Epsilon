using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.UI;

public class EquipJetpack : MonoBehaviour
{
    AudioManager audioManager;

    public GameObject thrustHolder;
    [SerializeField] SpriteRenderer sprRend;

    [SerializeField] GameObject jetpackTooltip;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        jetpackTooltip.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;

            if (collision != null) 
            {
                collision.gameObject.GetComponent<PlayerStateMachine>().isJetpackOn = true;
                collision.gameObject.GetComponent<SpriteResolver>().SetCategoryAndLabel("Player", "JetpackOn"); //TODO strings bad slow   
            } 

            if (thrustHolder != null)
            {
                thrustHolder.SetActive(true);
            }

            if(sprRend != null) sprRend.enabled = false;

            audioManager.bootUpSFX.Play();
            audioManager.helperCollectSFX.Play();
        }

        if(jetpackTooltip != null) jetpackTooltip.SetActive(true);
    }
}
