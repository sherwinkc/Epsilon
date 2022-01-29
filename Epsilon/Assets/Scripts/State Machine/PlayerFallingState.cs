using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //set Animator variables
        _ctx.Animator.SetBool(_ctx.IsFallingHash, true);
        _ctx.Animator.SetBool("ledgeDetected", false);

        _ctx.FootEmission.Stop();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * (_ctx.MoveSpeed * _ctx.InAirSpeedMultiplier)), _ctx.Rigidbody.velocity.y);

        ShootRaycastsForClimbing();
        RaycastDebug();

        RotateSprite();
    }

    public override void FixedUpdate()
    {

    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_ctx.isTouchingWall && !_ctx.isTouchingLedge && _ctx.ledgeInfo.isNearClimbableMesh)
        {
            SwitchState(_factory.LedgeHang());
        }
        else if (_ctx.isKneeTouchingLedge) 
        {
            SwitchState(_factory.Mount());
        }
        else if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.isThrustPressed)
        {
            SwitchState(_factory.Jetpack());
        }
    }

    private void ShootRaycastsForClimbing()
    {
        _ctx.isTouchingWall = Physics2D.Raycast(_ctx.wallCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);
        _ctx.isTouchingLedge = Physics2D.Raycast(_ctx.ledgeCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);

        _ctx.isKneeTouchingLedge = Physics2D.Raycast(_ctx.kneeCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance * 0.5f, _ctx.whatIsGround);
    }

    private void RaycastDebug()
    {
        Debug.DrawRay(_ctx.wallCheck.position, (Vector2.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);
        Debug.DrawRay(_ctx.ledgeCheck.position, (Vector2.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);

        Debug.DrawRay(_ctx.kneeCheck.position, (Vector2.right * _ctx.wallCheckDistance * 0.8f) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);
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
}
