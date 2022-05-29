using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBeam : MonoBehaviour
{
    [SerializeField] float destroyTime = 1.5f;

    void Start()
    {
        Invoke("DestroyPowerBeams", destroyTime);
    }

    private void DestroyPowerBeams()
    {
        Destroy(gameObject);
    }
}
