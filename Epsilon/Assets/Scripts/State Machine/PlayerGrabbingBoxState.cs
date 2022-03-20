using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabbingBoxState : PlayerBaseState
{
    //constructor functions
    // passes cocnrete state arguments directly to the base state constructor
    public PlayerGrabbingBoxState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {    
        _ctx.box = _ctx.hit.collider.gameObject;
        //_ctx.hit.collider.GetComponent<Rigidbody2D>().isKinematic = false;
        _ctx.hit.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        FixedJoint2D boxComponents = _ctx.box.GetComponent<FixedJoint2D>();
        boxComponents.enabled = true;
        boxComponents.connectedBody = this._ctx.GetComponent<Rigidbody2D>();
<<<<<<< HEAD

        _ctx.Animator.SetBool("isGrabbing", true);


=======
>>>>>>> parent of cc0cdbd (0088)
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        _ctx.Rigidbody.velocity = new Vector2(_ctx.CurrentMovement.x * _ctx.moveSpeedWhileGrabbing, _ctx.Rigidbody.velocity.y);

        _ctx.interact.interactHUD.SetActive(false); //TODO Horrible place in the update loop. Something is causing this to not work in EnterState
    }

    public override void FixedUpdate()
    {

    }

    public override void ExitState()
    {
        _ctx.hit.rigidbody.velocity = Vector2.zero;
        //_ctx.hit.collider.GetComponent<Rigidbody2D>().isKinematic = true;
        _ctx.hit.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        FixedJoint2D boxComponents = _ctx.box.GetComponent<FixedJoint2D>();
        boxComponents.enabled = false;
        boxComponents.connectedBody = null;
<<<<<<< HEAD

        _ctx.Animator.SetBool("isPushing", false);
        _ctx.Animator.SetBool("isGrabbing", false);
=======
>>>>>>> parent of cc0cdbd (0088)
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            SwitchState(_factory.Idle());
        }
    }
}
