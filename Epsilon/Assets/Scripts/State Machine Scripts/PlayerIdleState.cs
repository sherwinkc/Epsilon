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
        if(_ctx.IsGrounded) _ctx.Rigidbody.velocity = new Vector2(_ctx.Rigidbody.velocity.x * _ctx.decelerationRate, _ctx.Rigidbody.velocity.y);

        if (!_ctx.IsGrounded) _ctx.Rigidbody.velocity = Vector2.zero;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        RotateSprite();

        ShootRaycastsForBox();
        RaycastDebug();
    }

    public override void FixedUpdate()
    {

    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_ctx.inCinematic)
        {
            SwitchState(_factory.InCinematic());
        }
        else if (_ctx.isThrustPressed && _ctx.canJetpack && _ctx.isJetpackOn)
        {
            SwitchState(_factory.Jetpack());
        }
        else if (_ctx.Rigidbody.velocity.y < -2f) //if y velocity is negative switch to falling
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
        else if (_ctx._hasLetGoOfLedge && _ctx.Rigidbody.velocity.y < -3f)
        {
            SwitchState(_factory.Falling());
        }
        else if (_ctx.hit.collider != null && _ctx.hit.collider.gameObject.CompareTag("MovableBox") && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2)))
        {
            SwitchState(_factory.GrabbingBox());
        }
        else if (_ctx.isTouchingWall && !_ctx.isTouchingLedge && _ctx.ledgeInfo.isNearClimbableMesh)
        {
            SwitchState(_factory.LedgeHang());
        }
    }

    private void ShootRaycastsForBox()
    {
        Physics2D.queriesStartInColliders = false;
        _ctx.hit = Physics2D.Raycast(_ctx.wallCheckForBox.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.boxCheckDistance, _ctx.whatIsGround);

        if (_ctx.hit.collider != null && _ctx.hit.collider.CompareTag("MovableBox"))
        { 
            _ctx.interact.interactHUD.SetActive(true); //TODO - Don't like accessing interact script just to display HUD tooltip 
            //_ctx.interact.PlayInteractSound();
        }

        if (_ctx.hit.collider == null && !_ctx.interact.isCloseEnoughToBattery && !_ctx.interact.isCloseEnoughToRover && !_ctx.interact.isCloseEnoughToLiftButton)
        {
            _ctx.interact.interactHUD.SetActive(false);
            //_ctx.interact.interactHasSFXPlayed = false;
        }
    }

    private void RaycastDebug()
    {
        Debug.DrawRay(_ctx.wallCheckForBox.position, (Vector2.right * _ctx.boxCheckDistance) * _ctx.transform.localScale.x * _ctx.playerLocalScaleOffset, Color.yellow);
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
