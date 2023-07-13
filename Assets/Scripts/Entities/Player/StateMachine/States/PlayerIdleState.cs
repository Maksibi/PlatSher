public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) :
        base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
            stateMachine.ChangeState(player.moveState);
    }

    public override void Exit()
    { 
        base.Exit();
    }
}
