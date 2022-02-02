using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    [SerializeField] bool isDecreasingInSize = false;
    [SerializeField] float sizeDecreaseRate = 0.5f;

    private void Update()
    {
        if (transform.localScale.x < 0)
        {
            Destroy(this.gameObject);
        }

        if (isDecreasingInSize)
        {
            transform.localScale = new Vector2(transform.localScale.x - sizeDecreaseRate, transform.localScale.y - sizeDecreaseRate);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDecreasingInSize = true;
        }
    }
}
