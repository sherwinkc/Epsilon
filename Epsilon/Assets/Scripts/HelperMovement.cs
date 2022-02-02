using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMovement : MonoBehaviour
{
    Vector2 destination;
    [SerializeField] GameObject destinationTransform;

    [Tooltip("Lower numbers result in a longer easing time")]
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LerpFunction();
    }

    private void LerpFunction()
    {
        //poisiton above player head
        destination.x = destinationTransform.transform.position.x;
        destination.y = destinationTransform.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, destination, moveSpeed * Time.deltaTime);
    }
}
