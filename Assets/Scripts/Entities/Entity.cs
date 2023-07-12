using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected SpriteRenderer sprite;
    public Rigidbody2D rb;

    public Rigidbody2D Rigid => rb;

    protected bool isGrounded, isLeftWallDetected, isRightWallDetected;
    public bool IsGrounded => isGrounded;
    public bool IsLeftWallDetected => isLeftWallDetected;
    public bool IsRightWallDetected => isRightWallDetected;

    protected int facingDir = 1;
    public int FacingDir => facingDir;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void Flip(bool v)
    {
        facingDir = v ? 1 : -1;
        sprite.flipX = !v;
    }

    public virtual void SetGroundedState(bool v)
    {
        isGrounded = v;
    }

    public virtual void OnLeftSideWallTouch(bool v)
    {
        isLeftWallDetected = v;
    }

    public virtual void OnRightSideWallTouch(bool v)
    {
        isRightWallDetected = v;
    }
}
