using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMovement : MonoBehaviour
{
    public float moveSpeed = 0.01f;

    void Update()
    {
        transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.y);
    }
}
