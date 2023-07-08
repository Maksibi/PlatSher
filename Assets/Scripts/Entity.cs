using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isGrounded, isLeftWallDetected, isRightWallDetected;

    protected int facingDir = 1;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
