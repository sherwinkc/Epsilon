using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    //constructor functions
    // passes cocnrete state arguments directly to the base state constructor
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsRunningHash, true);
        //_ctx.Rigidbody.gravityScale = 1f;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * _ctx.MoveSpeed), _ctx.Rigidbody.velocity.y);

        EmitFootstepVFX();
    }

    private void EmitFootstepVFX()
    {
        if (!_ctx.FootEmission.isPlaying) _ctx.FootEmission.Play();
    }

    public override void ExitState()
    {

    }

    /*public override void InitializeSubState()
    {

    }*/

    public override void CheckSwitchStates()
    {
        if (!_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Idle());
        } 
        else if(_ctx.IsMovementPressed)
        {
            SwitchState(_factory.Run());
        }
        
        
        if (_ctx.IsJumpPressed)
        {
            SwitchState(_factory.Jump());
        }
    }

}
