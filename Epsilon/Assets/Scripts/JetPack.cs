using JetBrains.Annotations;
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

    public GameObject boosterFlame;

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

        boosterFlame.SetActive(false);
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

        //if we are flying and jetpack is on we can move left and right with velocity * flyspeed
        if (!isGrounded && jetIsOn && !ledgeClimb.isClimbing && playerMovement.canMove && boostTime >= 0f)
        {
            rb.velocity = new Vector2(playerMovement.move.x * flySpeed, rb.velocity.y);
        }        
        
        if (!isGrounded && !jetIsOn && !ledgeClimb.isClimbing && playerMovement.canMove)
        {
            rb.AddForce(new Vector2(playerMovement.move.x/3, 0f), ForceMode2D.Force);
        }

        //if we are flying and jetpack is off and boost time is 0 we can move left and right with velocity * flyspeed * 0.9f   
        if (!isGrounded && !jetIsOn && !ledgeClimb.isClimbing && playerMovement.canMove && boostTime <= 0f)
        {
            rb.velocity = new Vector2((playerMovement.move.x * flySpeed) * 0.9f, rb.velocity.y);
        }

        //Debug.Log(rb.velocity);

        isGrounded = playerMovement.isGrounded;

        if (isGrounded && boostTime >= 1.5f)
        {
            boostTime = 1.5f;
            slider.value = boostTime;
        }

        if(isGrounded && boostTime < 1.5f)
        {
            boostTime = boostTime += Time.deltaTime/2; //Time.deltaTime / 3;
            slider.value = boostTime += Time.deltaTime/2; //Time.deltaTime / 3;
        }

        if (jetIsOn)
        {
            animator.SetTrigger("JetPackOn");
            if (!thrusters.isPlaying && !isGrounded)
            {
                if (boostTime > 0f)
                {
                    thrusters.Play();
                    boosterFlame.SetActive(true);
                }
            }        
        }

        if(!jetIsOn)
        {
            if (thrusters.isPlaying)
            {
                thrusters.Stop();
                boosterFlame.SetActive(false);
            }
        }

        if(boostTime <= 0)
        {
            jetIsOn = false;
        }
    }

    void FixedUpdate()
    {
        if(!ledgeClimb.isClimbing && playerMovement.canMove)
        {
            if (jetIsOn && boostTime >= 0f) //if Jet is on and boost time is more than 0
            {
                
                //While in the air, if dipping, add a buffer (Feels better)
                /*if(rb.velocity.y < 0 && !isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, flightYbuffer);
                }*/

                //Adding the force here
                rb.AddForce(new Vector2(0f, jetForce), ForceMode2D.Force);

                //Start the coroutine
                StartCoroutine(JetPackCo());

                //slider - if slider equals boost time decreasing
                slider.value = boostTime -= Time.deltaTime;
            }
        }
    }
    public IEnumerator JetPackCo()
    {
        //animator.SetBool("isGrounded", false);

        if (isGrounded)
        {
            thrusters.Stop();
            boosterFlame.SetActive(false);
            jetIsOn = false;
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(boostTime);
        }
        jetIsOn = false;
    }
}


