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
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
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
}
