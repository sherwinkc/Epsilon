﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public LevelManager levelMan;
    public Transform newRespawnPos;

    // Start is called before the first frame update
    void Start()
    {
        levelMan = FindObjectOfType<LevelManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            levelMan.respawnPosition = newRespawnPos;
        }
    }
}
