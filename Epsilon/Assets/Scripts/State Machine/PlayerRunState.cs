using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerRunState : PlayerBaseState
{
    //constructor functions
    // passes cocnrete state arguments directly to the base state constructor
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //Debug.Log("Run Enter State");
        _ctx.Animator.SetBool(_ctx.IsRunningHash, true);
        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        //check if player is doing a soft landing - TODO create a Landing state for light and heavy landings
        if (_ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Landing_Light"))
        {
            _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * _ctx.MoveSpeed * _ctx.SoftLandingSpeedMultiplier), _ctx.Rigidbody.velocity.y);
        }
        else
        {
            float fHorizontalVelocity = _ctx.Rigidbody.velocity.x;
            //float fHorizontalDamping = 0.25f;

            fHorizontalVelocity += _ctx.CurrentMovement.x * Time.deltaTime * _ctx.accelerationRate;
            //fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 100f);
            //Rigidbody.velocity = new Vector2(fHorizontalVelocity, Rigidbody.velocity.y);


            //_ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * _ctx.MoveSpeed), _ctx.Rigidbody.velocity.y); //Default
            _ctx.Rigidbody.velocity = new Vector2((fHorizontalVelocity /** _ctx.MoveSpeed*/), _ctx.Rigidbody.velocity.y);

            //Rigidbody.velocity = new Vector2(fHorizontalVelocity, Rigidbody.velocity.y);
        }

        ClampVelocity();

        EmitFootstepVFX();
        RotateSprite();

        ShootRaycastsForBox();
        RaycastDebug();
    }

    public override void FixedUpdate() 
    { 

    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_ctx.inCinematic)
        {
            SwitchState(_factory.InCinematic());
        }
        else if(_ctx.IsJumpPressed)
        {
            SwitchState(_factory.Jump());
        }
        else if (_ctx.Rigidbody.velocity.y < -3f && !_ctx._hasLetGoOfLedge)
        {
            SwitchState(_factory.Falling());
        }
        else if (!_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.isThrustPressed && _ctx.isJetpackOn)
        {
            SwitchState(_factory.Jetpack());
        }
        else if (_ctx.hit.collider != null && _ctx.hit.collider.gameObject.CompareTag("MovableBox") && Input.GetKeyDown(KeyCode.E))
        {
            SwitchState(_factory.GrabbingBox());
        }

        /*else if(_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Run());
        }*/
    }

    private void ClampVelocity()
    {
        if (_ctx.Rigidbody.velocity.x > 4f)
        {
            _ctx.Rigidbody.velocity = new Vector2(4f, _ctx.Rigidbody.velocity.y);
        }

        if (_ctx.Rigidbody.velocity.x < -4f)
        {
            _ctx.Rigidbody.velocity = new Vector2(-4f, _ctx.Rigidbody.velocity.y);
        }
    }

    private void EmitFootstepVFX()
    {
        if (!_ctx.FootEmission.isPlaying && _ctx.IsGrounded && Mathf.Abs(_ctx.Rigidbody.velocity.x) > 1f)
        {
            _ctx.FootEmission.Play();
        }
    }

    private void ShootRaycastsForBox()
    {
        Physics2D.queriesStartInColliders = false;
        _ctx.hit = Physics2D.Raycast(_ctx.wallCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.boxCheckDistance, _ctx.whatIsGround);

        if (_ctx.hit.collider != null && _ctx.hit.collider.CompareTag("MovableBox"))
        {
            _ctx.interact.interactHUD.SetActive(true); //TODO - Don't like accessing interact script just to display HUD tooltip 
        }
        else if (_ctx.hit.collider == null)
        {
            _ctx.interact.interactHUD.SetActive(false);
        }
    }

    private void RaycastDebug()
    {
        Debug.DrawRay(_ctx.wallCheck.position, (Vector2.right * _ctx.boxCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.yellow);
    }

    void RotateSprite()
    {
        //rotate sprite when moving left and right
        if (_ctx.Rigidbody.velocity.x > 0.5f)
        {
            _ctx.transform.localScale = new Vector3(_ctx.RotationScaleAmount, _ctx.RotationScaleAmount, _ctx.transform.localScale.z);
        }
        else if (_ctx.Rigidbody.velocity.x < -0.5f)
        {
            _ctx.transform.localScale = new Vector3(-_ctx.RotationScaleAmount, _ctx.RotationScaleAmount, _ctx.transform.localScale.z);
        }
    }

    /*private void OnTriggerEnter2D(BoxCollider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("Dead");
        }
    }*/
}
