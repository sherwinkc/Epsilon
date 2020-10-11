using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public PlayerMovement playerMov;
    public Rigidbody2D spacemanRB;
    public CapsuleCollider2D spaceManCollider;
    public Collider c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11;


    // Start is called before the first frame update
    void Start()
    {
        spacemanRB = GetComponent<Rigidbody2D>();
        spaceManCollider = GetComponent<CapsuleCollider2D>();
        playerMov = GetComponent<PlayerMovement>();

        c1.isTrigger = true;
        c2.isTrigger = true;
        c3.isTrigger = true;
        c4.isTrigger = true;
        c5.isTrigger = true;
        c6.isTrigger = true;
        c7.isTrigger = true;
        c8.isTrigger = true;
        c9.isTrigger = true;
        c10.isTrigger = true;
        c11.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeRagdollColliders();
        }
    }

    void ChangeRagdollColliders()
    {
        playerMov.canMove = false;
        spaceManCollider.isTrigger = true;
        c1.isTrigger = false;
        c2.isTrigger = false;
        c3.isTrigger = false;
        c4.isTrigger = false;
        c5.isTrigger = false;
        c6.isTrigger = false;
        c7.isTrigger = false;
        c8.isTrigger = false;
        c9.isTrigger = false;
        c10.isTrigger = false;
        c11.isTrigger = false;
    }
}
