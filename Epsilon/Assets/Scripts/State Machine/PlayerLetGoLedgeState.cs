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
        _ctx.Animator.SetBool("isLettingGoLedge", true);
        _ctx.Animator.SetBool("ledgeDetected", false);

        LetGoLogic();
        Debug.Log("LetGoState");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * (_ctx.MoveSpeed * _ctx.InAirSpeedMultiplier)), _ctx.Rigidbody.velocity.y);

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
        if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }
    }

    private void LetGoLogic()
    {
        //_ctx.canDetectLedges = false;
        _ctx.Rigidbody.simulated = true;
        //_ctx.Animator.SetBool("ledgeDetected", false);
        //_ctx.Animator.SetBool(_ctx.IsFallingHash, true);
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
