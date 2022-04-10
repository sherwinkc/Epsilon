using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    [SerializeField] Rigidbody2D[] rbs;
    [SerializeField] DestroyOverTime[] destroy;


    private void Awake()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReleaseRocks();
            ActivateDestroyMethod();
            FindObjectOfType<AudioManager>().rockfallSFX.Play();
        }
    }

    private void ActivateDestroyMethod()
    {
        for (int i = 0; i < destroy.Length; i++)
        {
            destroy[i].Destroy();
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //rb.simulated = true;
            rb.constraints = RigidbodyConstraints2D.None;
            Invoke("DestroyThis", destroyTime);
        }
    }*/

    void ReleaseRocks()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].constraints = RigidbodyConstraints2D.None;
        }
    }
}
