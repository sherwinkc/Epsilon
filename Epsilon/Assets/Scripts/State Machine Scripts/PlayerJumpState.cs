using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    //constructor functions
    // passes concrete state arguments directly to the base state constructor

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) {
    }

    public override void EnterState()
    {
        HandleJump();

        _ctx.canJump = false;

        _ctx._impactEffect.Play();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        if (!_ctx.isInCollapsingBridgeSequence) //TODO Don't like checking bool for input, just for collapse sequence
        {         
            _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * (_ctx.MoveSpeed * _ctx.InAirSpeedMultiplier)), _ctx.Rigidbody.velocity.y); //default logic
            //_ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.Rigidbody.velocity.y);

            //InAirLogic();

            ShootRaycastsForClimbing();
            RaycastDebug();
            RotateSprite();
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void ExitState()
    {
        _ctx.Rigidbody.velocity = new Vector2(0f, 0f);
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.inCinematic)
        {
            SwitchState(_factory.InCinematic());
        }
        else if (_ctx.isTouchingClimbingPoint && _ctx.ledgeInfo.isNearClimbableMesh && !_ctx._hasLetGoOfLedge)
        {
            SwitchState(_factory.LedgeHang());
        }
        else if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.Rigidbody.velocity.y < -1f && !_ctx._hasLetGoOfLedge)
        {
            SwitchState(_factory.Falling());
        }  
        else if (_ctx.isThrustPressed && _ctx.isJetpackOn && !_ctx.isInCollapsingBridgeSequence)
        {
            SwitchState(_factory.Jetpack());
        }
        /*else if (_ctx.isKneeTouchingLedge)
        {
            SwitchState(_factory.Mount());
        }*/
    }

    void HandleJump()
    {
        //animate jump
        _ctx.Animator.SetTrigger(_ctx.JumpHash);

        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);

        if (_ctx.IsJumpPressed)
        {
            _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.JumpSpeed);
        }
        else if (!_ctx.IsJumpPressed) 
        {
            _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.JumpSpeed);
        }

    }

    private void ShootRaycastsForClimbing()
    {
        //TODO bool check is like islettinggoofledge?
        if (_ctx.canShootClimbingRaycasts) _ctx.isTouchingClimbingPoint = Physics2D.Raycast(_ctx.ledgeCheck.position, _ctx.transform.right * 
            (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsClimbable);        
    }

    private void RaycastDebug()
    {
        if (_ctx.canShootClimbingRaycasts) Debug.DrawRay(_ctx.ledgeCheck.position, (Vector2.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);
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

    private void InAirLogic()
    {
        float fHorizontalVelocity = _ctx.Rigidbody.velocity.x;
        //float fHorizontalDamping = 0.25f;

        fHorizontalVelocity += _ctx.CurrentMovement.x * Time.deltaTime * _ctx._inAirAccelerationRate;
        //fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 100f);
        //Rigidbody.velocity = new Vector2(fHorizontalVelocity, Rigidbody.velocity.y);


        //_ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * _ctx.MoveSpeed), _ctx.Rigidbody.velocity.y); //Default
        _ctx.Rigidbody.velocity = new Vector2((fHorizontalVelocity /** _ctx.MoveSpeed*/), _ctx.Rigidbody.velocity.y);
    }
}
