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
        //ClimbLedge();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if(!_ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Climb"))
        {
            SwitchState(_factory.Idle());
        }
    }

    void ClimbLedge()
    {

    }
}
