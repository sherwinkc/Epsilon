using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerBaseState
{
    public PlayerClimbState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
           : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.Animator.SetBool("Climb Up", true);
        _ctx.camManager.isCameraTargetPlayer = false;
        _ctx.isTouchingWall = false;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        _ctx.camManager.isCameraTargetPlayer = true;
    }

    public override void CheckSwitchStates()
    {
        if(!_ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Climb"))
        {
            SwitchState(_factory.Idle());
        }
    }
}
