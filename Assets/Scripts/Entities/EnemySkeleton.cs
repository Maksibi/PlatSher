using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Entity
{
    [SerializeField] private EntityStats stats;
    [SerializeField] private Transform groundCheck;

    private Vector2 groundCheckStartPosition;

    protected override void Start()
    {
        base.Start();
        groundCheckStartPosition = groundCheck.transform.localPosition;
    }

    protected override void Update()
    {
        base.Update();

        rb.velocity = new Vector2(stats.speed * facingDir, rb.velocity.y);

        if (!isGrounded) Flip(false);
        if (isLeftWallDetected) Flip(true);
        if (isRightWallDetected) Flip(false);
    }

    protected override void Flip(bool v)
    {
        base.Flip(v);

        groundCheck.localPosition = v ? groundCheckStartPosition : -groundCheckStartPosition;
    }

}
