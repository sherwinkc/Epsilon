using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;

    //Player
    public Rigidbody2D playerRigidbody;
    public CapsuleCollider2D playerCapsuleCollider;
    public Animator animator;

    public Rigidbody2D[] ragdollRigidbodies;
    public CapsuleCollider2D[] ragdollCapsuleColliders;
    public HingeJoint2D[] hingeJoints;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
    }

    void Start()
    {
        DisableRagdoll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            EnableRagdoll();
        }
    }

    private void DisableRagdoll()
    {
        TurnOnPlayerComponents();
        TurnOffRagdollComponents();
    }

    public void EnableRagdoll()
    {
        TurnOffPlayerComponents();
        TurnOnRagdollComponents();
    }

    private void TurnOnPlayerComponents()
    {
        //playerRigidbody.isKinematic = false;
        playerRigidbody.simulated = true;
        animator.enabled = true;
        playerCapsuleCollider.enabled = true;
    }

    private void TurnOffPlayerComponents()
    {
        //playerRigidbody.isKinematic = true;
        playerRigidbody.simulated = false;
        animator.enabled = false;
        playerCapsuleCollider.enabled = false;
    }

    private void TurnOnRagdollComponents()
    {
        for (int i = 0; i < ragdollRigidbodies.Length; i++)
        {
            ragdollRigidbodies[i].simulated = true;
        }

        for (int i = 0; i < ragdollCapsuleColliders.Length; i++)
        {
            ragdollCapsuleColliders[i].enabled = true;
        }

        for (int i = 0; i < hingeJoints.Length; i++)
        {
            if (hingeJoints[i] != null)
            { 
                hingeJoints[i].enabled = true; 
            }             
        }

        playerStateMachine._jetEmission.Play();
        playerStateMachine.FootEmission.Stop();
    }

    private void TurnOffRagdollComponents()
    {
        for (int i = 0; i < ragdollRigidbodies.Length; i++)
        {
            ragdollRigidbodies[i].simulated = false;
        }

        for (int i = 0; i < ragdollCapsuleColliders.Length; i++)
        {
            ragdollCapsuleColliders[i].enabled = false;
        }

        for (int i = 0; i < hingeJoints.Length; i++)
        {
            if (hingeJoints[i] != null)
            {
                hingeJoints[i].enabled = false;
            }
        }
    }
}
