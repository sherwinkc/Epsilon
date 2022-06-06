using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datapad : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] SpriteRenderer spr;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] GameObject textToShow;

    [SerializeField] float showTextDuration;

    [SerializeField] GameObject errandsHUD;

    private void Awake()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        if (textToShow != null) textToShow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            spr.enabled = false;
            if(textToShow != null) textToShow.SetActive(true);

            Invoke("TurnOffText", showTextDuration);

            PlayOpenSFX();
            Invoke("PlayOpenSFX", 10.5f);

            if(errandsHUD != null) Invoke("ShowErrands", showTextDuration);
        }
    }

    private void TurnOffText()
    {
        if (textToShow != null) textToShow.SetActive(false);

        //audioManager.datapadSFXClose.Play();
    }

    void PlayOpenSFX()
    {
        audioManager.datapadSFXOpen.Play();
    }

    private void ShowErrands()
    {
        errandsHUD.SetActive(true);
    }
}
