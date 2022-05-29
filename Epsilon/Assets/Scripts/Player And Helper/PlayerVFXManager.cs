using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerVFXManager : MonoBehaviour
{
    public Transform feetPosition;
    public float distance;
    public AudioMaterial material;

    public RaycastHit2D hitInfo;

    void Update()
    {
        Invoke("ShootRaycastDownToDetectMaterial", 0.5f);
        RaycastDebug();
    }

    private void ShootRaycastDownToDetectMaterial()
    {
        hitInfo = Physics2D.Raycast(feetPosition.position, -transform.up, distance);

        //If the collider of the object hit is not NUll
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Sand"))
            {
                material = AudioMaterial.Sand;
            }
            else if (hitInfo.collider.gameObject.CompareTag("Metal"))
            {
                material = AudioMaterial.Metal;
            }
            else if (hitInfo.collider.gameObject.CompareTag("ClimbableMesh"))
            {
                material = AudioMaterial.Metal;
            }
            else if (hitInfo.collider.gameObject.CompareTag("Lift"))
            {
                material = AudioMaterial.Metal;
            }
            else
            {
                material = AudioMaterial.Sand;
            }
        }

        //Debug.Log(hitInfo.collider);
        //Debug.Log(hitInfo.collider.name);

        /*if (hitInfo.collider.GetComponent<MaterialType>() != null)
        {
            material = hitInfo.collider.gameObject.GetComponent<MaterialType>().audioMaterial;
        }*/
    }

    private void RaycastDebug()
    {
        Debug.DrawRay(feetPosition.position, (-Vector2.up * distance), Color.white);
    }
}
