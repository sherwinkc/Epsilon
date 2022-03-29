using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class EquipJetpack : MonoBehaviour
{
    [SerializeField] GameObject thrustHolder;
    [SerializeField] SpriteRenderer sprRend;

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
        }
    }
}
