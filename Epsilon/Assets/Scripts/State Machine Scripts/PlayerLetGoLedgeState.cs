using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLetGoLedgeState : PlayerBaseState
{
    public PlayerLetGoLedgeState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
           : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        //set Animator variables
        //_ctx.Animator.SetBool("isLettingGoLedge", true);
        _ctx.Animator.SetBool("isHangingFromLedge", false);
        _ctx.Animator.SetBool("isTouchingClimbingPoint", false);

        LetGoLogic();
        //Debug.Log("LetGoState");
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
        /*if (_ctx.Rigidbody.velocity.y < -1f && _ctx.airTime > 0.2f) //TODO fix so that I can grab ledges when falling in this state
        {
            SwitchState(_factory.Falling());
        }
        else*/ if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.isThrustPressed && _ctx.isJetpackOn)
        {
            SwitchState(_factory.Jetpack());
        }
                else if (_ctx.isTouchingClimbingPoint && _ctx.ledgeInfo.isNearClimbableMesh && _ctx.canShootClimbingRaycasts)
        {
            SwitchState(_factory.LedgeHang());
        }
    }

    private void LetGoLogic()
    {
        _ctx.Rigidbody.simulated = true;
        //_ctx.Animator.SetBool(_ctx.IsFallingHash, true);
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
