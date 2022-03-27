using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;

    [SerializeField] float destroyTime = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        //rb.simulated = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
            //rb.simulated = true;
            rb.constraints = RigidbodyConstraints2D.None;
            Invoke("DestroyThis", destroyTime);
        }
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
