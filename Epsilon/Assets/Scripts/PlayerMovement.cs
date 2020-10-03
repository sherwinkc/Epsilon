﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    //Components
    public Rigidbody2D rb;
    public Animator animator;
    public JetPack jetPack;
    public HUDController hudController;

    //Controller Movement
    PlayerControls controls;
    public Vector2 move;    

    //checks    
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    public Transform groundCheck;
    public Transform wallCheck;
    public bool isTouchingWall;

    //Movement
    public float speed;
    public float maxSpeed;
    public float walkAccel;
    public float walkDeccel;

    public float jumpSpeed;
    public bool isSlowWalking;
    public bool isNearBox = false;

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
        controls.Gameplay.Interact.canceled += ctx => StopInteracting();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jetPack = GetComponent<JetPack>();
        hudController = GetComponent<HUDController>();
    }

    void Update()
    {
        //ground check bool
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        PlayerMovementGround();
        RotateSprite();

        //ANIMATOR
        //getting the player speed for the animator
        if (isGrounded)
        {
            animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isSlowWalking", isSlowWalking);
        animator.SetFloat("SpeedY", rb.velocity.y);
    }

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

        //wall stop needs its own check
        if (isTouchingWall && isGrounded)
        {
            animator.SetTrigger("wallStop");
            speed = 0;
        }

        if(rb.velocity.y < 0 && !isGrounded)
        {
            //animator.SetTrigger("Falling");
        }
    }

    void RotateSprite()
    {
        //rotate sprite when moving left and right
        if (move.x > 0.3)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, transform.localScale.z);
        }

        if (move.x < -0.3)
        {
            transform.localScale = new Vector3(-0.4f, 0.4f, transform.localScale.z);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            animator.SetTrigger("Jump");
        }
        else if (!isGrounded && !jetPack.jetIsOn)
        {
            jetPack.jetIsOn = true;
        }
        else if (!isGrounded && jetPack.jetIsOn)
        {
            jetPack.jetIsOn = false;
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
        if(isGrounded)
        {
            playerFootsteps.Play();
            playerFootsteps.pitch = (Random.Range(0.85f, 1f));
        }
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
            hudController.SuitRepaired();
        }
    }

    public void StopInteracting()
    {
        animator.SetBool("stopPushing", true);
    }

    //Interact IEnumerators
    public IEnumerator HealthBoxInteraction()
    {
        PD_HealthBox.Play();

        yield return new WaitForSeconds(15);

        maxSpeed = 4;
        slowWalkCollider.enabled = false;
        isSlowWalking = false;
        isNearBox = false;

        yield return null;
    }
}
