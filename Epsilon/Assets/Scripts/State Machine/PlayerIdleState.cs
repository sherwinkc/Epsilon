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
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        RotateSprite();
    }

    public override void ExitState()
    {

    }

    /*public override void InitializeSubState()
    {

    }*/

    public override void CheckSwitchStates()
    {
        if (_ctx.IsMovementPressed && _ctx.IsGrounded)
        {
            SwitchState(_factory.Run());
        }

        if (_ctx.IsJumpPressed)
        {
            SwitchState(_factory.Jump());
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
