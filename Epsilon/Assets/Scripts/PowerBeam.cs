using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBeam : MonoBehaviour
{
    [SerializeField] float destroyTime = 1.5f;

    private void Awake()
    {

    }

    void Start()
    {
        Invoke("DestroyPowerBeams", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DestroyPowerBeams()
    {
        Destroy(gameObject);
    }
}
