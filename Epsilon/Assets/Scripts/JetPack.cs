﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour
{
    //Components
    public PlayerMovement playerMovement;
    public Rigidbody2D rb;
    public Animator animator;
    public LedgeClimb ledgeClimb;

    public Vector2 jetpack;

    public Slider slider;

    public bool isGrounded;

    //Jetpack
    public float jetForce;
    public bool jetIsOn;
    public float boostTime;
    public float flySpeed;
    
    public float flightYbuffer;
    public float flightXInertia;

    //public float airSpeed;
    //public float maxAirSpeed;
    //public float flyAccel;
    //public float flyDeccel;

    public AudioSource jetpackSfx;
    public AudioSource thrusters;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ledgeClimb = GetComponent<LedgeClimb>();

        flySpeed = playerMovement.maxSpeed * 1.2f;

        slider.maxValue = boostTime;
    }

    // Update is called once per frame
    void Update()
    {
        //AIR
        //slightly increase movement when in the air
        /*if(!isGrounded && jetIsOn && !ledgeClimb.isClimbing)
        {
            if(playerMovement.move.x <= 0.1 && playerMovement.move.x >= -0.1f)
            {   
                playerMovement.move.x = flightXInertia * transform.localScale.x;
            }
            rb.velocity = new Vector2(playerMovement.move.x * flySpeed, rb.velocity.y);
        }*/

        if (!isGrounded && jetIsOn && !ledgeClimb.isClimbing && playerMovement.canMove)
        {
            rb.velocity = new Vector2(playerMovement.move.x * flySpeed, rb.velocity.y);
        }

        //Debug.Log(rb.velocity);

        isGrounded = playerMovement.isGrounded;

        if (isGrounded && boostTime >= 1.5f)
        {
            boostTime = 1.5f;
            slider.value = boostTime;
            //thrusters.Stop();
        }

        if(isGrounded && boostTime < 1.5f)
        {
            boostTime = boostTime += Time.deltaTime; //Time.deltaTime / 3;
            slider.value = boostTime += Time.deltaTime; //Time.deltaTime / 3;
            //thrusters.Stop();
        }

        if (jetIsOn)
        {
            animator.SetTrigger("JetPackOn");
            if (!thrusters.isPlaying)
            {
                thrusters.Play();
            }

            if (!jetpackSfx.isPlaying)
            {
                jetpackSfx.Play();
            }
        }

        if(!jetIsOn)
        {
            if (thrusters.isPlaying)
            {
                thrusters.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        if(!ledgeClimb.isClimbing && playerMovement.canMove)
        {
            if (jetIsOn && boostTime >= 0f)
            {
                //add force when using jetpack
                if(rb.velocity.y < 0 && !isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, flightYbuffer);
                }
                rb.AddForce(new Vector2(0f, jetForce), ForceMode2D.Force);

                StartCoroutine(JetPackCo());

                slider.value = boostTime -= Time.deltaTime;

                if(!jetpackSfx.isPlaying)
                {
                    jetpackSfx.Play();
                }
            }
        }
    }
    public IEnumerator JetPackCo()
    {
        //animator.SetBool("isGrounded", false);

        if (isGrounded)
        {
            //thrusters.Stop();
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(boostTime);
        }
        jetIsOn = false;
    }
}


