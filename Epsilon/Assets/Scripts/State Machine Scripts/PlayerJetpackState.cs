using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetpackState : PlayerBaseState
{
    public PlayerJetpackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
       : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        if(!_ctx.Rigidbody.simulated) _ctx.Rigidbody.simulated = true; // if the player jetpacks out of ledge hang

        if (!_ctx.IsGrounded)
        {
            _ctx.Rigidbody.velocity = Vector2.zero; // get rid of any velocity
        }

        if (_ctx.IsGrounded)
        {
            _ctx.Rigidbody.AddForce(new Vector3(0f, 75f, 0f), ForceMode2D.Impulse); // give player an initial boost once
            if(!_ctx.audioManager.jetpackStart.isPlaying) _ctx.audioManager.jetpackStart.Play(); // play audio
        }

        _ctx.Animator.SetBool("isJetpacking", true);
        _ctx.Animator.SetBool("isFalling", false);
;
        _ctx._jetEmission.Play();
        _ctx.FootEmission.Stop();

        _ctx.boostDetails.SetActive(true);

        _ctx.canJetpack = false;

        if (_ctx.audioManager != null) 
        { 
            _ctx.audioManager.jetpackLoop.Play();
            //if(!_ctx.audioManager.jetpackStart.isPlaying) _ctx.audioManager.jetpackStart.Play();        
        }

        _ctx.StartCoroutine(_ctx.DelayRaycasts());
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        //_ctx.Rigidbody.AddForce(new Vector2(0f, _ctx.thrust), ForceMode2D.Force);
        //_ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x, _ctx.Rigidbody.velocity.y + 0.15f);

        if (!_ctx.isInCollapsingBridgeSequence && _ctx.isJetpackOn) //TODO Don't like checking bool for input, just for collapse sequence
        {

        }

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
        _ctx.boostDetails.SetActive(false);

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
        else if (_ctx.Rigidbody.velocity.y < -10f) // switch to falling when veloctity is falling
        {
            SwitchState(_factory.Falling());
        }
        else if (!_ctx.isThrustPressed)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.isTouchingClimbingPoint && _ctx.ledgeInfo.isNearClimbableMesh && !_ctx._hasLetGoOfLedge)
        {
            SwitchState(_factory.LedgeHang());
        }
        /*else if (!_ctx.canJetpack)
        {
            SwitchState(_factory.Falling());
        }*/

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
        //canshootraycasts is like islettinggoof ledge check
        if (_ctx.canShootClimbingRaycasts) _ctx.isTouchingClimbingPoint = Physics2D.Raycast(_ctx.ledgeCheck.position, _ctx.transform.right *
            (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsClimbable);
    }

    private void RaycastDebug()
    {
        if (_ctx.canShootClimbingRaycasts) Debug.DrawRay(_ctx.ledgeCheck.position, (_ctx.transform.right * _ctx.wallCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.red);
    }
}
