using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    //constructor functions
    // passes cocnrete state arguments directly to the base state constructor
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) {
        _isRootState = true;
        //InitializeSubState(); 
    }

    public override void EnterState()
    {
        HandleJump();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * (_ctx.MoveSpeed * _ctx.InAirSpeedMultiplier)), _ctx.Rigidbody.velocity.y);
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        //if is grounded (from PlayerStateMachine) switch state to the PlayerGroundedState
        if (_ctx.IsGrounded)
        {
            //SwitchState(_factory.Grounded());
            SwitchState(_factory.Idle());
        }
    }

    /*public override void InitializeSubState()
    {

    }*/

    void HandleJump()
    {
        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);
        _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.JumpSpeed);
        _ctx.Animator.SetTrigger("Jump");
    }
        
    /*else if (!isFalling)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * jumpReleasedMultiplier);
        }

        //Debug.Log("is Jump Pressed: " + _isJumpPressed);*/
}
