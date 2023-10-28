using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Entity
{
    [SerializeField] private EntityStats stats;
    [SerializeField] private Transform groundCheck;

    private Vector2 groundCheckStartPosition;
    [Space]

    [Header("Player Detection")]
    [SerializeField] private Transform playerDetection;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask playerLayer;

    private RaycastHit2D playerRayHit;
    private bool seePlayer = false;

    private bool isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        groundCheckStartPosition = groundCheck.transform.localPosition;
    }

    protected override void Update()
    {
        base.Update();

        seePlayer = CheckPlayer(Vector2.right);
        //CheckPlayer(Vector2.right);

        if (seePlayer)
        {
            if (playerRayHit.distance > 1)
            {
                rb.velocity = new Vector2(stats.speed * 1.5f * facingDir, rb.velocity.y);

                Debug.Log("Player detected");
                isAttacking = false;
            }
            else
            {
                Debug.Log("Attack");
                isAttacking = true;
            }
        }
        else

            Movement();

        if (!isGrounded) Flip(false);
        if (isLeftWallDetected) Flip(true);
        if (isRightWallDetected) Flip(false);
    }

    private void Movement()
    {
        if(!isAttacking) rb.velocity = new Vector2(stats.speed * facingDir, rb.velocity.y);
    }

    protected override void Flip(bool v)
    {
        base.Flip(v);

        groundCheck.localPosition = v ? groundCheckStartPosition : -groundCheckStartPosition;
    }

    private bool CheckPlayer(Vector2 dir)
    {
        playerRayHit = Physics2D.Raycast(playerDetection.position, dir, facingDir * playerDetectionDistance, playerLayer);

        if(playerRayHit.transform != null) 
            return playerRayHit.transform.TryGetComponent(out PlayerMovement player);
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(playerDetection.position, new Vector2
            (transform.position.x + playerDetectionDistance * facingDir, transform.position.y), Color.blue);
    }
}
