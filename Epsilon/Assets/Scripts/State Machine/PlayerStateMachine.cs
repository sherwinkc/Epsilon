using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //decalre reference variables
    PlayerControls _playerControls;
    Rigidbody2D _rb;
    Animator _anim;
    AudioManager audioManager; // TODO Remove from here?
    public LedgeInformation ledgeInfo;

    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //camera test
    public CameraManager camManager;

    //variables
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _inAirMoveSpeedMultiplier;
    [SerializeField] float _rotationScaleAmount = 0.33f;

    //grounded checks    
    [SerializeField] bool _isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.2f;

    //variables to store player inputs
    Vector2 _currentMovementInput;
    Vector2 _currentMovement;
    [SerializeField] bool isMovementPressed;

    #region Animation Hashes
    int isGroundedHash;
    int _isRunningHash;
    //int speedXHash;
    #endregion

    //jumping variables
    [SerializeField] bool _isJumpPressed = false;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _gravityScaleWhenFalling = 2.5f;
    //[SerializeField] float jumpReleasedMultiplier = 0.33f;

    //Falling & Velocity
    [SerializeField] float _fallingYAxisThreshold = -0.25f;
    [SerializeField] bool _isFalling;
    [SerializeField] float _maxFallVelocity = 10f;

    //Footsteps particle system
    public ParticleSystem _footEmission;
    //public ParticleSystem _footsteps;
    //public ParticleSystem _impactEffect;
    //bool _wasOnGround;

    //Ledge Hang
    public Transform wallCheck;
    public Transform ledgeCheck;

    public float playerLocalScaleOffset = 3f; // should equal 1 divided player scale reduction

    public bool isTouchingWall;
    public bool isTouchingLedge;
    public float wallCheckDistance;
    public bool ledgeDetected = false;

    #region Getters & Setters
    // getters and setters - Cleaner way to access member variable in another class. Grant accessing class read, write or both permission on the var
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _anim; } }
    public Rigidbody2D Rigidbody { get { return _rb; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public Vector2 CurrentMovement { get { return _currentMovement;  } }
    public ParticleSystem FootEmission { get { return _footEmission; } set { _footEmission = value; } }
    public bool IsGrounded { get { return _isGrounded; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public float JumpSpeed { get { return _jumpSpeed; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float InAirSpeedMultiplier { get { return _inAirMoveSpeedMultiplier; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public float FallingYAxisThreshold { get { return _fallingYAxisThreshold; } }
    public bool IsFalling { get { return _isFalling;  } }
    public float MaxFallVelocity { get { return _maxFallVelocity; } }
    public float GravityScaleWhenFalling { get { return _gravityScaleWhenFalling; } }
    public float RotationScaleAmount {  get { return _rotationScaleAmount; } }
    #endregion

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        ledgeInfo = GetComponent<LedgeInformation>();

        //camera test
        camManager = FindObjectOfType<CameraManager>();

        //Controls calling OnMovement Input Function
        _playerControls.Gameplay.Move.started += OnMovementInput;
        _playerControls.Gameplay.Move.canceled += OnMovementInput;
        _playerControls.Gameplay.Move.performed += OnMovementInput;
        _playerControls.Gameplay.Jump.started += OnJump;
        _playerControls.Gameplay.Jump.canceled += OnJump;

        //Converting strings to hash is more performant
        isGroundedHash = Animator.StringToHash("isGrounded");
        _isRunningHash = Animator.StringToHash("isRunning");
        //speedXHash = Animator.StringToHash("SpeedX");

        // setup state
        _states = new PlayerStateFactory(this); // passes this PlayerStateMachine instance. PlayerStateFactory is expecting a PlayerStateMachine 
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //ground check bool
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        _anim.SetBool("isGrounded", _isGrounded);

        _currentState.UpdateStates();
    }

    void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _currentMovementInput = ctx.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.y = _currentMovementInput.y;
        isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        _isJumpPressed = ctx.ReadValueAsButton();
    }

    public void TeleportPlayerAfterLedgeClimb()
    {
        if(ledgeInfo._currentEndPoint.position != null ) transform.position = ledgeInfo._currentEndPoint.position;
        Rigidbody.simulated = true;
        Rigidbody.velocity = Vector2.zero;
    }

    private void OnEnable()
    {
        _playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Gameplay.Disable();
    }

    #region Audio functions for Animator events //TODO find a potential alternative solution
    public void PlayFootsteps()
    {
        audioManager.Play_playerSFX_footsteps_sand();
    }

    public void PlayJumpSFX()
    {
        audioManager.Play_playerSFX_Jump();
    }

    public void PlayLandingSFX()
    {
        audioManager.Play_playerSFX_Land();
    }
    #endregion
}
