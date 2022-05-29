using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    Animator animator;

    public bool isThisButtonActive = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivateButton();
            isThisButtonActive = true;
        }
    }

    public void ResetButton()
    {
        animator.Play("ButtonDefault");
    }

    public void ActivateButton()
    {
        if(!isThisButtonActive) animator.SetTrigger("Activate");
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("ButtonDefault"))
    }
}
