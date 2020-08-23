using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    //Movement
    public float speed;
    public float maxSpeed;
    public float walkAccel;
    public float walkDeccel;
    public float flySpeed;

    //Jetpack
    public float jetForce;
    public bool jetIsOn;
    public float boostTime;
    public float flyForce;

    public float airSpeed;
    public float maxAirSpeed;
    public float flyAccel;
    public float flyDeccel;

    private void Awake()
    {
        controls = new PlayerControls();

        //Stick Movement
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        //JetPack
        controls.Gameplay.Jetpack.performed += ctx => JumpJet();
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
            if(move.x < 0.1f && move.x > -0.1f && move.y < 0.1f && move.y > -0.1f)
            {
                speed = 0;
            }
        }

        //AIR
        //slightly increase movement when in the air
        if(!isGrounded && jetIsOn)
        {
            rb.velocity = new Vector2(move.x * flySpeed, rb.velocity.y);
        }

        //rotate sprite when moving left and right
        if (move.x > 0.3)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, transform.localScale.z);
        }

        if (move.x < -0.3)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, transform.localScale.z);
        }

        //ANIMATOR
        //getting the player speed for the animator
        if (isGrounded)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            animator.SetBool("isGrounded", true);
        }
    }

    void FixedUpdate()
    {
        if(jetIsOn)
        {
            //add force when using jetpack
            rb.AddForce(new Vector2(0f, jetForce), ForceMode2D.Force);
            StartCoroutine(JetPackCo());
        }
    }
    public IEnumerator JetPackCo()
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

    void JumpJet()
    {
        jetIsOn = true;  
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
