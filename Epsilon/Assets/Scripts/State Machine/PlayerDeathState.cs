using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {
    }

    public override void EnterState() 
    {
        _ctx.StartCoroutine(Die());

        _ctx.audioManager.PlayDeathCrunchSFX();

        _ctx.ActivateDeathCam();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdate() { }

    public override void ExitState() { }

    public override void CheckSwitchStates() { }

    public IEnumerator Die()
    {
        _ctx.ragdoll.EnableRagdoll();

        yield return new WaitForSeconds(_ctx.deadTimeBeforeRestart);

        _ctx.FadeScreen();

        yield return new WaitForSeconds(0.5f);

        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}
