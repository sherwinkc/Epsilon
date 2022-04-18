using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetpackState : PlayerBaseState
{
    public PlayerJetpackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
       : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        if(!_ctx.IsGrounded) _ctx.Rigidbody.velocity = Vector2.zero;

        _ctx.Animator.SetBool("isJetpacking", true);
        _ctx.Animator.SetBool("isFalling", false);
;
        _ctx._jetEmission.Play();
        _ctx.FootEmission.Stop();

        _ctx.canJetpack = false;

        if (_ctx.audioManager != null) 
        { 
            _ctx.audioManager.jetpackLoop.Play();
            //if(!_ctx.audioManager.jetpackStart.isPlaying) _ctx.audioManager.jetpackStart.Play();        
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        //_ctx.Rigidbody.AddForce(new Vector2(0f, _ctx.thrust), ForceMode2D.Force);
        //_ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.Rigidbody.velocity.y + 0.15f);

        _ctx.Rigidbody.velocity = new Vector2((_ctx.CurrentMovement.x * _ctx._jetPackMoveSpeed), _ctx.Rigidbody.velocity.y);

        _ctx.thrustCounter -= Time.deltaTime;

        RotateSprite();

        ShootRaycastsForClimbing();
        RaycastDebug();
    }

    public override void FixedUpdate()
    {
        _ctx.Rigidbody.AddForce(new Vector2(0f, _ctx.thrustForce), ForceMode2D.Force);
    }

    public override void ExitState()
    {
        _ctx.Animator.SetBool("isJetpacking", false);

        if (_ctx.audioManager != null) _ctx.audioManager.jetpackLoop.Stop();

        _ctx._jetEmission.Stop();

        //_ctx.thrustCounter = _ctx.thrustTime;

        //_ctx.audioManager.StopJetpackLoop();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.inCinematic)
        {
            SwitchState(_factory.InCinematic());
        }
        else if(_ctx.thrustCounter <= 0f)
        {
            SwitchState(_factory.Falling());
        }
        else if (_ctx.Rigidbody.velocity.y < -1f) // switch to falling when veloctity is falling
        {
            SwitchState(_factory.Falling());
        }
        else if (!_ctx.isThrustPressed)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.isTouchingWall && !_ctx.isTouchingLedge && _ctx.ledgeInfo.isNearClimbableMesh)
        {
            SwitchState(_factory.LedgeHang());
        }

        /*if (_ctx.IsGrounded)
        {
            SwitchState(_factory.Idle());
        }*/
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

    private void ShootRaycastsForClimbing()
    {
        _ctx.isTouchingWall = Physics2D.Raycast(_ctx.wallCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);
        _ctx.isTouchingLedge = Physics2D.Raycast(_ctx.ledgeCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);

        //_ctx.isKneeTouchingLedge = Physics2D.Raycast(_ctx.kneeCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance * 0.5f, _ctx.whatIsGround);
    }

    private void RaycastDebug()
    {
        Debug.DrawRay(_ctx.wallCheck.position, (Vector2.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);
        Debug.DrawRay(_ctx.ledgeCheck.position, (Vector2.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);

        //Debug.DrawRay(_ctx.kneeCheck.position, (Vector2.right * _ctx.wallCheckDistance * 0.8f) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.white);
    }
}
