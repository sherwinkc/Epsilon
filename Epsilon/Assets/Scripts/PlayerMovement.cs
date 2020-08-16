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
    public float moveSpeed;

    //Jetpack
    public float jetForce;
    public bool jetIsOn;
    public float boostTime;

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
    }

    void Update()
    {
        //ground check bool
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Move the player with new Vector2
        rb.velocity = new Vector2(move.x * moveSpeed, rb.velocity.y);

        //rotate sprite when moving left and right
        if (move.x > 0.3)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, transform.localScale.z);
            //RunningSound();
        }
        if (move.x < -0.3)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, transform.localScale.z);
        }

        //ANIMATOR
        //getting the player speed fot the animator

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
    }

    void FixedUpdate()
    {
        if(jetIsOn)
        {
            rb.AddForce(new Vector2(0f, jetForce), ForceMode2D.Force);
            StartCoroutine(JetPackCo());
        }
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

    public IEnumerator JetPackCo()
    {
        animator.SetTrigger("jetPackActive");

        yield return new WaitForSeconds(boostTime);

        jetIsOn = false;
    }
}
