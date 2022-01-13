using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
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
        else if(!isFalling)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * jumpReleasedMultiplier);
        }

        Debug.Log("is Jump Pressed: " + isJumpPressed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandleAnimation()
    {
        bool isRunning = _anim.GetBool(isRunningHash);

        if (isMovementPressed)
        {
            _anim.SetBool(isRunningHash, true);
        }
        else if (!isMovementPressed)
        {
            _anim.SetBool(isRunningHash, false);
        }

        _anim.SetBool("isFalling", isFalling);
    }

    void Update()
    {
        //ground check bool
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        _anim.SetBool("isGrounded", isGrounded);

        if (!isGrounded)
        {
            _rb.velocity = new Vector2((currentMovement.x * moveSpeed * inAirMoveSpeedMultiplier), _rb.velocity.y);

            // Time.delta time version
            //rb.velocity = new Vector3((currentMovement.x * moveSpeed * inAirMoveSpeedMultiplier * Time.deltaTime), rb.velocity.y);

        }
        else
        {
            _rb.velocity = new Vector2((currentMovement.x * moveSpeed), _rb.velocity.y);

            // Time.delta time version
            //rb.velocity = new Vector2((currentMovement.x * moveSpeed * Time.deltaTime), rb.velocity.y);
        }

        //Debug.Log(currentMovement.x);
        Debug.Log("y vel: " + _rb.velocity.y);

        CheckIfPlayerIsFalling();

        //set gravity when falling
        if (isFalling)
        {
            _rb.gravityScale = gravityScaleWhenFalling;
        }
        else
        {
            _rb.gravityScale = 1f;
        }

        //clamp max y velocity if falling
        if (_rb.velocity.y < -maxFallVelocity)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -maxFallVelocity);
        }

        HandleAnimation();
        RotateSprite();
    }

    private void CheckIfPlayerIsFalling()
    {
        // is the player falling
        if (!isGrounded && _rb.velocity.y < fallingYAxisThreshold)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    void RotateSprite()
    {
        //rotate sprite when moving left and right
        if (currentMovement.x > 0.1)
        {
            transform.localScale = new Vector3(rotationScaleAmount, rotationScaleAmount, transform.localScale.z);
        }
        else if (currentMovement.x < -0.1)
        {
            transform.localScale = new Vector3(-rotationScaleAmount, rotationScaleAmount, transform.localScale.z);
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
