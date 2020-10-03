﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCollectable : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Collision Detected");
            player.currentAir = player.maxAir;
            Destroy(gameObject);
        }
    }
}
