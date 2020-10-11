using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyScript : MonoBehaviour
{
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            sr.color = new Color(1f, 1f, 1f, 0.33f);
        }
    }
}
