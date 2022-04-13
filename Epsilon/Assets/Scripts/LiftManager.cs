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
        
            if(isMovingDown)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
            }
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

        if (moveLift)
        {
            timer = 0f;
        }

        if (timer >= timeToReset)
        {
            ResetLift();
        }
    }

    void ResetLift()
    {
        isMovingUp = false;
        isMovingDown = true;
        moveLift = true;
        audioManager.liftActiveLoop.Stop();
        //transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
    }
}
