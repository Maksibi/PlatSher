using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    IEnumerator RollCoroutine()
    {
        isRolling = true;
        yield return new WaitForSeconds(rollLength);
        isRolling = false;
    }

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [Header("Movement stats")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 12;
    [SerializeField] private float slideMultiplier = 0.2f;

    [Header("Roll stats")]
    [SerializeField] private float rollLength = 0.75f;
    [SerializeField] private float rollVelocityMultiplier = 1.5f;

    private bool canDoubleJump;
    private bool isGrounded, isLeftWallDetected, isRightWallDetected, isLeftLedgeDetected,
        isRightLedgeDetected, isWallSliding, canWallSlide, isMoving, isRolling, canClimb, isClimbing;

    private bool canWallJump = true;
    private bool canMove = true;

    private float moveInput;

    private int facingDir = 1;
    [SerializeField] private Vector2 wallJumpDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        CheckInput();
        FlipControl();
        AnimatorControl();

        if (!isGrounded && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }
        else canWallSlide = false;
    }

    private void FixedUpdate()
    {
        isMoving = rb.velocity.x != 0;

        if (isGrounded)
        {
            canMove = true;
            canDoubleJump = true;
            isClimbing = false;
        }

        if ((isLeftWallDetected || isRightWallDetected) && canWallSlide && !isGrounded)
        {
            isWallSliding = true;
            isRolling = false;
            slideMultiplier = (Input.GetAxis("Vertical") < 0) ? 0.9f : 0.3f;

            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * slideMultiplier);
        }
        else
        {
            isWallSliding = false;
            Move();
        }

        if (isWallSliding) canDoubleJump = true;
    }

    private void CheckInput()
    {
        if (isRolling) return;

        if (canMove) moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.Space)) Climb();

        if (Input.GetKeyUp(KeyCode.LeftShift)) Roll();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (isRolling) rb.velocity = new Vector2(facingDir * speed * rollVelocityMultiplier, rb.velocity.y);
        if (isClimbing) rb.velocity = new Vector2(0, 0);
    }

    private void Jump()
    {
        if (isWallSliding && canWallJump && facingDir == moveInput) WallJump();

        else if (isGrounded) rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        else if (canDoubleJump && !isWallSliding)
        {
            canMove = true;
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        canWallSlide = false;
        isRolling = false;
    }

    private void Climb()
    {
        if (isLeftLedgeDetected || isRightLedgeDetected && canClimb)
        {
            isClimbing = true;
        }
    }

    private void Roll()
    {
        if (!isWallSliding & canMove & isMoving)
        {
            StartCoroutine(RollCoroutine());
        }
    }

    private void WallJump()
    {
        canMove = false;

        Vector2 dir = new Vector2(wallJumpDir.x * -facingDir, wallJumpDir.y);

        rb.AddForce(dir, ForceMode2D.Impulse);
    }

    private void Flip(bool v)
    {
        facingDir = v ? 1 : -1;
        sprite.flipX = !v;
    }

    private void FlipControl()
    {
        float xMotion = rb.velocity.x;

        if (isWallSliding & isLeftWallDetected)
        {
            Flip(true);
        }
        else if (isWallSliding & isRightWallDetected)
        {
            Flip(false);
        }
        else
        {
            if (xMotion > 0.1f) Flip(true);
            else if (xMotion < -0.1f) Flip(false);
        }
    }

    private void AnimatorControl()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("isWallSliding", isWallSliding);
        //anim.SetBool("isWallDetected", )
        anim.SetBool("isRolling", isRolling);
        anim.SetBool("isClimbing", isClimbing);
    }
    #region Public API
    public void SetGroundedState(bool v)
    {
        isGrounded = v;
    }

    public void OnLeftSideWallTouch(bool v)
    {
        isLeftWallDetected = v;
    }

    public void OnRightSideWallTouch(bool v)
    {
        isRightWallDetected = v;
    }

    public void OnLeftWallLedge(bool v)
    {
        isLeftLedgeDetected = v;
    }

    public void OnRightWallLedge(bool v)
    {
        isRightLedgeDetected = v;
    }
    #endregion
}










