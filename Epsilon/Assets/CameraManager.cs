using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera camTarget;

    public Transform player;
    public Transform target;

    public bool isCameraTargetPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        //cam1.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraTargetPlayer)
        {
            //cam1.Follow = player;
            cam1.gameObject.SetActive(true);
            camTarget.gameObject.SetActive(false);
        }
        else if (!isCameraTargetPlayer)
        {
            cam1.gameObject.SetActive(false);
            camTarget.gameObject.SetActive(true);
        }
    }
}
