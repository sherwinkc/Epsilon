using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float time;

    public void Destroy()
    {
        Destroy(this.gameObject, time);
    }
}
