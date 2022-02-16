using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StationCamera : MonoBehaviour
{
    public CinemachineVirtualCamera mainPlayerCam;
    public CinemachineVirtualCamera stationCam;

    //TODO disable trigger when puzzle is complete
    //[SerializeField] bool disableTriggerOncePuzzleIsComplete;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainPlayerCam.Priority = 10;
            stationCam.Priority = 100;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainPlayerCam.Priority = 100;
            stationCam.Priority = 10;
        }
    }
}
