//Not MonoBehaviour
//abstract mean we cannot create an instance of this class, we only create instances of the concrete states idle, run etc
using UnityEngine; //remove this if not debugging.

public abstract class PlayerBaseState 
{
    protected bool _isRootState = false;
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;

    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;

    //public PlayerBaseState CurrentSubState { get { return _currentSubState; } }

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

    //public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        
        
        /*if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }*/

        //Debug.Log("Current Substate : " + _currentSubState);
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        _ctx.CurrentState = newState;

        //switch current state of the context
        /*if (_isRootState)
        {
            _ctx.CurrentState = newState; // this is using the setter from PlayerStateMachine script
        }
        else if(_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }*/
    }

    /*protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }*/
}
