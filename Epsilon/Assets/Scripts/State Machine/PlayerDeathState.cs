using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

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

        _ctx.Screenshake(1f);

        _ctx.audioManager.PlayDeathSounds();
        _ctx.audioManager.playerBreathingSFX.Stop();

        yield return new WaitForSeconds(_ctx.deadTimeBeforeRestart);

        _ctx.FadeScreen();

        yield return new WaitForSeconds(0.5f);

        _ctx.ragdoll.DisableRagdoll();

        _ctx.Respawn(); // This is in Player state Machine script - more of the respawn sequence

        //string scene = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(scene);
    }
}
