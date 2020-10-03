using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    public Animator animator;

    //Pushing/Pulling boxes
    public bool isCloseEnoughToPush;
    public LayerMask whatIsMovable;
    public float isCloseEnoughToPushDistance;

    public Transform wallCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D isCloseEnoughToPush = Physics2D.Raycast(wallCheck.position, transform.right * transform.localScale.x, isCloseEnoughToPushDistance, whatIsMovable);

        if (isCloseEnoughToPush)
        {
            Rigidbody2D boxRB = isCloseEnoughToPush.rigidbody.GetComponent<Rigidbody2D>();

            if (boxRB != null && Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("in function");
                //boxRB.AddForce(new Vector2(1000f, 0f));
                boxRB.velocity = new Vector2(boxRB.velocity.x + 10f, boxRB.velocity.y);
            }

            if (boxRB != null && Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("in function");
                //boxRB.AddForce(new Vector2(1000f, 0f));
                boxRB.velocity = new Vector2(boxRB.velocity.x - 10f, boxRB.velocity.y);
            }

            animator.SetBool("stopPushing", false);
            animator.SetTrigger("pushObject");
        }
    }
}
