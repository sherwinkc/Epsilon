using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeInformation : MonoBehaviour
{
    public Transform _currentGrabPoint;
    public Transform _currentEndPoint;

    public bool isNearClimbableMesh; // this checks weather player is near a climable box. See Player Jump State CheckSwitchStates()

    void Start()
    {
        _currentGrabPoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftSideLedge") || collision.gameObject.CompareTag("RightSideLedge"))
        {
            //Debug.Log(collision.tag);
            _currentGrabPoint = collision.GetComponentInChildren<GrabPoint>().transform;
            _currentEndPoint = collision.GetComponentInChildren<EndPoint>().transform;

            isNearClimbableMesh = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftSideLedge") || collision.gameObject.CompareTag("RightSideLedge"))
        {
            //_currentGrabPoint = null;
            //_currentEndPoint = null;
            if (_currentGrabPoint == null && _currentEndPoint == null) Debug.Log("Current Grab Point Is Null!");

            isNearClimbableMesh = false;
        }
    }
}