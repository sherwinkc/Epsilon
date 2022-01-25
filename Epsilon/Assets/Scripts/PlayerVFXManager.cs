using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerVFXManager : MonoBehaviour
{
    //PlayerStateMachine playerStateMachine;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteShapeRenderer spriteShapeRenderer;

    public Transform feetPosition;
    public float distance;
    public bool isReturningGround;

    private void Awake()
    {
        //playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        ShootRaycastForMaterialDetector();
        RaycastDebug();
    }

    private void ShootRaycastForMaterialDetector()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(feetPosition.position, -transform.up, distance);
        //if (hitInfo) Debug.Log("name: " + hitInfo.transform.name);
        //if (hitInfo) Debug.Log("tag: " + hitInfo.transform.tag);

        if (hitInfo)
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) ;
            {
                if (hitInfo) spriteRenderer = hitInfo.transform.GetComponent<SpriteRenderer>();
                if (hitInfo) spriteShapeRenderer = hitInfo.transform.GetComponent<SpriteShapeRenderer>();

                //Debug.Log(spriteRenderer);
                //Debug.Log(spriteShapeRenderer);
            }
        }



        //isReturningGround = Physics2D.Raycast(feetPosition.position, -transform.up, distance, playerStateMachine.whatIsGround);
        //_ctx.isTouchingWall = Physics2D.Raycast(_ctx.wallCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);
        //_ctx.isTouchingLedge = Physics2D.Raycast(_ctx.ledgeCheck.position, _ctx.transform.right * (_ctx.transform.localScale.x * _ctx.playerLocalScaleOffset), _ctx.wallCheckDistance, _ctx.whatIsGround);

    }

    private void RaycastDebug()
    {
        Debug.DrawRay(feetPosition.position, (-Vector2.up * distance), Color.white);
    }
}
