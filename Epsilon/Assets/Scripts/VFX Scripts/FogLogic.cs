using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogLogic : MonoBehaviour
{
    public float scrollSpeed = 0.0005f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - scrollSpeed, transform.position.y);
    }
}
