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

        //check if player is doing a soft landing - TODO create a Landing state for light and heavy landings
        if (_ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Landing_Light"))
        {
            _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * _ctx.MoveSpeed * 0.75f), _ctx.Rigidbody.velocity.y); //TODO magic number
        }
        else
        {
            _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * _ctx.MoveSpeed), _ctx.Rigidbody.velocity.y);
        }

        EmitFootstepVFX();

        RotateSprite();
    }

    private void EmitFootstepVFX()
    {
        if (!_ctx.FootEmission.isPlaying) _ctx.FootEmission.Play();
    }

    public override void ExitState()
    {

    }

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
