using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    //constructor functions
    // passes cocnrete state arguments directly to the base state constructor

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) {
    }

    public override void EnterState()
    {
        HandleJump();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * (_ctx.MoveSpeed * _ctx.InAirSpeedMultiplier)), _ctx.Rigidbody.velocity.y);

        HandleFalling();

        ShootRaycastsForLedgeClimb();
        RaycastDebug();
        RotateSprite();

        Debug.Log(_ctx.ledgeInfo.gameObject.tag);
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        //if is grounded (from PlayerStateMachine) switch state to the PlayerIdle
        if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.isTouchingWall && !_ctx.isTouchingLedge && _ctx.ledgeInfo.isNearClimbableMesh)
        {  
            SwitchState(_factory.LedgeHang());
        }
    }

    void HandleJump()
    {
        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);
        _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.JumpSpeed);
        _ctx.Animator.SetTrigger("Jump");
    }

    void HandleFalling()
    {
        // if the player falling
        if (_ctx.Rigidbody.velocity.y < _ctx.FallingYAxisThreshold)
        {
            _ctx.Rigidbody.gravityScale = _ctx.GravityScaleWhenFalling;
        }
        else
        {
            _ctx.Rigidbody.gravityScale = 1f;
        }
    }

    /*else if (!isFalling)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * jumpReleasedMultiplier);
        }

        //Debug.Log("is Jump Pressed: " + _isJumpPressed);*/

    private void ShootRaycastsForLedgeClimb()
    {

        _ctx.isTouchingWall = Physics2D.Raycast(_ctx.wallCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);
        _ctx.isTouchingLedge = Physics2D.Raycast(_ctx.ledgeCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);

    }

    private void RaycastDebug()
    {
        Debug.DrawRay(_ctx.wallCheck.position, (Vector2.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);
        Debug.DrawRay(_ctx.ledgeCheck.position, (Vector2.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);
    }

    void RotateSprite()
    {
        //rotate sprite when moving left and right
        if (_ctx.CurrentMovement.x > 0.1)
        {
            _ctx.transform.localScale = new Vector3(_ctx.RotationScaleAmount, _ctx.RotationScaleAmount, _ctx.transform.localScale.z);
        }
        else if (_ctx.CurrentMovement.x < -0.1)
        {
            _ctx.transform.localScale = new Vector3(-_ctx.RotationScaleAmount, _ctx.RotationScaleAmount, _ctx.transform.localScale.z);
        }
    }
}
