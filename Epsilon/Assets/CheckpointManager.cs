using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;

    [SerializeField] int checkpointNumber;

    public Transform[] checkpoints;
    public Transform activeTransform;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        activeTransform = checkpoints[checkpointNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

            if (checkpointNumber < checkpoints.Length) 
            {
                playerStateMachine.transform.position = activeTransform.position;
                checkpointNumber++;
                activeTransform = checkpoints[checkpointNumber];
            } 
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (checkpointNumber != 0)
                {
                    playerStateMachine.transform.position = activeTransform.position;
                    checkpointNumber--;
                    activeTransform = checkpoints[checkpointNumber];
                }  
            }
        }
    }
}
