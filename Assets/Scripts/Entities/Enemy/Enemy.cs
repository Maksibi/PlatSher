using UnityEngine;

public class Enemy : Entity
{
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    [Space]
    [Header("Battle Info")]
    [SerializeField] protected LayerMask playerLayer;
    public float playerDetectionDistance;
    public float attackDistance;
    public float attackCooldown;
    public float battleTime;
    [HideInInspector]public float lastTimeAttacked;
    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    public EnemyStateMachine stateMachine { get; private set; }

    #region Unity API

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    #endregion

    #region Public API

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position,
                                                    Vector2.right * facingDir, playerDetectionDistance, playerLayer);

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    #endregion
}