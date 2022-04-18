using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenFadeOut : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator != null) animator.enabled = true;
    }

    public void TurnOffBlackScreenFadeOut()
    {
        if(animator != null) animator.enabled = false;
    }
}
