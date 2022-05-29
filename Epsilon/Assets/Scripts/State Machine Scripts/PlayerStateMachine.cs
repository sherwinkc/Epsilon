using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Reference variables")]
    PlayerControls _playerControls;
    Rigidbody2D _rb;
    Animator _anim;
    
    public Interact interact; //TODO - This is for playeridlestate when raycast hits box. Don't like accessing interact script just to display HUD tooltip 
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
    [Tooltip("Default = 10f. Higher values result in faster acceleration")]
    public float _inAirAccelerationRate = 7.5f; //TODO change to serializefield
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
    public Slider thrustImage;
    public ParticleSystem _jetEmission;
    public float _jetPackMoveSpeed = 2f;
    public bool isThrustPressed = false;
    public float thrustForce;
    public float thrustCounter;
    public float thrustTime;
    public bool canJetpack = true;
    public bool isJetpackOn = true; //this is used for preventing player using jetpack totally
    public bool isJetpackVisible = true;
    public bool regenerateThrust;
    public float regenerateThrustSpeed;
    [SerializeField] float delayThrustRegenerationSpeed = 1f;
    public Vector3 impulseJetpack; //TODO not currently used

    public GameObject boostDetails;

    [Header("Player Health & Death")]
    public int playerHealth = 1;
    public float deadTimeBeforeRestart = 5f;

    [Header("Timeline & Cinematics")]
    public bool inCinematic = false;

    [Header("Push & Pull Boxes")]
    public GameObject box;
    public RaycastHit2D hit;
    public Transform wallCheckForBox;
    public float moveSpeedWhileGrabbing = 2f;
    public float boxCheckDistance;

    //For Collision Debug
    public Transform collisionTransform;

    [Header("Screenshake")]
    CinemachineImpulseSource impulse;

    public bool isFacingRight;

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
        interact = GetComponent<Interact>();

        //camera test
        camManager = FindObjectOfType<CameraManager>();

        //ScreenShake
        impulse = FindObjectOfType<CinemachineImpulseSource>();

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
        EnableDisableJetpackSprite();

        impulseJetpack = new Vector3(0f, 20f, 0f);
    }

    void Update()
    {
        //Ground Check
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        CheckWhichWayPlayerIsFacing(); //TODO we don't need to check which way player is facing every frame. Only when player actually changes direction.

        //Update Animator
        _anim.SetBool(_isGroundedHash, _isGrounded);
        _anim.SetFloat("SpeedX", Mathf.Abs(Rigidbody.velocity.x));
        _anim.SetFloat("VelocityX", Rigidbody.velocity.x);

        if (_isGrounded) Animator.SetBool("isLettingGoLedge", false);

        _currentState.UpdateStates();
        JumpLogic();
        PlayLandingImpactVFX();

        if (_isGrounded) 
        {
            _hasLetGoOfLedge = false;
            canJetpack = true;
            StopCoroutine(DelayThrustRegen());
            StartCoroutine(DelayThrustRegen());

        }
        else
        {
            regenerateThrust = false; //TODO player only regenerates thrust when on the ground? Yay or Nay
            StopCoroutine(DelayThrustRegen());
        }

        //Thrust Logic
        if (regenerateThrust)
        {
            if (thrustCounter < thrustTime)
            {
                thrustCounter += Time.deltaTime * regenerateThrustSpeed;
            }
        }

        thrustImage.value = thrustCounter * 100;
    }

    private void EnableDisableJetpackSprite()
    {
        //experimenting - swapping out sprites programmatically
        /*if (isJetpackVisible)
        {
            GetComponent<UnityEngine.U2D.Animation.SpriteResolver>().SetCategoryAndLabel("Player", "JetpackOn"); //TODO strings bad slow        
        }
        else
        {
            GetComponent<UnityEngine.U2D.Animation.SpriteResolver>().SetCategoryAndLabel("Player", "JetpackOff"); //TODO strings bad slow   
            GetComponent<UnityEngine.U2D.Animation.SpriteResolver>().SetCategoryAndLabel("Player", "Entry"); //TODO strings bad slow   
        }*/

        //GetComponent<UnityEngine.U2D.Animation.SpriteResolver>().SetCategoryAndLabel("NewPlayer", "No Eyes");
    }

    private void CheckWhichWayPlayerIsFacing()
    {
        if (transform.localScale.x > 0)
        {
            _anim.SetBool("isLookingRight", true);
            isFacingRight = true;
        }
        else
        {
            _anim.SetBool("isLookingRight", false);
            isFacingRight = false;
        }
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

    private IEnumerator DelayThrustRegen()
    {
        yield return new WaitForSeconds(delayThrustRegenerationSpeed);

        regenerateThrust = true;
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

    public void FadeScreen()
    {
        ScreenFadeManager screenFadeManager = FindObjectOfType<ScreenFadeManager>();

        if (screenFadeManager != null)
        {
            screenFadeManager.TurnOnAnimatorAndFadeOut();
        }
    }

    public void ActivateDeathCam()
    {
        FindObjectOfType<CameraManager>().deathCam.Priority = 500;
    }

    public void DeactivateDeathCam()
    {
        FindObjectOfType<CameraManager>().deathCam.Priority = 10;
    }

    //make player a child of moving platforms
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Remove me in final Builds
        //Debug.Log(collision.transform.name);
        //collisionTransform = collision.transform; //Debug Only Remove me TODO

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }

        if (collision.gameObject.CompareTag("Lift"))
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

        if (collision.gameObject.CompareTag("Lift"))
        {
            transform.parent = null;
        }
    }

    public void Respawn()
    {
        GameCheckpointManager checkpointManager = FindObjectOfType<GameCheckpointManager>(); //TODO Cache this

        transform.position = checkpointManager.currentActiveSpawnPoint.transform.position;

        DeactivateDeathCam();

        //_rb.velocity = Vector2.zero;

        ScreenFadeManager screenFadeManager = FindObjectOfType<ScreenFadeManager>(); //TODO Cache this (Also Above)

        if (screenFadeManager != null)
        {
            screenFadeManager.FadeIn();
        }

        audioManager.playerBreathingSFX.Play();

        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    public void Screenshake(float force)
    {
        if (impulse != null) 
        {
            impulse.GenerateImpulse(force);
        }
    }

    public void EnterIdleState()
    {
        _currentState = _states.Idle();
        _currentState.EnterState();
        _anim.Play("Player_Idle");
    }

    public void EnterCinematicState()
    {
        inCinematic = true;
        _currentState = _states.InCinematic();
        _currentState.EnterState();
    }

    private void OnEnable()
    {
        EnableGameplayControls();
    }
    private void OnDisable()
    {
        DisableGameplayControls();
    }
    public void EnableGameplayControls()
    {
        _playerControls.Gameplay.Enable();
    }
    public void DisableGameplayControls()
    {
        _playerControls.Gameplay.Disable();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
