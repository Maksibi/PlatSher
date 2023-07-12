using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Player
{
    public class PlayerState
    {
        protected PlayerStateMachine stateMachine;
        protected PlayerController player;
        protected AnimatorController animator;

        protected float xInput;
        private string animBoolName;

        public PlayerState(PlayerController _player, PlayerStateMachine _stateMachine, string animBoolName)
        {
            this.stateMachine = _stateMachine;
            this.player = _player;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            player.AnimatorSetBool(animBoolName, true);
        }

        public virtual void Update()
        {
            xInput = Input.GetAxisRaw("Horizontal");
            Debug.Log("In: " + animBoolName);
        }

        public virtual void Exit()
        {
            player.AnimatorSetBool(animBoolName, false);
        }
    }
}