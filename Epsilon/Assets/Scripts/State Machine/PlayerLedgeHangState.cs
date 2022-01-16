using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeHangState : PlayerBaseState
{
    public PlayerLedgeHangState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
           : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    { 
        _ctx.Animator.SetBool("ledgeDetected", true);
        LedgeHang();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        _ctx.Animator.SetBool("ledgeDetected", false);
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            SwitchState(_factory.ClimbLedge());
        }
    }

    public void LedgeHang()
    {
        _ctx.Rigidbody.simulated = false;
        Debug.Log("Ledge Hang");
        _ctx.transform.position = _ctx.ledgeInfo._currentGrabPoint.transform.position; //not working
        //_ctx.transform.position = _ctx.grabPoint.transform.position;
    }
}
