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
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float inAirMoveSpeedMultiplier;
    [SerializeField] float rotationScaleAmount = 0.33f;

    //grounded checks    
    [SerializeField] bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.2f;

    //variables to store player inputs
    Vector2 currentMovementInput;
    Vector2 currentMovement;
    [SerializeField] bool isMovementPressed;

    //Animation Hashes
    int isGroundedHash;
    int isRunningHash;
    //int speedXHash;

    //jumping variables
    bool isJumpPressed = false;
    [SerializeField] float jumpSpeed;
    [SerializeField] float gravityScaleWhenFalling = 2.5f;
    [SerializeField] float jumpReleasedMultiplier = 0.33f;

    [SerializeField] float fallingYAxisThreshold = -0.25f;
    [SerializeField] bool isFalling;
    [SerializeField] float maxFallVelocity = 10f;

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
        isRunningHash = Animator.StringToHash("isRunning");
        //speedXHash = Animator.StringToHash("SpeedX");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMovementInput(InputAction.CallbackContext ctx)
    {
        currentMovementInput = ctx.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        //Debug.Log(ctx.ReadValue<Vector2>());
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        isJumpPressed = ctx.ReadValueAsButton();

        if (isGrounded && isJumpPressed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);

            _anim.SetTrigger("Jump");
        }
        else if (!isFalling)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * jumpReleasedMultiplier);
        }

        Debug.Log("is Jump Pressed: " + isJumpPressed);
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
