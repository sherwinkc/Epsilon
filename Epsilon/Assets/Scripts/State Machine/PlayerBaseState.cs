//Not MonoBehaviour
//abstract mean we cannot create an instance of this class, we only create instances of the concrete states idle, run etc
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

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    void UpdateStates()
    {

    }

    protected void SwitchState(PlayerBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        //switch current state of the context
        _ctx.CurrentState = newState; // this is using the setter from PlayerStateMachine script
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
