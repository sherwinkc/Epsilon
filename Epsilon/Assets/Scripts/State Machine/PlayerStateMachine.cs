using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.InputSystem;
//using UnityEngine.SceneManagement;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Reference variables")]
    PlayerControls _playerControls;
    Rigidbody2D _rb;
    Animator _anim;
    public AudioManager audioManager;
    public RagdollController ragdoll;

    public LedgeInformation ledgeInfo;

    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    [Header("Camera")]
    public CameraManager camManager;

    [Header("Variables")]
    [SerializeField] float _moveSpeed = 4f; //4f before * by Time.Delta Time.
    [SerializeField] float _inAirMoveSpeedMultiplier;
    [SerializeField] float _rotationScaleAmount = 0.33f;
    [SerializeField] float _softLandingSpeedMultiplier = 0.75f;
    public bool canJump = true;

    [Header("Ground Checks")]  
    [SerializeField] bool _isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.2f;

    [Header("Player Input Variables")]    //variables to store player inputs
    Vector2 _currentMovementInput;
    Vector2 _currentMovement;
    [SerializeField] bool isMovementPressed;

    #region Animation Hashes
    int _isGroundedHash;
    int _isRunningHash;
    int _isFallingHash;
    int _jumpHash;
    //int speedXHash;
    #endregion

    [Header("Jump Variables")]
    [SerializeField] bool _isJumpPressed = false;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _gravityScaleWhenFalling = 2.5f;
    [SerializeField] float jumpReleaseDampener = 0.5f;
    //[SerializeField] float jumpReleasedMultiplier = 0.33f;
    public float jumpBufferCounter = 0f;
    public float jumpBufferTime = 0.2f;
    public float coyoteTimeCounter;
    public float coyoteTime = 0.4f;

    [Header("Falling & Velocity")]
    [SerializeField] float _fallingYAxisThreshold = -0.25f;
    public bool _hasLetGoOfLedge = false;
    [SerializeField] float _maxFallVelocity = 10f;

    [Header("Footsteps VFX")]    //Footsteps particle system
    public ParticleSystem _footEmission;
    //public ParticleSystem _footsteps;
    public ParticleSystem _impactEffect;
    public bool _wasOnGround;

    [Header("Ledge Hang")]
    public Transform wallCheck;
    public Transform ledgeCheck;
    public float playerLocalScaleOffset = 3f; // should equal 1 divided player scale reduction
    public bool isTouchingWall;
    public bool isTouchingLedge;
    public float wallCheckDistance;
    //public bool ledgeDetected = false;
    //public bool canDetectLedges = true;

    [Header("Mount")]
    public Transform kneeCheck;
    public bool isKneeTouchingLedge;
    public float mountPositionOffsetX;
    public float mountPositionOffsetY = 1.5f;

    [Header("Acceleration & Deacceleration")]
    //public float fHorizontalDamping;
    //public float fHorizontalVelocity;
    [Tooltip("Default = 10f. Higher values result in faster acceleration")]
    public float accelerationRate = 10f;
    [Tooltip("Default = 0.2f. Lower values result in faster deceleration")]
    public float decelerationRate = 0.5f;

    [Header("Jetpack")]
    public ParticleSystem _jetEmission;
    public float _jetPackMoveSpeed = 2f;
    public bool isThrustPressed = false;
    public float thrustForce;
    public float thrustCounter;
    public float thrustTime;
    public bool canJetpack = true;

    [Header("Player Health & Death")]
    public int playerHealth = 1;
    public float deadTimeBeforeRestart = 5f;

    [Header("Timeline & Cinematics")]
    public bool inCinematic = false;

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
    public float SoftLandingSpeedMultiplier { get { return _softLandingSpeedMultiplier; } }
    public float InAirSpeedMultiplier { get { return _inAirMoveSpeedMultiplier; } }
    public float FallingYAxisThreshold { get { return _fallingYAxisThreshold; } }
    public float MaxFallVelocity { get { return _maxFallVelocity; } }
    public float GravityScaleWhenFalling { get { return _gravityScaleWhenFalling; } }
    public float RotationScaleAmount {  get { return _rotationScaleAmount; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int JumpHash { get { return _jumpHash; } }
    public int IsFallingHash { get { return _isFallingHash; } }

    //public bool IsFalling { get { return _isFalling;  } }
    #endregion

    void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        ledgeInfo = GetComponent<LedgeInformation>();
        audioManager = FindObjectOfType<AudioManager>();
        ragdoll = FindObjectOfType<RagdollController>();

        //camera test
        camManager = FindObjectOfType<CameraManager>();

        //Controls calling OnMovement Input Function
        _playerControls.Gameplay.Move.started += OnMovementInput;
        _playerControls.Gameplay.Move.canceled += OnMovementInput;
        _playerControls.Gameplay.Move.performed += OnMovementInput;
        //_playerControls.Gameplay.Jump.started += OnJump; //Default
        //_playerControls.Gameplay.Jump.canceled += OnJump; //Default
        _playerControls.Gameplay.Jump.started += ctx => OnJump();
        _playerControls.Gameplay.Jump.canceled += ctx => OnJumpReleased();

        _playerControls.Gameplay.Jetpack.performed += ctx => Thrust();
        _playerControls.Gameplay.Jetpack.canceled += ctx => ThrustReleased();

        //Converting strings to hash is more performant
        _isGroundedHash = Animator.StringToHash("isGrounded");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isFallingHash = Animator.StringToHash("isFalling");
        _jumpHash = Animator.StringToHash("Jump");
        //speedXHash = Animator.StringToHash("SpeedX");

        // setup state
        _states = new PlayerStateFactory(this); // passes this PlayerStateMachine instance. PlayerStateFactory is expecting a PlayerStateMachine 
                
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    void Start()
    {
        //experimenting - swapping out sprites programmatically
        GetComponent<SpriteResolver>().SetCategoryAndLabel("Player", "JetpackOn"); //TODO strings bad slow
    }

    void Update()
    {
        //Ground Check
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Update Animator
        _anim.SetBool(_isGroundedHash, _isGrounded);
        _anim.SetFloat("SpeedX", Mathf.Abs(Rigidbody.velocity.x));
        if (_isGrounded) Animator.SetBool("isLettingGoLedge", false);

        _currentState.UpdateStates();
        JumpLogic();
        PlayLandingImpactVFX();

        if (_isGrounded) 
        {
            _hasLetGoOfLedge = false;


            canJetpack = true;
            thrustCounter = thrustTime;
        } 

        //RotateSprite();
    }

    void FixedUpdate()
    {
        _currentState.UpdateFixedUpdateStates();
    }

    void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _currentMovementInput = ctx.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.y = _currentMovementInput.y;
        isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    /*void OnJump(InputAction.CallbackContext ctx)
    {
        if (jumpBufferTime > 0f && IsGrounded)
        {
            _isJumpPressed = ctx.ReadValueAsButton();
        }
        jumpBuffer = jumpBufferTime;
    }*/

    void OnJump()
    {
        if (coyoteTimeCounter > 0f)
        {
            _isJumpPressed = true;
        }

        jumpBufferCounter = jumpBufferTime;
    }

    void OnJumpReleased()
    {
        _isJumpPressed = false;
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y * jumpReleaseDampener);
    }

    private void JumpLogic()
    {
        jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f && _isGrounded)
        {
            _isJumpPressed = true;
        }
        else if (jumpBufferCounter < 0 && !_isGrounded)       
        {
            _isJumpPressed = false;
        }

        coyoteTimeCounter -= Time.deltaTime;

        if (_isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
    }

    private void Thrust()
    {
        isThrustPressed = true;
    }

    private void ThrustReleased()
    {
        isThrustPressed = false;
        if (audioManager != null) audioManager.jetpackLoop.Stop();
    }

    private void PlayLandingImpactVFX()
    {
        if (!_wasOnGround && _isGrounded)
        {
            _impactEffect.Play();
        }
        _wasOnGround = _isGrounded;
    }

    public void TeleportPlayerAfterLedgeClimb()
    {
        if(ledgeInfo._currentEndPoint != null) transform.position = ledgeInfo._currentEndPoint.position;
        Rigidbody.simulated = true;
        Rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Player hit collider: " + collision.gameObject.name);

        //if we hit spikes, change, update state and enter death state
        if (collision.gameObject.CompareTag("Spikes"))
        {
            _currentState = _states.Death();
            _currentState.EnterState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Player exit collider: " + collision.gameObject.name);
    }

    //make player a child of moving platforms
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.transform.position, groundCheckRadius);
    }

    /*void RotateSprite()
    {
        //rotate sprite when moving left and right
        if (Rigidbody.velocity.x > 0.5f)
        {
            transform.localScale = new Vector3(RotationScaleAmount, RotationScaleAmount, transform.localScale.z);
        }
        else if (Rigidbody.velocity.x < -0.5f)
        {
            transform.localScale = new Vector3(-RotationScaleAmount, RotationScaleAmount, transform.localScale.z);
        }
    }*/
}
