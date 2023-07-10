using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : Entity
    {
        [SerializeField] private EntityStats stats;

        private PlayerCombat combat;
        private PlayerMovement movement;
        private AnimatorController animator;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponentInChildren<AnimatorController>();
            combat = GetComponent<PlayerCombat>();
            movement = GetComponent<PlayerMovement>();
        }

        protected void Start()
        {
            combat.Init(movement, stats);
            movement.Init(this, stats);
            animator.Init(movement, combat, this);
        }

        protected override void Update()
        {
            base.Update();

            FlipControl();
        }

        private void FlipControl()
        {
            float xMotion = Rigid.velocity.x;

            if (movement.IsWallSliding & IsLeftWallDetected)
            {
                Flip(true);
            }
            else if (movement.IsWallSliding & IsRightWallDetected)
            {
                Flip(false);
            }
            else
            {
                if (xMotion > 0.1f) Flip(true);
                else if (xMotion < -0.1f) Flip(false);
            }
        }
    }
}
