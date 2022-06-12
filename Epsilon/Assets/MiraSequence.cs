using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraSequence : MonoBehaviour
{
    CameraManager cameraManager;
    PlayerStateMachine playerStateMachine;

    [SerializeField] BoxCollider2D trigger;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();

        trigger = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with player");
            trigger.enabled = false;

            StartCoroutine(StartMiraSequence());            
        }
    }

    public IEnumerator StartMiraSequence()
    {
        cameraManager.FocusMiraCam();
        playerStateMachine.inCinematic = true;

        yield return new WaitForSeconds(16f);

        cameraManager.ResetMiraCam();
        playerStateMachine.inCinematic = false;
    }
}
