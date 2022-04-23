using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] GameObject debug;
    public bool enableDebug;

    void Start()
    {
        EnableOrDisableDebug();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            enableDebug = !enableDebug;
            EnableOrDisableDebug();
        }

        //TODO add FPS toggle on F2
        /*if (Input.GetKeyDown(KeyCode.F2))
        {

        }*/
    }

    private void EnableOrDisableDebug()
    {
        if (enableDebug)
        {
            debug.SetActive(true);
        }
        else
        {
            debug.SetActive(false);
        }
    }
}
