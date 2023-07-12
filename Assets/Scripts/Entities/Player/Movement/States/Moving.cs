using UnityEngine;


namespace Player
{
    public class Moving : PlayerState
    {
        public Moving(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) :
            base(_player, _stateMachine, _animBoolName)
        { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            player.SetVelocity(xInput, player.rb.velocity.y);

            if(xInput == 0)
                stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}