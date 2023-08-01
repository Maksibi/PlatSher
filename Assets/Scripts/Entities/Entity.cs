using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] public Transform attackCheck;
    [SerializeField] public float attackCheckRadius;
    [Header("Knockback Info")]
    [SerializeField] protected float knockbackDuration = 0.07f;
    [SerializeField] protected Vector2 knockbackDir;
    protected bool isKnocked;


    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CharacterStats stats { get; private set; }
    #endregion Components

    public Action onFlipped;


    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        fx = GetComponentInChildren<EntityFX>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);

    #region SpriteFlip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if (onFlipped != null)
            onFlipped();
    }

    public virtual void FlipController(float x)
    {
        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();
    }
    #endregion

    #region Velocity
    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public virtual void SetZeroVelocity()
    {
        if (isKnocked)
            return;

        rb.velocity = Vector2.zero;
    }
    #endregion

    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name + " damaged");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        rb.velocity = new Vector2(knockbackDir.x * -facingDir, knockbackDir.y);

        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }

    public virtual void Die()
    {

    }
}
