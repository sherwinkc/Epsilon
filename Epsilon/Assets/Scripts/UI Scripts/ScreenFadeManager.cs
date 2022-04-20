using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeManager : MonoBehaviour
{
    Animator animator;
    [SerializeField] Image img;

    //[SerializeField] bool turnOffCompletelyWhenNotRun = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator != null) animator.enabled = true;

        img = GetComponent<Image>();
    }

    public void TurnOnAnimatorAndFadeOut()
    {
        TurnOnBlackImage();

        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("BlackScreenFadeOut");
        }
    }

    public void TurnOffAnimator()
    {
        if (animator != null) animator.enabled = false;
        //if (turnOffCompletelyWhenNotRun) this.gameObject.SetActive(false);

        Invoke("TurnOffBlackImage", 0.26f);
    }

    public void FadeIn()
    {
        animator.Play("BlackScreenFadeIn");
    }

    public void TurnOffBlackImage()
    {
        if (img != null) img.enabled = false;
    }

    public void TurnOnBlackImage()
    {
        if (img != null) img.enabled = true;
    }
}

