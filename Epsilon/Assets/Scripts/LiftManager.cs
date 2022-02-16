using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.01f;
    public bool isMoving = false; 

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
        }
    }
}
