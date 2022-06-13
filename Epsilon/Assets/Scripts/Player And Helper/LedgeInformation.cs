using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeInformation : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;

    public Transform _currentGrabPoint;
    public Transform _currentEndPoint;

    public bool isNearClimbableMesh; // this checks weather player is near a climable box. See Player Jump State CheckSwitchStates()

    public bool isPlayerLeftSideOfMesh, isPlayerRightSideOfMesh;

    private void Awake()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    void Start()
    {
        _currentGrabPoint = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftSideLedge"))
        {
            //Debug.Log(collision.tag);
            _currentGrabPoint = collision.GetComponentInChildren<GrabPoint>().transform;
            playerStateMachine.canClimbUp = collision.GetComponentInChildren<GrabPoint>().hasAnExitPoint;

            _currentEndPoint = collision.GetComponentInChildren<EndPoint>().transform;

            isNearClimbableMesh = true;
            isPlayerLeftSideOfMesh = true;
            isPlayerRightSideOfMesh = false;
        }
        else if (collision.gameObject.CompareTag("RightSideLedge"))
        {
            //Debug.Log(collision.tag);
            _currentGrabPoint = collision.GetComponentInChildren<GrabPoint>().transform;
            playerStateMachine.canClimbUp = collision.GetComponentInChildren<GrabPoint>().hasAnExitPoint;

            _currentEndPoint = collision.GetComponentInChildren<EndPoint>().transform;

            isNearClimbableMesh = true;
            isPlayerLeftSideOfMesh = false;
            isPlayerRightSideOfMesh = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftSideLedge") || collision.gameObject.CompareTag("RightSideLedge"))
        {
            //MakeGrabPointsNull();

            if (_currentGrabPoint == null && _currentEndPoint == null) Debug.Log("Current Grab Point Is Null!");

            isNearClimbableMesh = false;
            isPlayerLeftSideOfMesh = false;
            isPlayerRightSideOfMesh = false;
        }
    }

    public void MakeGrabPointsNull() //Not sure why we are not using this
    {
        //_currentGrabPoint = null;
        //_currentEndPoint = null;
    }
}
