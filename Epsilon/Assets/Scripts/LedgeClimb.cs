using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    //Components
    public PlayerMovement playerMov;
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

    public float playerLocalScaleOffset = 3f; // should equal 1 divided player scale reduction

    public bool isTouchingWall;
    public bool isTouchingLedge;
    public float wallCheckDistance;

    public bool canClimbLedge = false;
    public bool ledgeDetected = false;
    public bool canDetectLedges = true;
    public bool isHanging = false;

    //Floats
    //public float ledgeXSpeed;
    //public float ledgeYSpeed;
    [SerializeField] float ledgeTeleportOffsetX;
    [SerializeField] float ledgeTeleportOffsetY;

    public Transform grabPoint;

    public bool isClimbing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerMov = GetComponent<PlayerMovement>();
    }

    void Start()
    {

    }

    void Update()
    {
        ShootRaycasts();

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected && playerMov.canMove)
        {
            ledgeDetected = true;
            animator.SetBool("ledgeDetected", ledgeDetected);
            //StartCoroutine(LedgeClimbimgCo());
            if (canDetectLedges) LedgeHang();

            isClimbing = true;
        }

       /* if (transform.localScale.x < 0)
        {
            ledgeXSpeed = -1f;
        }
        else
        {
            ledgeXSpeed = 1f;
        }*/

        if (Input.GetKeyDown(KeyCode.S) && ledgeDetected)
        {
            LedgeLetGo();
        }

        RaycastDebug();

        if (playerMov.isGrounded)
        {
            canDetectLedges = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && isHanging)
        {
            animator.SetTrigger("Climb Up");
        }
    }


    private void ShootRaycasts()
    {
        if (canDetectLedges)
        {
            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right * (transform.localScale.x * playerLocalScaleOffset), wallCheckDistance, whatIsGround);
            isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right * (transform.localScale.x * playerLocalScaleOffset), wallCheckDistance, whatIsGround);
        }
    }
    private void RaycastDebug()
    {
        Debug.DrawRay(wallCheck.position, (Vector2.right * wallCheckDistance) * transform.localScale.x * playerLocalScaleOffset, Color.white);
        Debug.DrawRay(ledgeCheck.position, (Vector2.right * wallCheckDistance) * transform.localScale.x * playerLocalScaleOffset, Color.white);
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

        //rb.position = new Vector2(rb.position.x + ledgeXSpeed, rb.position.y);

        //rb.velocity = new Vector2(rb.velocity.x + ledgeXSpeed, rb.velocity.y);

        yield return new WaitForSeconds(0.5f);

        rb.gravityScale = 0.75f;

        ledgeDetected = false;
        animator.SetBool("ledgeDetected", ledgeDetected);

        isClimbing = false;

        yield return null;
    }

    public void LedgeHang()
    {
        //rb.gravityScale = 0;
        //rb.velocity = Vector2.zero;
        rb.simulated = false;
        playerMov.transform.position = grabPoint.transform.position;
        canDetectLedges = false;
        isHanging = true;
    }

    public void LedgeLetGo()
    {
        ledgeDetected = false;
        rb.simulated = true;
        animator.SetBool("ledgeDetected", ledgeDetected);
    }

    public void TeleportPlayerAfterLedgeClimb()
    {
        
        playerMov.transform.position = new Vector3(playerMov.transform.position.x + ledgeTeleportOffsetX, playerMov.transform.position.y + ledgeTeleportOffsetY);
        ledgeDetected = false;
        rb.simulated = true;
        //rb.position = new Vector2(rb.position.x + ledgeTeleportOffsetX, rb.position.y + ledgeTeleportOffsetY);
        animator.SetBool("ledgeDetected", ledgeDetected);
    }
}
