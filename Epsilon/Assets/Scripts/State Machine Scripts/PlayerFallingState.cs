using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.isTouchingClimbingPoint = false;

        //set Animator variables
        _ctx.Animator.SetBool(_ctx.IsFallingHash, true);
        _ctx.Animator.SetBool("isTouchingClimbingPoint", _ctx.isTouchingClimbingPoint);

        _ctx.FootEmission.Stop();

        _ctx.Rigidbody.gravityScale = _ctx.GravityScaleWhenFalling;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        if (!_ctx.isInCollapsingBridgeSequence) //TODO Don't like checking bool for input, just for collapse sequence
        {
            _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * (_ctx.MoveSpeed * _ctx.InAirSpeedMultiplier)), _ctx.Rigidbody.velocity.y);

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
        _ctx.Rigidbody.gravityScale = _ctx.DefaultScaleWhenFalling;
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
        else if (_ctx.IsGrounded && _ctx.airTime > _ctx.timeInAirBeforeDeath && _ctx.Rigidbody.velocity.y < _ctx.velocityInAirBeforeDeath) 
        {
            SwitchState(_factory.Death());
        }
        else if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.isThrustPressed && _ctx.thrustCounter > 0 && _ctx.isJetpackOn && !_ctx.isInCollapsingBridgeSequence)
        {
            SwitchState(_factory.Jetpack());
        }
        
        /*if (_ctx.isKneeTouchingLedge)
        {
            //SwitchState(_factory.Mount());
        }*/
    }

    private void ShootRaycastsForClimbing()
    {
        //canshootraycasts is like islettinggoof ledge check
        if (_ctx.canShootClimbingRaycasts) _ctx.isTouchingClimbingPoint = Physics2D.Raycast(_ctx.ledgeCheck.position, _ctx.transform.right * 
            (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsClimbable);
    }

    private void RaycastDebug()
    {
        if (_ctx.canShootClimbingRaycasts) Debug.DrawRay(_ctx.ledgeCheck.position, (_ctx.transform.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.red);
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
