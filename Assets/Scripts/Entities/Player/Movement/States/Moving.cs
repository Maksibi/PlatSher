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
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}