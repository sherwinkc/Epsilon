using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] float moveSpeed = 0.01f;

    public Transform startPoint;
    public Transform endPoint;

    public bool moveLift = false;
    public bool isMovingUp = true;
    public bool isMovingDown = false;

    public bool isTimerActive = false;

    public float timeToReset = 10f;
    public float timer = 0f;

    [SerializeField] bool isLiftLoopPlaying = false;

    public float distanceFromSound = 15f;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        isMovingUp = true;
    }

    void FixedUpdate()
    {
        if (moveLift)
        {
            if (isMovingUp)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
            }
            else if (isMovingDown)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
            }

            timer = 0f; // if lift is moving timer is zero;
        }

        if (transform.position.y >= endPoint.transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            moveLift = false;
            isMovingUp = false;
            isMovingDown = true;
        }

        if (transform.position.y <= startPoint.transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            moveLift = false;
            isMovingUp = true;
            isMovingDown = false;
        }

        if (!moveLift && transform.position.y > startPoint.transform.position.y)
        {
            timer += Time.deltaTime;
        }

        if (timer >= timeToReset)
        {
            ResetLift();
            StopAudio();
        }

        PlayLiftAudio();
    }

    private void StopAudio()
    {
        audioManager.liftActiveLoop.Stop(); //TODO being called every second when lift is not moving BAD
    }

    void ResetLift()
    {
        isMovingUp = false;
        isMovingDown = true;
        moveLift = true;
        //transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
    }

    private void PlayLiftAudio()
    {
        //if lift is moving
        if (moveLift)
        {
            if(Vector2.Distance(transform.position, FindObjectOfType<PlayerStateMachine>().transform.position) < distanceFromSound) //TODO Cache Player Transform
            {
                //play audio
                if (!isLiftLoopPlaying)
                {
                    audioManager.liftActiveLoop.Play();
                    isLiftLoopPlaying = true;
                }
            }
        }
        //if lift is not moving
        else if (!moveLift)
        {
            //stop audio
            if (isLiftLoopPlaying)
            {
                audioManager.liftActiveLoop.Stop();
                isLiftLoopPlaying = false;
            }
        }
    }
}
