using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMovement : MonoBehaviour
{
    Vector2 destination;
    [SerializeField] GameObject idleDestinationTransform;

    //Pickup
    public GameObject objectToPickUp;
    [SerializeField] GameObject depositPosition;

    //move speed
    [Tooltip("Lower numbers result in a longer easing time")]
    [SerializeField] float moveSpeed;
    [SerializeField] float movePickupSpeed = 0.1f;

    public bool isPickingUpItem = false;
    public bool isDepositingToRover = false;
    public bool isCarryingBattery = false;

    void Start()
    {
        
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            isPickingUpItem = !isPickingUpItem;
        }*/

        /*if (Input.GetKeyDown(KeyCode.B))
        {
            isDepositingToRover = !isDepositingToRover;
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPickingUpItem && objectToPickUp != null) 
        {
            transform.position = Vector2.MoveTowards(transform.position, objectToPickUp.transform.position, movePickupSpeed);
        }
        else if (isDepositingToRover && depositPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, depositPosition.transform.position, movePickupSpeed);
        }
        else
        {
            StayWithPlayerLerpFunction();
        }
    }

    private void StayWithPlayerLerpFunction()
    {
        //poisiton above player head
        destination.x = idleDestinationTransform.transform.position.x;
        destination.y = idleDestinationTransform.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, destination, moveSpeed * Time.deltaTime);
    }
}
