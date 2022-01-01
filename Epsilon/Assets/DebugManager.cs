using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    public TMP_Text playerStateDebug;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerStateDebug.text = "Player State: " + FindObjectOfType<PlayerMovement>().playerCurrentState.ToString();
    }
}
