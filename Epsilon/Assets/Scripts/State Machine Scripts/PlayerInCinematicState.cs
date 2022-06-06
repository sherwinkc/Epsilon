using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInCinematicState : PlayerBaseState
{
    public PlayerInCinematicState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
          : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.Rigidbody.velocity = Vector2.zero;
        //_ctx.Rigidbody.simulated = false; //Was turing off rigidbody during cinematic state such as stationary cameras

        //stop foot emission VFX when entering Idle
        _ctx.FootEmission.Stop();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdate()
    {

    }

    public override void ExitState()
    {
        //_ctx.Rigidbody.simulated = true;
    }

    public override void CheckSwitchStates()
    {
        if (!_ctx.inCinematic)
        {
            SwitchState(_factory.Idle());
        }
    }
}
