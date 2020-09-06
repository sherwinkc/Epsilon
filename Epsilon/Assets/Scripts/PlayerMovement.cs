using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    //Components
    public Rigidbody2D rb;
    public Animator animator;

    //Controller Movement
    PlayerControls controls;
    public Vector2 move;    
    public Vector2 jetpack;

    //checks    
    public float groundCheckRadius;
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

    //Movement
    public float speed;
    public float maxSpeed;
    public float walkAccel;
    public float walkDeccel;
    public float flySpeed;

    public float ledgeXSpeed;
    public float ledgeYSpeed;

    public float jumpSpeed;
    public bool isSlowWalking;
    public bool isNearBox = false;

    //Jetpack
    /*public float jetForce;
    public bool jetIsOn;
    public float boostTime;
    public float flyForce;

    public float airSpeed;
    public float maxAirSpeed;
    public float flyAccel;
    public float flyDeccel;*/

    //Colliders/Tiggers
    public Collider2D slowWalkCollider;

    //Playable Directors
    public PlayableDirector PD_HealthBox;

    //Audio
    public AudioSource playerFootsteps;

    public UI_Interact interactBool;

    private void Awake()
    {
        controls = new PlayerControls();

        //Stick Movement
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        //JetPack
        controls.Gameplay.Jump.performed += ctx => Jump();

        //Interact
        controls.Gameplay.Interact.performed += ctx => Interact();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        flySpeed = maxSpeed * 1.33f;
    }

    void Update()
    {
        //ground check bool
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            animator.SetBool("ledgeDetected", ledgeDetected);
            StartCoroutine(LedgeClimbimgCo());
        }

        PlayerMovementGround();
        RotateSprite();
        CheckLedgeClimb();

        //AIR
        //slightly increase movement when in the air
        /*if(!isGrounded && jetIsOn)
        {
            rb.velocity = new Vector2(move.x * flySpeed, rb.velocity.y);
        }*/

        //ANIMATOR
        //getting the player speed for the animator
        if (isGrounded)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isSlowWalking", isSlowWalking);
    }

    void FixedUpdate()
    {
        /*if(jetIsOn)
        {
            //add force when using jetpack
            rb.AddForce(new Vector2(0f, jetForce), ForceMode2D.Force);
            StartCoroutine(JetPackCo());
        }*/
    }

    /*public IEnumerator JetPackCo()
    {
        animator.SetTrigger("jetPackActive");
        animator.SetBool("isGrounded", false);

        if(isGrounded)
        {
            yield break;
        } else
        {
            yield return new WaitForSeconds(boostTime);
        }
        jetIsOn = false;
    }
    */

    void PlayerMovementGround()
    {
        //GROUND
        //Move the player with new Vector2
        if (isGrounded)
        {
            if (speed > -maxSpeed)
            {
                rb.velocity = new Vector2(move.x * (speed += (walkAccel * Time.deltaTime)), rb.velocity.y); //Slowly increasing the speed value to fake acceleeration
            }

            //setting speed to not excede max speed
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }

            //reset speed to 0 if the player stop moving, so acceleration will kick in again
            if (move.x < 0.1f && move.x > -0.1f && move.y < 0.1f && move.y > -0.1f)
            {
                speed = 0;
            }            
        }

        if (isTouchingWall && isGrounded)
        {
            animator.SetTrigger("wallStop");
            speed = 0;
        }

        //Debug.Log("move.x" + move.x);
    }

    void RotateSprite()
    {
        //rotate sprite when moving left and right
        if (move.x > 0.3)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, transform.localScale.z);
            Debug.DrawRay(wallCheck.position, Vector2.right * wallCheckDistance, Color.white);
            Debug.DrawRay(ledgeCheck.position, Vector2.right * wallCheckDistance, Color.white);

        }

        if (move.x < -0.3)
        {
            transform.localScale = new Vector3(-0.4f, 0.4f, transform.localScale.z);
            Debug.DrawRay(wallCheck.position, Vector2.left * wallCheckDistance, Color.white);
            Debug.DrawRay(ledgeCheck.position, Vector2.left * wallCheckDistance, Color.white);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            animator.SetTrigger("Jump");
        }
    }

    //Player Input 
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void PlayPlayerFootsteps()
    {
        playerFootsteps.pitch = (Random.Range(0.85f, 1f));
        playerFootsteps.Play();
    }

    public void CheckLedgeClimb()
    {

    }

    public IEnumerator LedgeClimbimgCo()
    {
        Debug.Log("Inside Coroutine");

        rb.gravityScale = 0;

        rb.position = new Vector2(rb.position.x, rb.position.y + 0.5f);

        yield return new WaitForSeconds(0.33f);

        rb.position = new Vector2(rb.position.x + 0.6f, rb.position.y);

        yield return new WaitForSeconds(0.2f);

        rb.gravityScale = 0.75f;

        ledgeDetected = false;
        animator.SetBool("ledgeDetected", ledgeDetected);

        yield return null;
    }

    //SLOW WALK FUNCTIONS
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SlowWalk")
        {
            maxSpeed = 0.75f;
            isSlowWalking = true;
        }

        if (other.tag == "HealthBox")
        {
            isNearBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HealthBox")
        {
            //isNearBox = false;
        }
    }

    public void Interact()
    {
        if(isNearBox)
        {
            StartCoroutine(HealthBoxInteraction());
            interactBool.textUI.enabled = false;
        }
    }

    public IEnumerator HealthBoxInteraction()
    {
        PD_HealthBox.Play();

        yield return new WaitForSeconds(15);

        maxSpeed = 4;
        slowWalkCollider.enabled = false;
        isSlowWalking = false;

        yield return null;
    }
}
