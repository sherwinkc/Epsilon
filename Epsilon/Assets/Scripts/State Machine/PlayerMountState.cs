using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMountState : PlayerBaseState
{
    public PlayerMountState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
           : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.Animator.SetBool("mountDetected", true);
        _ctx.Rigidbody.simulated = false;

        AdjustPlayerPosition();
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
        _ctx.camManager.isCameraTargetPlayer = true;

        _ctx.isKneeTouchingLedge = false;
        _ctx.Animator.SetBool("mountDetected", false);
    }

    public override void CheckSwitchStates()
    {
        if (!_ctx.Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Mount"))
        {
            SwitchState(_factory.Idle());
        }
    }

    private void AdjustPlayerPosition()
    {
        if (_ctx.ledgeInfo.isPlayerLeftSideOfMesh)
        {
            _ctx.transform.position = new Vector2(_ctx.ledgeInfo._currentGrabPoint.transform.position.x + _ctx.mountPositionOffsetX, _ctx.ledgeInfo._currentGrabPoint.transform.position.y + _ctx.mountPositionOffsetY);
        }
        else if (_ctx.ledgeInfo.isPlayerRightSideOfMesh)
        {
            _ctx.transform.position = new Vector2(_ctx.ledgeInfo._currentGrabPoint.transform.position.x - _ctx.mountPositionOffsetX, _ctx.ledgeInfo._currentGrabPoint.transform.position.y + _ctx.mountPositionOffsetY);
        }
    }
}
