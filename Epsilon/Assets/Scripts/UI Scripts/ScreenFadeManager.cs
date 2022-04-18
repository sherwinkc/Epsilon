using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeManager : MonoBehaviour
{
    Animator animator;

    //[SerializeField] bool turnOffCompletelyWhenNotRun = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator != null) animator.enabled = true;
    }

    public void TurnOnAnimatorAndFadeOut()
    {
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
    }

    public void FadeIn()
    {
        animator.Play("BlackScreenFadeIn");
    }
}
