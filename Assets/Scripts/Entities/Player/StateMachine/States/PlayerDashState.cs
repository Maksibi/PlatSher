using UnityEditor;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private readonly SpriteRenderer spriteRenderer;

    private float trailTimer = 0.05f;
    private float trailCreationTime = 0f;

    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName, SpriteRenderer sr) : base(_stateMachine, _player, _animBoolName)
    {
        spriteRenderer = sr;
    }

    public override void Enter()
    {
        base.Enter();

        player.skillManager.clone.CreateClone(player.transform, (PlayerStats)player.stats);

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

        trailCreationTime -= Time.deltaTime;

        if (trailCreationTime < 0f)
        {
            player.skillManager.dash.CreateDashTrail(player.transform, spriteRenderer.sprite);
            trailCreationTime = trailTimer;
        }
    }
}