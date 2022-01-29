using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeHangState : PlayerBaseState
{
    public PlayerLedgeHangState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
           : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    { 
        //set animator variables
        _ctx.Animator.SetBool("ledgeDetected", true);

        LedgeHang();
        _ctx.Rigidbody.velocity = new Vector2(0f, 0f);
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

    }

    public override void CheckSwitchStates()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            SwitchState(_factory.ClimbLedge());
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            _ctx._hasLetGoOfLedge = true;
            SwitchState(_factory.LetGoOfLedge());
        }
    }

    public void LedgeHang()
    {
        _ctx.Rigidbody.simulated = false;
        _ctx.transform.position = _ctx.ledgeInfo._currentGrabPoint.transform.position; 
    } 
}
