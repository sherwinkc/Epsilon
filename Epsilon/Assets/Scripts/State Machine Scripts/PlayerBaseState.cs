//Not MonoBehaviour
//abstract mean we cannot create an instance of this class, we only create instances of the concrete states idle, run etc
//using UnityEngine; //remove this if not debugging.

public abstract class PlayerBaseState 
{
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;

    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;

    // constructor that expects the params, the same as the concrete states
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    //concrete states need to define functionality themselves. Abstract methods must be defined by inheriting class
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void FixedUpdate();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public void UpdateStates()
    {
        UpdateState();
    }

    public void UpdateFixedUpdateStates()
    {
        FixedUpdate();
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        _ctx.CurrentState = newState;
    }
}
