using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public Vector2 jetpack;

    //Jetpack
    /*public float jetForce;
    public bool jetIsOn;
    public float boostTime;
    public float flyForce;

    public float airSpeed;
    public float maxAirSpeed;
    public float flyAccel;
    public float flyDeccel;*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
         /*if(jetIsOn)
        {
            //add force when using jetpack
            rb.AddForce(new Vector2(0f, jetForce), ForceMode2D.Force);
            StartCoroutine(JetPackCo());
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
        }*/
    }
}


