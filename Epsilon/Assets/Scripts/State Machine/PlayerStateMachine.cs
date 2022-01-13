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

    //Animation Hashes
    int isGroundedHash;
    int _isRunningHash;
    //int speedXHash;

    //jumping variables
    [SerializeField] bool _isJumpPressed = false;
    [SerializeField] float _jumpSpeed = 5f;
    //[SerializeField] float gravityScaleWhenFalling = 2.5f;
    //[SerializeField] float jumpReleasedMultiplier = 0.33f;

    //Falling & Velocity
    //[SerializeField] float fallingYAxisThreshold = -0.25f;
    //[SerializeField] bool isFalling;
    //[SerializeField] float maxFallVelocity = 10f;

    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    #region Getters & Setters
    // getters and setters - Cleaner way to access member variable in another class. Grant accessing class read, write or both permission on the var
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _anim; } }
    public Rigidbody2D Rigidbody { get { return _rb; } }
    public bool IsGrounded { get { return _isGrounded; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public float JumpSpeed { get { return _jumpSpeed; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public Vector2 CurrentMovement { get { return _currentMovement;  } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float InAirSpeedMultiplier { get { return _inAirMoveSpeedMultiplier; } }
    public int IsRunningHash { get { return _isRunningHash; }/*get { IsRunningHash = value; }*/ }
    #endregion

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

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

    // Update is called once per frame
    void Update()
    {
        //ground check bool
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        _anim.SetBool("isGrounded", _isGrounded);

        _currentState.UpdateStates();
        //Debug.Log("State : " + _currentState);
        //Debug.Log("SubState : " +  

        RotateSprite();
    }

    void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _currentMovementInput = ctx.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.y = _currentMovementInput.y;
        isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        //Debug.Log(ctx.ReadValue<Vector2>());
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        _isJumpPressed = ctx.ReadValueAsButton();
    }

    void RotateSprite()
    {
        //rotate sprite when moving left and right
        if (_currentMovement.x > 0.1)
        {
            transform.localScale = new Vector3(_rotationScaleAmount, _rotationScaleAmount, transform.localScale.z);
        }
        else if (_currentMovement.x < -0.1)
        {
            transform.localScale = new Vector3(-_rotationScaleAmount, _rotationScaleAmount, transform.localScale.z);
        }
    }

    private void OnEnable()
    {
        _playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Gameplay.Disable();
    }
}
