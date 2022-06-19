using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letterbox : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject blackBarsHolder;

    [SerializeField] bool playOnAwake;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (playOnAwake)
        {
            blackBarsHolder.SetActive(true);
            animator.enabled = true;
            MoveIn();
        }
        else
        {
            blackBarsHolder.SetActive(false);
        }
    }


    void Update()
    {
        
    }

    public void MoveIn()
    {
        if (blackBarsHolder.activeSelf == false) blackBarsHolder.SetActive(true);
        if (animator.enabled == false) animator.enabled = true;
        animator.Play("MoveIn");
    }

    public void MoveOut()
    {

        animator.Play("MoveOut");
    }
}
