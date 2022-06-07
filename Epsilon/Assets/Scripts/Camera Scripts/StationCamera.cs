using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StationCamera : MonoBehaviour
{
    CameraManager cameraManager;

    public CinemachineVirtualCamera mainPlayerCam;
    public CinemachineVirtualCamera stationCam;

    //TODO disable trigger when puzzle is complete
    //[SerializeField] bool disableTriggerOncePuzzleIsComplete;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entered Collider: " + collision.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            stationCam.Priority = 200;
            cameraManager.isInAStationaryCam = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Exited Collider: " + collision.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            stationCam.Priority = 10;
            cameraManager.isInAStationaryCam = false;
        }
    }
}
