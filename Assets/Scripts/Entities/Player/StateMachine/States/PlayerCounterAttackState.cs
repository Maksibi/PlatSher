using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfullCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        Enemy enemy;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            enemy = collider.GetComponentInParent<Enemy>();

            if (enemy)
                if (enemy.CanBeStunned())
                {
                    stateTimer = 10;
                    player.anim.SetBool("SuccessfullCounterAttack", true);
                }
        }
        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}