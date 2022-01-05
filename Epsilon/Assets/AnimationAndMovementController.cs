using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    //decalre reference variables
    PlayerControls playerControls;
    Rigidbody2D rb;
    Animator anim;    

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

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Controls calling OnMovement Input Function
        playerControls.Gameplay.Move.started += OnMovementInput;
        playerControls.Gameplay.Move.canceled += OnMovementInput;
        playerControls.Gameplay.Move.performed += OnMovementInput;
        playerControls.Gameplay.Jump.started += OnJump;
        //playerControls.Gameplay.Jump.canceled += OnJump;

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

        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            anim.SetTrigger("Jump");
        }

        //Debug.Log(isJumpPressed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandleAnimation()
    {
        bool isRunning = anim.GetBool(isRunningHash);

        if (isMovementPressed)
        {
            anim.SetBool(isRunningHash, true);
        }
        else if (!isMovementPressed)
        {
            anim.SetBool(isRunningHash, false);
        }
    }

    void Update()
    {
        //ground check bool
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);

        if (!isGrounded)
        {
            rb.velocity = new Vector3((currentMovement.x * moveSpeed * inAirMoveSpeedMultiplier * Time.deltaTime), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2((currentMovement.x * moveSpeed * Time.deltaTime), rb.velocity.y);
        }

        //Debug.Log(currentMovement.x);
        Debug.Log("vel: " + rb.velocity);

        if (rb.velocity.y < -0.4f)
        {
            rb.gravityScale = 1.8f;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        HandleAnimation();
        RotateSprite();
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
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();   
    }
}
