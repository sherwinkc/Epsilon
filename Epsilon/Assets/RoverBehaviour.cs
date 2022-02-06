using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverBehaviour : MonoBehaviour
{
    public float moveSpeed = 1f;

    bool canMove = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RoverStopPoint"))
        {
            canMove = false;
            FindObjectOfType<AudioManager>().roverEngine.Stop(); //TODO cache audio manager
            GetComponent<BoxCollider2D>().enabled = false;


        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            canMove = true;
            FindObjectOfType<AudioManager>().roverEngine.Play(); //TODO cache audio manager
        }
    }
}
