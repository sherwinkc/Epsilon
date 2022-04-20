using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    //Input
    PlayerControls controls;

    [SerializeField] Transform playerTransform;

    [Header("Player Climbing Camera")]
    public CinemachineVirtualCamera cam1; 
    public CinemachineVirtualCamera camTarget;
    public CinemachineTargetGroup targetGroup;

    [Header("Player Look Camera")]
    public CinemachineVirtualCamera camLookUp;
    public CinemachineVirtualCamera camLookDown;

    public Transform player;
    public Transform target;

    public CinemachineVirtualCamera helperCam;

    public bool isCameraTargetPlayer = true;
    public bool isFocusingOnHelper;

    public CinemachineVirtualCamera deathCam;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.LookUp.performed += ctx => LookUp();
        controls.Gameplay.LookUp.canceled += ctx => LookUpReleased();
        controls.Gameplay.LookDown.performed += ctx => LookDown();
        controls.Gameplay.LookDown.canceled += ctx => LookDownReleased();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCamerasDuringLedgeClimb();

        /*//Debug for Helper cam TODO rework this
        if (Input.GetKeyDown(KeyCode.H))
        {
            helperCam.Priority = 100;
            cam1.Priority = 10;
            isFocusingOnHelper = true;
            FindObjectOfType<AudioManager>().PlayHelperAudio(); //TODO audio - this needs to move
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            helperCam.Priority = 10;
            cam1.Priority = 100;
            isFocusingOnHelper = false;
        }*/         

        //mouse and keyboard
        CheckKeyboardInputs(); //TODO this is overriding the controller inputs
    }

    private void CheckKeyboardInputs()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            camLookUp.Priority = 100;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            camLookUp.Priority = 10;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            camLookDown.Priority = 100;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            camLookDown.Priority = 10;
        }
    }

    private void LookUp()
    {
        camLookUp.Priority = 100;
    }

    private void LookUpReleased()
    {
        camLookUp.Priority = 10;
    }

    private void LookDown()
    {
        camLookDown.Priority = 100;
    }

    private void LookDownReleased()
    {
        camLookDown.Priority = 10;
    }

    private void SetCamerasDuringLedgeClimb()
    {
        if (!isFocusingOnHelper)
        {
            if (isCameraTargetPlayer)
            {
                //cam1.Follow = player;
                //cam1.gameObject.SetActive(true);
                //camTarget.gameObject.SetActive(false);

                cam1.Priority = 100;
                camTarget.Priority = 10;
            }
            else if (!isCameraTargetPlayer)
            {
                //cam1.gameObject.SetActive(false);
                //camTarget.gameObject.SetActive(true);

                cam1.Priority = 10;
                camTarget.Priority = 100;
            }
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}