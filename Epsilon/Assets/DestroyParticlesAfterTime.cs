using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesAfterTime : MonoBehaviour
{
    public float time;

    private void Start()
    {
        Destroy();
    }

    public void Destroy()
    {
        Destroy(this.gameObject, time);
    }
}
