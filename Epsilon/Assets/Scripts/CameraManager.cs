using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Player Climbing Camera")]
    public CinemachineVirtualCamera cam1; 
    public CinemachineVirtualCamera camTarget;

    [Header("Player Look Camera")]
    public CinemachineVirtualCamera camLookUp;
    public CinemachineVirtualCamera camLookDown;

    public Transform player;
    public Transform target;

    public CinemachineVirtualCamera helperCam;

    public bool isCameraTargetPlayer = true;
    public bool isFocusingOnHelper;

    // Start is called before the first frame update
    void Start()
    {
        //cam1.gameObject.SetActive(true);

        helperCam.Priority = 10;
    }

    // Update is called once per frame
    void Update()
    {
        SetCamerasDuringLedgeClimb();
        SetPlayerLookCameras();

        if (Input.GetKeyDown(KeyCode.H))
        {
            helperCam.Priority = 100;
            cam1.Priority = 10;
            isFocusingOnHelper = true;
            FindObjectOfType<AudioManager>().PlayHelperAudio(); //TODO audio - this needs to move
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            helperCam.Priority = 10;
            cam1.Priority = 100;
            isFocusingOnHelper = false;
        }
    }

    private void SetPlayerLookCameras()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            camLookUp.Priority = 100;
        }
        else
        {
            camLookUp.Priority = 10;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            camLookDown.Priority = 100;
        }
        else
        {
            camLookDown.Priority = 10;
        }
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
}
