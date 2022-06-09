using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFarm : MonoBehaviour
{
    Animator animator;

    [SerializeField] float minRandomValue, maxRandomValue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (animator != null) animator.speed = Random.Range(minRandomValue, maxRandomValue);
    }


}
