using UnityEngine;

namespace Player
{
    public class AnimatorController : MonoBehaviour
    {
        private PlayerController controller;
        private PlayerMovement movement;
        private PlayerCombat combat;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            AnimatorControl();
        }

        private void AnimationTrigger()
        {
            combat.AttackOver();
        }

        public void Init(PlayerMovement movement, PlayerCombat combat, PlayerController controller)
        {
            this.movement = movement;
            this.combat = combat;
            this.controller = controller;
        }

        public void SetBool(string animBoolName, bool value)
        {
            animator.SetBool(animBoolName, value);
        }

        private void AnimatorControl()
        {
            animator.SetBool("isGrounded", controller.IsGrounded);
            animator.SetBool("IsMoving", movement.IsMoving);
            animator.SetBool("isWallSliding", movement.IsWallSliding);
            //animator.SetBool("isWallDetected", )
            animator.SetBool("isRolling", movement.IsRolling);
            //animator.SetBool("isClimbing", movement.IsClimbing);
            animator.SetBool("isAttacking", combat.IsAttacking);
            animator.SetInteger("comboCounter", combat.ComboCounter);
        }
    }
}
