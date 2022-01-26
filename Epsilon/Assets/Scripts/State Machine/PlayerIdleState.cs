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
        //Set Animator        
        _ctx.Animator.SetBool(_ctx.IsRunningHash, false);
        _ctx.Animator.SetBool(_ctx.IsFallingHash, false);
        _ctx.Animator.SetBool("isLettingGoLedge", false);
        _ctx.Animator.SetBool("ledgeDetected", false);

        //stop foot emission VFX when entering Idle
        _ctx.FootEmission.Stop();

        //Deceleration when idling from moving
        _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x * _ctx.decelerationRate, _ctx.Rigidbody.velocity.y); 
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
        if (_ctx.Rigidbody.velocity.y < -3f && _ctx._hasLetGoOfLedge) //if y velocity is negative switch to falling
        {
            SwitchState(_factory.Falling());
        }
        else if (_ctx.IsJumpPressed) //if jump pressed jump
        {
            SwitchState(_factory.Jump());
        }
        else if (_ctx.IsMovementPressed && _ctx.IsGrounded) // if movement pressed and grounded run
        {
            SwitchState(_factory.Run());
        }      
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
