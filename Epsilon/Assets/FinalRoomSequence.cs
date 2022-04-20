using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoomSequence : MonoBehaviour
{
    PlayerStateMachine player;
    public Animator anim;

    [SerializeField] Transform teleportTo;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>();
    }    
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.transform.position = teleportTo.transform.position;

            anim.Play("Player_LyingDown");
        }
    }
}
