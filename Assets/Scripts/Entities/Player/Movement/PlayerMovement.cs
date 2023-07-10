using UnityEngine;
using System.Collections;
using System;
using Player;

public class PlayerMovement : MonoBehaviour
{
    private EntityStats stats;

    private PlayerController controller;

    private float slideVelocity;
    private float rollTime;

    private bool canDoubleJump;
    private bool  isLeftLedgeDetected, isRightLedgeDetected, isWallSliding,
        canWallSlide, isMoving, isRolling, canClimb, isClimbing;

    public bool IsWallSliding => isWallSliding;
    public bool IsMoving => isMoving;
    public bool IsRolling => isRolling;
    

    private bool canWallJump = true;
    private bool canMove = true;

    private float moveInput;

 
    [SerializeField] private Vector2 wallJumpDir;

    protected void Start()
    {
        slideVelocity = stats.slideMultiplier;
        rollTime = stats.rollLength;
    }

    protected void Update()
    {
        CheckInput();

        if (!controller.IsGrounded && controller.Rigid.velocity.y < 0)
        {
            canWallSlide = true;
        }
        else canWallSlide = false;

        rollTime -= Time.deltaTime;
        if(rollTime < 0) isRolling = false;
    }

    private void FixedUpdate()
    {
        isMoving = controller.Rigid.velocity.x != 0;

        if (controller.IsGrounded)
        {
            canMove = true;
            canDoubleJump = true;
            isClimbing = false;
        }

        if ((controller.IsLeftWallDetected || controller.IsRightWallDetected) && canWallSlide && !controller.IsGrounded)
        {
            isWallSliding = true;
            isRolling = false;
            slideVelocity = (Input.GetAxis("Vertical") < 0) ? 0.9f : 0.3f;

            controller.Rigid.velocity = new Vector2(controller.Rigid.velocity.x, controller.Rigid.velocity.y * slideVelocity);
        }
        else
        {
            isWallSliding = false;
            Move();
        }

        if (isWallSliding) canDoubleJump = true;
    }
    #region Private API
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
        controller.Rigid.velocity = new Vector2(moveInput * stats.speed, controller.Rigid.velocity.y);
        if (isRolling) controller.Rigid.velocity = new Vector2(controller.FacingDir * stats.speed * stats.rollVelocityMultiplier, 
            controller.Rigid.velocity.y);

        if (isClimbing) controller.Rigid.velocity = new Vector2(0, 0);
    }

    private void Jump()
    {
        if (isWallSliding && canWallJump && controller.FacingDir == moveInput) WallJump();

        else if (controller.IsGrounded) controller.Rigid.velocity = new Vector2(controller.Rigid.velocity.x, stats.jumpForce);

        else if (canDoubleJump && !isWallSliding)
        {
            canMove = true;
            canDoubleJump = false;
            controller.Rigid.velocity = new Vector2(controller.Rigid.velocity.x, stats.jumpForce);
        }
        canWallSlide = false;
        isRolling = false;
    }

    private void Climb()
    {
        //if (isLeftLedgeDetected || isRightLedgeDetected && canClimb)
        //{
            //isClimbing = true;
        //}
    }

    private void Roll()
    {
        if (!isWallSliding & canMove & isMoving)
        {
            rollTime = stats.rollLength;

            if (rollTime > 0)
            {
                isRolling = true;
            }
            else isRolling = false;
        }
    }

    private void WallJump()
    {
        canMove = false;

        Vector2 dir = new Vector2(wallJumpDir.x * controller.FacingDir, wallJumpDir.y);

        Debug.Log("WALLJUMP         " + dir);

        controller.Rigid.AddForce(dir, ForceMode2D.Impulse);
    }

    #endregion
    #region Public API

    public void Init(PlayerController controller, EntityStats stats)
    {
        this.controller = controller;
        this.stats = stats;
    }
    #endregion
}










