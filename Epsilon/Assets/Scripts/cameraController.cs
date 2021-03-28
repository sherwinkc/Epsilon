using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    public PlayerMovement playerMov;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    // Start is called before the first frame update
    void Start()
    {
        playerMov = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!playerMov.isGrounded && playerMov.canMove)
        {
            cam1.enabled = false;
            cam2.enabled = true;
        }
        else
        {
            cam1.enabled = true;
            cam2.enabled = false;
        }*/
    }
}
