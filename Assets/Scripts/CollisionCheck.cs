using UnityEngine;

#if UNITY_EDITOR
[DisallowMultipleComponent]
#endif
public class CollisionCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Transform ledgeCheck, wallCheck;

    private PlayerMovement _playerController = null;

    private bool isGrounded = false;
    private bool isTouchingLeftWall = false;
    private bool isTouchingRightWall = false;
    private bool ledgeRightWall = false;
    private bool ledgeLeftWall = false;
    
    private void Start()
    {
        _playerController = gameObject.transform.root.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _playerController.SetGroundedState(isGrounded);
        _playerController.OnLeftSideWallTouch(isTouchingLeftWall);
        _playerController.OnRightSideWallTouch(isTouchingRightWall);
        _playerController.OnLeftWallLedge(ledgeLeftWall);
        _playerController.OnRightWallLedge(ledgeRightWall);
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
        float heightOffset = 0.03f;
        RaycastHit2D hit = Physics2D.Raycast(_boxCollider.bounds.center, Vector2.down,
            _boxCollider.bounds.extents.y + heightOffset, groundLayer);

        Debug.DrawRay(_boxCollider.bounds.center, Vector2.down * (_boxCollider.bounds.extents.y + heightOffset), Color.red);
        
        return hit.collider != null;
    }

    private bool CheckTouchingWall(Vector2 direction)
    {
        float widthOffset = 0.03f;
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, direction,
            _boxCollider.bounds.extents.x + widthOffset, groundLayer);

        Debug.DrawRay(wallCheck.position, direction * (_boxCollider.bounds.extents.x + widthOffset), Color.red);
        return hit.collider != null;
    }

    private bool LedgeCheck(Vector2 direction)
    {
        float widthOffset = 0.03f;
        RaycastHit2D hit = Physics2D.Raycast(ledgeCheck.position, direction,
            _boxCollider.bounds.extents.x + widthOffset, groundLayer);

        Debug.DrawRay(ledgeCheck.position, direction * (_boxCollider.bounds.extents.x + widthOffset), Color.green);
        return hit.collider != null;
    }
}


