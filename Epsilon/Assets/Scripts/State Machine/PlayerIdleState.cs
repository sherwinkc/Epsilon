using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    //constructor functions
    // passes cocnrete state arguments directly to the base state constructor
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()  
    {
        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);

        //stop foot emission VFX when entering Idle
        _ctx.FootEmission.Stop();

        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);

        _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x * _ctx.deaccelerationRate, _ctx.Rigidbody.velocity.y);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        RotateSprite();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Rigidbody.velocity.y < -1f)
        {
            SwitchState(_factory.Falling());
        }
        else if (_ctx.IsJumpPressed)
        {
            SwitchState(_factory.Jump());
        }
        else if (_ctx.IsMovementPressed && _ctx.IsGrounded)
        {
            SwitchState(_factory.Run());
        }      
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
