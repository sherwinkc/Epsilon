using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ActivateSidePanel()
    {
        anim.enabled = true;
    }
}
