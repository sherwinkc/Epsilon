using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMovement : MonoBehaviour
{
    Vector2 destination;
    CircleCollider2D circleCollider;
    [SerializeField] GameObject idleDestinationTransform;

    //Pickup
    public GameObject objectToPickUp;
    //[SerializeField] GameObject depositPosition;
    public Transform depositTransform;

    //move speed
    [Tooltip("Lower numbers result in a longer easing time")]
    [SerializeField] float moveSpeed;
    [SerializeField] float movePickupSpeed = 0.1f;

    public bool isPickingUpItem = false;
    public bool isDepositingToRover = false;
    public bool isCarryingBattery = false;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();    
    }

    void Start()
    {
        depositTransform = null;
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
            EnableCircleCollider();
        }
        else if (isDepositingToRover && depositTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, depositTransform.position, movePickupSpeed);
            //EnableCircleCollider();
        }
        else
        {
            StayWithPlayerLerpFunction();
        }
    }

    //TODO - Don't want to check this every frame
    private void EnableCircleCollider() 
    {
        circleCollider.enabled = true;
    }

    private void StayWithPlayerLerpFunction()
    {
        //poisiton above player head
        destination.x = idleDestinationTransform.transform.position.x;
        destination.y = idleDestinationTransform.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, destination, moveSpeed * Time.deltaTime);

        //TODO - Don't want to check this every frame
        circleCollider.enabled = false;
    }
}
