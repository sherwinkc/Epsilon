using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingTexture : MonoBehaviour
{
    Vector2 origOffset;
    public Material rend;

    public float scrollSpeed = 0.5f;
    public float downSpeed = 0.0f;
    public bool randomizeAtStart;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>().material;
        //if (randomizeAtStart) origOffset = rend.material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        rend.SetTextureOffset("_MainTex", new Vector2(Time.deltaTime * scrollSpeed, downSpeed));
    }
}
