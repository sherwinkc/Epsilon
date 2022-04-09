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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isThisButtonActive = true;
            animator.SetTrigger("Activate");
        }
    }

    public void ResetButton()
    {
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("ButtonDefault")) animator.SetTrigger("Reset");
        animator.Play("ButtonDefault");
    }
}
