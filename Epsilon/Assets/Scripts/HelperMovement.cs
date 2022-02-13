using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMovement : MonoBehaviour
{
    Vector2 destination;
    [SerializeField] GameObject destinationTransform;
    [SerializeField] GameObject objectToPickUp;
    [SerializeField] GameObject roverPosition;

    [Tooltip("Lower numbers result in a longer easing time")]
    [SerializeField] float moveSpeed;
    [SerializeField] float movePickupSpeed = 0.1f;

    public bool isPickingUpItem = false;
    public bool isDepositingToRover = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isPickingUpItem = !isPickingUpItem;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            isDepositingToRover = !isDepositingToRover;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPickingUpItem && objectToPickUp != null) 
        {
            transform.position = Vector2.MoveTowards(transform.position, objectToPickUp.transform.position, movePickupSpeed);
        }
        else if (isDepositingToRover && roverPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, roverPosition.transform.position, movePickupSpeed);
        }
        else
        {
            StayWithPlayerLerpFunction();
        }
    }

    private void StayWithPlayerLerpFunction()
    {
        //poisiton above player head
        destination.x = destinationTransform.transform.position.x;
        destination.y = destinationTransform.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, destination, moveSpeed * Time.deltaTime);
    }
}
