﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    //Components
    public Animator animator;
    public Rigidbody2D rb;

    //Checks
    public LayerMask whatIsGround;
    public bool isGrounded;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;

    public bool isTouchingWall;
    public bool isTouchingLedge;
    public float wallCheckDistance;

    public bool canClimbLedge = false;
    public bool ledgeDetected = false;

    //Floats
    public float ledgeXSpeed;
    public float ledgeYSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast Checks
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right * (transform.localScale.x * 2.5f), wallCheckDistance, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right * (transform.localScale.x * 2.5f), wallCheckDistance, whatIsGround);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            animator.SetBool("ledgeDetected", ledgeDetected);
            StartCoroutine(LedgeClimbimgCo());
        }

        if (transform.localScale.x < 0)
        {
            ledgeXSpeed = -0.6f;
        }
        else
        {
            ledgeXSpeed = 0.6f;
        }

        //Raycast Debug
        Debug.DrawRay(wallCheck.position, (Vector2.right * wallCheckDistance) * transform.localScale.x * 2.5f, Color.white);
        Debug.DrawRay(ledgeCheck.position, (Vector2.right * wallCheckDistance) * transform.localScale.x * 2.5f, Color.white);
    }

    public IEnumerator LedgeClimbimgCo()
    {
        //Debug.Log("Inside Coroutine");

        rb.gravityScale = 0;

        rb.position = new Vector2(rb.position.x, rb.position.y + 0.5f);

        yield return new WaitForSeconds(0.4f);

        rb.position = new Vector2(rb.position.x + ledgeXSpeed, rb.position.y);

        yield return new WaitForSeconds(0.5f);

        rb.gravityScale = 0.75f;

        ledgeDetected = false;
        animator.SetBool("ledgeDetected", ledgeDetected);

        yield return null;
    }
}
