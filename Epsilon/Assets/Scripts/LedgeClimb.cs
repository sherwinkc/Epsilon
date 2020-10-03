using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    //Components
    public Animator animator;
    public Rigidbody2D rb;
    public Transform playerTransform;
    //public Transform climbMarker;

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
            ledgeXSpeed = -1f;
        }
        else
        {
            ledgeXSpeed = 1f;
        }

        //Raycast Debug
        Debug.DrawRay(wallCheck.position, (Vector2.right * wallCheckDistance) * transform.localScale.x * 2.5f, Color.white);
        Debug.DrawRay(ledgeCheck.position, (Vector2.right * wallCheckDistance) * transform.localScale.x * 2.5f, Color.white);
    }

    public IEnumerator LedgeClimbimgCo()
    {
        //Debug.Log("Inside Coroutine");

        rb.gravityScale = 0;

        //playerTransform.transform.position = climbMarker.transform.position;

        rb.velocity = Vector2.zero;

        rb.position = new Vector2(rb.position.x, rb.position.y + 0.5f);

        //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + ledgeYSpeed);

        yield return new WaitForSeconds(0.5f);

        rb.position = new Vector2(rb.position.x + ledgeXSpeed, rb.position.y);

        //rb.velocity = new Vector2(rb.velocity.x + ledgeXSpeed, rb.velocity.y);

        yield return new WaitForSeconds(0.5f);

        rb.gravityScale = 0.75f;

        ledgeDetected = false;
        animator.SetBool("ledgeDetected", ledgeDetected);

        yield return null;
    }
}
