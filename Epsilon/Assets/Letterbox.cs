using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letterbox : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject blackBarsHolder;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        //blackBarsHolder.SetActive(false);
    }

    void Start()
    {
        //MoveIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveIn()
    {
        if(blackBarsHolder.activeSelf == false) blackBarsHolder.SetActive(true);
        animator.enabled = true;
        animator.SetTrigger("MoveIn");
    }

    public void MoveOut()
    {
        animator.SetTrigger("MoveOut");
    }
}
