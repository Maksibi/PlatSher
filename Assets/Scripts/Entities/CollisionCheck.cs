using UnityEngine;

#if UNITY_EDITOR
[DisallowMultipleComponent]
#endif
public class CollisionCheck : MonoBehaviour
{
    [Header ("Collision")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private Transform ledgeCheck, wallCheck, groundCheck;
    [SerializeField] private float groundCheckDistance;

    private Entity _controller = null;

    private bool isGrounded = false;
    private bool isTouchingLeftWall = false;
    private bool isTouchingRightWall = false;
    private bool ledgeRightWall = false;
    private bool ledgeLeftWall = false;

    private void Start()
    {
        _controller = gameObject.transform.root.GetComponent<Entity>();
    }

    private void Update()
    {
        _controller.SetGroundedState(isGrounded);
        _controller.OnLeftSideWallTouch(isTouchingLeftWall);
        _controller.OnRightSideWallTouch(isTouchingRightWall);

        //_playerController.OnLeftWallLedge(ledgeLeftWall);
        //_playerController.OnRightWallLedge(ledgeRightWall);
    }

    private void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        isTouchingLeftWall = CheckTouchingWall(Vector2.left);
        isTouchingRightWall = CheckTouchingWall(Vector2.right);
        ledgeLeftWall = LedgeCheck(Vector2.left);
        ledgeRightWall = LedgeCheck(Vector2.right);
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down,
            groundCheckDistance, groundLayer);

        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance, Color.red);

        return hit.collider != null;
    }

    private bool CheckTouchingWall(Vector2 direction)
    {
        float widthOffset = 0.03f;
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, direction,
            _capsuleCollider.bounds.extents.x + widthOffset, groundLayer);

        Debug.DrawRay(wallCheck.position, direction * (_capsuleCollider.bounds.extents.x + widthOffset), Color.red);
        return hit.collider != null;
    }

    private bool LedgeCheck(Vector2 direction)
    {
        float widthOffset = 0.03f;
        RaycastHit2D hit = Physics2D.Raycast(ledgeCheck.position, direction,
            _capsuleCollider.bounds.extents.x + widthOffset, groundLayer);

        Debug.DrawRay(ledgeCheck.position, direction * (_capsuleCollider.bounds.extents.x + widthOffset), Color.green);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        float widthOffset = 0.03f;

        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawRay(wallCheck.position, Vector2.left * (_capsuleCollider.bounds.extents.x + widthOffset), Color.red);
        Debug.DrawRay(wallCheck.position, Vector2.right * (_capsuleCollider.bounds.extents.x + widthOffset), Color.red);
        Debug.DrawRay(ledgeCheck.position, Vector2.left * (_capsuleCollider.bounds.extents.x + widthOffset), Color.green);
        Debug.DrawRay(ledgeCheck.position, Vector2.right * (_capsuleCollider.bounds.extents.x + widthOffset), Color.green);
    }
}


