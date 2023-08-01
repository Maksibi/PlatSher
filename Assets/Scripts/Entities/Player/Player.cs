using System.Collections;
using UnityEngine;

public class Player : Entity
{
    #region Fields
    [Header("Combat")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;
    [Space]
    [Header("Move info")]
    public float moveSpeed = 12;
    public float jumpForce = 12;
    [Space]
    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }
    [Header("Components")]
    [SerializeField] private SpriteRenderer SpriteRenderer;

    #endregion

    public bool isBusy { get; private set; }

    public SkillManager skillManager { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }

    public PlayerCounterAttackState counterState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion States

    #region Unity API
    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash", SpriteRenderer);
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "Jump");

        primaryAttack = new PlayerPrimaryAttackState(stateMachine, this, "Attack");
        counterState = new PlayerCounterAttackState(stateMachine, this, "CounterAttack");
        deadState = new PlayerDeadState(stateMachine, this, "Dead");
    }

    protected override void Start()
    {
        base.Start();

        skillManager = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
        CheckInputForDash();
    }

    #endregion Unity API

    private void CheckInputForDash()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    #region Public API

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
    #endregion
}
