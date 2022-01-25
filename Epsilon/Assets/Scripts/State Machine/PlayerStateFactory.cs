public class PlayerStateFactory
{
    PlayerStateMachine _context;

    // constructor functions are called when we create a new instance of a class
    // below, a new instance of PlayerStateFactory requires a PlayerStateMachine to be passed in
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    // Factory holds a reference of our state machine

    // public methods for our concrete states, of type PlayerBaseState
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this); // e.g return new instance of their respective states
    }

    public PlayerBaseState Run()
    {
        return new PlayerRunState(_context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState LedgeHang()
    {
        return new PlayerLedgeHangState(_context, this);
    }

    public PlayerBaseState ClimbLedge()
    {
        return new PlayerClimbState(_context, this);
    }

    public PlayerBaseState Falling()
    {
        return new PlayerFallingState(_context, this);
    }

    public PlayerBaseState LetGoOfLedge()
    {
        return new PlayerLetGoLedgeState(_context, this);
    }
}
