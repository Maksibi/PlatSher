using UnityEngine;

namespace Player
{
    public class Idle : PlayerState
    {
        public Idle(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) :
            base(_player, _stateMachine, _animBoolName)
        { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (xInput != 0)
                stateMachine.ChangeState(player.movingState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}