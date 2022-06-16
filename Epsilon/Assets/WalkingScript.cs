using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingScript : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;
    //Collapse collapse;

    public bool isAutoWalking = false;
    [SerializeField] float walkSpeed = 3f;
    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        //collapse = FindObjectOfType<Collapse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAutoWalking) playerStateMachine.Rigidbody.velocity = new Vector2(walkSpeed, playerStateMachine.Rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStateMachine.EnterIdleState();
            playerStateMachine.inCinematic = true;
            playerStateMachine.DisableGameplayControls();
            isAutoWalking = true;
        }
    }
}
