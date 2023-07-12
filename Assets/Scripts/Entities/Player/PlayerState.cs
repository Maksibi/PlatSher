using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerState
    {
        protected PlayerStateMachine stateMachine;
        protected PlayerController player;

        private string animBoolName;

        public PlayerState(PlayerController _player, PlayerStateMachine _stateMachine, string animBoolName)
        {
            this.stateMachine = _stateMachine;
            this.player = _player;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            Debug.Log("Enter: " + animBoolName);
        }

        public virtual void Update()
        {
            Debug.Log("In: " + animBoolName);
        }

        public virtual void Exit()
        {
            Debug.Log("Ended: " + animBoolName);
        }
    }
}