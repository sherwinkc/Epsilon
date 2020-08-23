using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public float offset;
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //offset valu
        offset = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((cam.transform.position.x * parallaxSpeed) + offset, transform.position.y, transform.position.z);
    }
}
