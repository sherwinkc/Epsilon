using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSprite : MonoBehaviour
{
    //this script turns the helper left and right by setting its scale

    Transform player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>().transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-0.075f, 0.075f, 1f);
        }
        else if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(0.075f, 0.075f, 1f);
        }
    }
}
